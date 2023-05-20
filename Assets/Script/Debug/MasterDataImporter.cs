using System;
using System.Collections;
using System.Collections.Generic;
using CoreData.Master;
using LitJson;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class MasterDataImporter : MonoBehaviour
{
    static MasterDataImporter instance;

    public static MasterDataImporter I
    {
        get
        {
            if (!instance)
            {
                var obj = new GameObject();
                instance = obj.AddComponent<MasterDataImporter>();
            }
            return instance;
        }
    }
    
    bool imported = false;
    
    const string ApiKey = "AIzaSyA-Cs3B0KpMGW7BkkpKKPDn9I_IPdYnF8s";
    const string SheetId = "1i3JuxbKJZta9bCqVkP_5wQrR910RMkAxz7ZskJen8SA";
    
    static string URL(string sheetName) 
        => $"https://sheets.googleapis.com/v4/spreadsheets/{SheetId}/values/{sheetName}?key={ApiKey}";

    public void StartImport()
    {
        if (imported) return;
        StartCoroutine(ImportEnumerator());
    }

    IEnumerator ImportEnumerator()
    {
        var _ = new MasterData();
        
        yield return StartCoroutine(ImportSheet<CoreData.Master.Item>("Items", r =>
        {
            foreach (var i in r)
            {
                MasterData.I.Items[i.Id] = i;
            }
        }));
        
        yield return StartCoroutine(ImportSheet<CoreData.Master.Seed>("Seeds", r =>
        {
            foreach (var i in r)
            {
                MasterData.I.Seeds[i.Id] = i;
            }
        }));
        
        yield return StartCoroutine(ImportSheet<CoreData.Master.IntConst>("IntConsts", r =>
        {
            foreach (var i in r)
            {
                MasterData.I.IntConsts[i.Key] = i.Value;
            }
        }));

        imported = true;
    }

    IEnumerator ImportSheet<T>(string sheetName, UnityAction<T[]> onFinish) where T : new()
    {
        Debug.Log($"[{sheetName}] Start");
        
        var json = "";
        yield return GetJson(sheetName, jsonString => { json = jsonString; });
        var sheetValue = Deserialize<SheetValue>(json);

        onFinish.Invoke( Convert<T>(sheetValue.values));
        
        Debug.Log($"[{sheetName}] Finish");
    }

    static T[] Convert<T>(string[][] data) where T : new()
    {
        if (data.Length < 3)
        {
            Debug.LogError("取り込めない" + JsonMapper.ToJson(data));
            return null;
        }
        
        var fieldNames = data[0];
        var result = new T[data.Length - 3];
        
        for (var dataIndex = 3; dataIndex < data.Length; dataIndex++)
        {
            var obj = new T();
            for (var fieldIndex = 0; fieldIndex < data[0].Length; fieldIndex++)
            {
                if(data[dataIndex].Length <= fieldIndex) continue;
                
                var fieldName = fieldNames[fieldIndex];
                var field = typeof(T).GetField(fieldName);
                if (field == null) continue;

                object value = Type.GetTypeCode(field.FieldType) switch
                {
                    TypeCode.Int32 => int.Parse(data[dataIndex][fieldIndex]),
                    TypeCode.String => data[dataIndex][fieldIndex],
                    _ => null
                };

                field.SetValue(obj, value);
            }

            result[dataIndex - 3] = obj;
        }

        return result;
    }

    static T Deserialize<T>(string json) => JsonMapper.ToObject<T>(json);

    IEnumerator GetJson(string sheetName, UnityAction<string> onFinish)
    {
        var request = UnityWebRequest.Get(URL(sheetName));
        yield return StartCoroutine(ApiGetEnumerator(request));
        onFinish.Invoke(request.downloadHandler.text);
    }

    static IEnumerator ApiGetEnumerator(UnityWebRequest request)
    {
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            
        }
        else
        {
            Debug.LogError($"Api is Error. {request.downloadHandler.text}");
        }
        
        yield return "";
    }

    [Serializable]
    public class SheetValue
    {
        public string range;
        public string[][] values;
    }
}