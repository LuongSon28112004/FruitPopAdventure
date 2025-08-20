using System;
using UnityEngine;


[Serializable]
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

public static class TypeHelper
{
    // Truyền enum => trả về int
    public static int ToInt(Type type)
    {
        return (int)type;
    }

    // Truyền int => trả về enum
    public static Type ToEnum(int value)
    {
        if (Enum.IsDefined(typeof(Type), value))
        {
            return (Type)value;
        }
        else
        {
            throw new ArgumentOutOfRangeException(nameof(value), "Giá trị không hợp lệ cho enum Type");
        }
    }
}

[Serializable]
public enum isClick
{
    True,
    False
}

[Serializable]
public class TypeItem : BaseMonoBehaviour
{
    [SerializeField] private Type _type;
    [SerializeField] private isClick _isClick;

    public Type Type { get => _type; set => _type = value; }
    public isClick IsClick { get => _isClick; set => _isClick = value; }

    public void getTypeByIndex(int index)
    {
        _type = (Type)(index -1);
    }
}
