using System;
using UnityEngine;

public class TextLevel : BaseText
{
    protected override void Awake()
    {
        base.Awake();
        this.LoadTextLevel();
    }

    private void LoadTextLevel()
    {
        this.text.text = 
            transform.parent.parent.name[
                transform.parent.parent.name.Length - 1
            ].ToString();
    }
}
