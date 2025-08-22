using UnityEngine;
using UnityEngine.SceneManagement;

public class DoneButton : BaseButton
{
    public override void OnClick()
    {
        Debug.Log("Done Clicked");
        GameManager.Instance.IsDone = false;
        GameManager.Instance.CheckLoss = false;

        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(1);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // hủy đăng ký để không bị gọi lại nhiều lần
        SceneManager.sceneLoaded -= OnSceneLoaded;

        // lấy lại StarsEffect trong scene mới
        UILevelGameManager.Instance.AllStars[0].PlayStarEffect(3);
    }

}
