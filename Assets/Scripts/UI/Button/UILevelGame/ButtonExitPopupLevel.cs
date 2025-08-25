using DG.Tweening;
using UnityEngine;

public class ButtonExitPopupLevel : BaseButton
{
    [SerializeField]
    private GameObject popupLevelGame;

    [SerializeField]
    private GameObject topMenu;

    [SerializeField]
    private GameObject bottomMenu;

    public override void OnClick()
    {
        popupLevelGame.SetActive(false);
        RectTransform rectTransform1 = topMenu.GetComponent<RectTransform>();
        rectTransform1.DOMoveY(0f, 0.5f); // di chuyển sang x = 0 trong 0.5 giây
        RectTransform rectTransform2 = bottomMenu.GetComponent<RectTransform>();
        rectTransform2.DOMoveY(0f, 0.5f); // di chuyển sang x = 0 trong 0.5 giây
    }
}
