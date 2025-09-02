using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : BaseButton
{
    [SerializeField]
    private GameObject popupLevelGame;

    [SerializeField]
    private GameObject topMenu;

    [SerializeField]
    private GameObject bottomMenu;
    [SerializeField]
    private TextMeshPro textLevel;

    public override void OnClick()
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.ButtonSound_Two);
        popupLevelGame.SetActive(true);
        RectTransform rectTransform1 = topMenu.GetComponent<RectTransform>();
        rectTransform1.DOMoveY(1.2f, 0.5f); // di chuyển sang x = 0 trong 0.5 giây
        RectTransform rectTransform2 = bottomMenu.GetComponent<RectTransform>();
        rectTransform2.DOMoveY(-1.2f, 0.5f); // di chuyển sang x = 0 trong 0.5 giây
        this.getText();
    }

    public void getText()
    {
        string parentName = transform.parent.name;
        textLevel.text = "LEVEL " +  parentName[parentName.Length - 1].ToString();
    }
}

// public override void OnClick()
//     {
//         string parentName = transform.parent.name; // vd: "Level1"
//         if (Enum.TryParse(parentName, out Level level))
//         {
//             LevelCtrl.Instance.Level = level;
//             SceneManager.LoadScene("GamePlay");
//         }
//         else
//         {
//             Debug.LogError("Tên parent không khớp với enum Level: " + parentName);
//         }
//     }
