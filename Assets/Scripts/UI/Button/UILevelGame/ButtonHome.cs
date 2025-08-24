using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHome : BaseButton
{
    public override void OnClick()
    {
        SceneManager.LoadScene("StartGame");
    }
}
