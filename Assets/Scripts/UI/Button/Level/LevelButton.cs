using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : BaseButton
{
    public override void OnClick()
{
    string parentName = transform.parent.name; // vd: "Level1"
    if (Enum.TryParse(parentName, out Level level))
    {
        LevelCtrl.Instance.Level = level;
        SceneManager.LoadScene(1);
    }
    else
    {
        Debug.LogError("Tên parent không khớp với enum Level: " + parentName);
    }
}

}
