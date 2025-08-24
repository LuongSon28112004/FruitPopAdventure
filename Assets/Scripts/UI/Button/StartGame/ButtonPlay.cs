using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonPlay : BaseButton
{
    public override void OnClick()
    {
        SceneManager.LoadScene("UILevelGame");
    }
}
