using DG.Tweening;
using UnityEngine;

public class ButtonSetting : BaseButton
{
    [Header("Button Setting")]
    [SerializeField]
    private GameObject MenuGame;

    [SerializeField]
    private GameObject ButtonMainMenu;

    [SerializeField]
    private GameObject ButtonBackToMap;

    [SerializeField]
    private GameObject Logo;

    public override void OnClick()
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.ButtonSound_Two);
        var rectTransform = MenuGame.GetComponent<RectTransform>();
        rectTransform.DOMoveX(4.55f, 0.5f);

        // Logo rơi xuống từ Y = 70 về Y = 19
        RectTransform logoRect = Logo.GetComponent<RectTransform>();
        logoRect.DOAnchorPosY(19f, 0.6f)
            .From(new Vector2(0, 70))
            .SetEase(Ease.OutBounce);

        // Rung 2 button (shake)
        ButtonMainMenu.transform.DOShakePosition(
            0.5f,   // thời gian rung
            strength: new Vector3(10f, 10f, 0), // biên độ rung theo X,Y
            vibrato: 15,  // số lần rung
            randomness: 90,
            snapping: false,
            fadeOut: true);

        ButtonBackToMap.transform.DOShakePosition(
            0.5f,
            strength: new Vector3(10f, 10f, 0),
            vibrato: 15,
            randomness: 90,
            snapping: false,
            fadeOut: true);
    }
}
