using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMainMenu : BaseButton
{
    [Header("Button MainMenu Setting")]
    [SerializeField]
    private GameObject MenuGame;

    public override void OnClick()
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.ButtonSound_Two);
        var rectTransform = MenuGame.GetComponent<RectTransform>();

        // Thực hiện di chuyển và chỉ load scene sau khi hoàn tất
        rectTransform.DOMoveX(0f, 0.5f)
            .OnComplete(() =>
            {
                SceneManager.LoadScene("StartGame");
            });
    }
}
