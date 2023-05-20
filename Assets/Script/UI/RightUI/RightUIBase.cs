using CoreData;
using UnityEngine;

public abstract class RightUIBase : MonoBehaviour
{
    public abstract void Initialize(SaveData saveData);

    public abstract void Select(int value);
}