using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonPlayPopupLevel : BaseButton
{
    [SerializeField]
    private TextMeshPro text;

    [SerializeField]
    private Level level;

    public override void OnClick()
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.ButtonSound_Two);
        this.getLevel();
        LevelCtrl.Instance.Level = level;
        SceneManager.LoadScene("GamePlay");
    }

    private void getLevel()
    {
        if (int.Parse(text.text[text.text.Length - 1].ToString()) == 1)
        {
            level = Level.Level1;
        }
        else if (int.Parse(text.text[text.text.Length - 1].ToString()) == 2)
        {
            level = Level.Level2;
        }
    }
}
