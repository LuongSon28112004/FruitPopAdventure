using DG.Tweening;
using UnityEngine;

public class ButtonSetting : BaseButton
{
    [Header("Button Setting")]
    [SerializeField]
    private GameObject MenuGame;

    public override void OnClick()
    {
        var rectTransform = MenuGame.GetComponent<RectTransform>();
        rectTransform.DOMoveX(4.4f, 0.5f); // di chuyển sang x = 4.2 trong 0.5 giây
    }
}
