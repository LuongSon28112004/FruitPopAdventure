using UnityEngine;


public enum Type
{
    One,
    Two,
    Three,
    Four,
    Five,
    Six,
    Seven
}

public enum isClick
{
    True,
    False
}


public class TypeItem : MonoBehaviour
{
    [SerializeField] private Type _type;
    [SerializeField] private isClick _isClick;

    public Type Type { get => _type; set => _type = value; }
    public isClick IsClick { get => _isClick; set => _isClick = value; }

    public void getTypeByIndex(int index)
    {
        _type = (Type)index;
    }
}
