using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ListMenuElement : MonoBehaviour
{
    [SerializeField] GameObject selectedObject;
    [SerializeField] TMP_Text text;
    [SerializeField] Image icon;

    public class Context
    {
        public string Text;
        public Sprite Icon;
    }
    
    public void Initialize(Context context)
    {
        text.text = context.Text;
        // icon.sprite = context.Icon;
        
        selectedObject.SetActive(false);
    }

    public void Selected()
    {
        selectedObject.SetActive(true);
    }

    public void UnSelected()
    {
        selectedObject.SetActive(false);
    }
}