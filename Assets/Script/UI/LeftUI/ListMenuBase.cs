using System.Collections.Generic;
using CoreData;
using UnityEngine;

public abstract class ListMenuBase : UIBase
{
    int currentSelectIndex = 0;
    public int CurrentSelectIndex => currentSelectIndex;
    
    [SerializeField] ListMenuElement elementPrefab;
    List<ListMenuElement> elements = new();

    public abstract void Initialize(SaveData saveData, State state);
    
    public abstract State NextState();

    public virtual string Action => null;
    public virtual LeftUIActionContext ActionValue => null;

    protected void SetContexts(IEnumerable<ListMenuElement.Context> contexts)
    {
        RemoveAll();
        foreach (var context in contexts)
        {
            var element = Instantiate(elementPrefab, this.transform);
            element.gameObject.SetActive(true);
            element.Initialize(context);
            elements.Add(element);
        }

        Select(0);
    }

    protected void Select(int selectIndex)
    {
        elements[currentSelectIndex].UnSelected();
        currentSelectIndex = selectIndex;
        elements[currentSelectIndex].Selected();
    }

    public void Next()
    {
        var index =  currentSelectIndex;
        index++;
       
        if (index >= elements.Count)
        {
            index = 0;
        }
        Select(index);
    }
    
    public void Prev()
    {
        var index = currentSelectIndex;
        index--;
        
        if (index < 0)
        {
            index = elements.Count - 1;
        }
        Select(index);
    }

    void RemoveAll()
    {
        foreach (var element in elements)
        {
            Destroy(element.gameObject);
        }
        elements.Clear();
    }
}