using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHome : BaseButton
{
    public override void OnClick()
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.ButtonSound_Two);
        SceneManager.LoadScene("StartGame");
    }
}
