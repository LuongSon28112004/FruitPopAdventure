using DG.Tweening;
using UnityEngine;

public class ButtonExitSetting : BaseButton
{
    [Header("Button Exit Setting")]
     [SerializeField]
    private GameObject MenuGame;

    public override void OnClick()
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.ButtonSound_Two);
        var rectTransform = MenuGame.GetComponent<RectTransform>();
        rectTransform.DOMoveX(0f, 0.5f); // di chuyển sang x = 4.2 trong 0.5 giây
    }
}
