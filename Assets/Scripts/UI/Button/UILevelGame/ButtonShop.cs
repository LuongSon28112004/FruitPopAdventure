using DG.Tweening;
using UnityEngine;

public class ButtonShop : BaseButton
{
    [Header("Button Shop Settings")]
    [SerializeField]
    private GameObject MenuGame;
    [SerializeField]
    private GameObject MenuGameBottom;
    [SerializeField]
    private GameObject Map;

    public override void OnClick()
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.ButtonSound_One);
        Map.transform.Find("PanelMap").gameObject.SetActive(false);
        Map.transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f);
        var rectTransform1 = MenuGame.GetComponent<RectTransform>();
        rectTransform1.DOMoveX(-5.05f, 0.5f); // di chuyển sang x = 4.2 trong 0.5 giây
        var rectTransform2 = MenuGameBottom.GetComponent<RectTransform>();
        rectTransform2.DOMoveX(0f, 0.5f); // di chuyển sang x = 4.2 trong 0.5 giây
        transform.parent.Find("PanelShop").gameObject.SetActive(true);
        transform.parent.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.5f);//scale button len
    }
}
