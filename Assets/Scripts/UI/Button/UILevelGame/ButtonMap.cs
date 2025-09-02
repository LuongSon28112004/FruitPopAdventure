using DG.Tweening;
using UnityEngine;

public class ButtonMap : BaseButton
{
    [Header("Button Map Settings")]
    [SerializeField]
    private GameObject MenuGame;
    [SerializeField]
    private GameObject MenuGameBottom;
    [SerializeField]
    private GameObject Shop;

    public override void OnClick()
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.ButtonSound_One);
        Shop.transform.Find("PanelShop").gameObject.SetActive(false);
        Shop.transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f);
        var rectTransform1 = MenuGame.GetComponent<RectTransform>();
        rectTransform1.DOMoveX(0f, 0.5f); // di chuyển sang x = 0 trong 0.5 giây
        var rectTransform2 = MenuGameBottom.GetComponent<RectTransform>();
        rectTransform2.DOMoveX(0f, 0.5f); // di chuyển sang x = 0 trong 0.5 giây
        transform.parent.Find("PanelMap").gameObject.SetActive(true);
        transform.parent.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.5f);//scale button len

    }

    protected override void Awake()
    {
        base.Awake();
        transform.parent.Find("PanelMap").gameObject.SetActive(true);
        transform.parent.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.5f);//scale button len
    }
}
