using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Authentication.PlayerAccounts;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLoginUnityPlayer : BaseButton
{
    [SerializeField]
    private GameObject loadingPanel;

    protected override async void Start()
    {
        await UnityServices.InitializeAsync();

        PlayerAccountService.Instance.SignedIn += SignInWithUnityAuth;
    }

    public override async void OnClick()
    {
        await StartPlayerAccountsSignInAsync();
    }

    private async Task StartPlayerAccountsSignInAsync()
    {
        if (PlayerAccountService.Instance.IsSignedIn)
        {
            SignInWithUnityAuth();
            return;
        }

        try
        {
            loadingPanel.SetActive(true); // Hiện loading khi bắt đầu sign-in
            await PlayerAccountService.Instance.StartSignInAsync();
        }
        catch (PlayerAccountsException ex)
        {
            Debug.LogException(ex);
            loadingPanel.SetActive(false);
        }
        catch (RequestFailedException ex)
        {
            Debug.LogException(ex);
            loadingPanel.SetActive(false);
        }
    }

    private async void SignInWithUnityAuth()
    {
        try
        {
            loadingPanel.SetActive(true); // Hiện loading khi bắt đầu Auth
            await AuthenticationService.Instance.SignInWithUnityAsync(
                PlayerAccountService.Instance.AccessToken
            );
            Debug.Log(
                "SignIn is successful. Player id: " + AuthenticationService.Instance.PlayerId
            );

            loadingPanel.SetActive(false); // Tắt loading khi thành công
            SceneManager.LoadScene("StartGame");
        }
        catch (AuthenticationException ex)
        {
            Debug.LogException(ex);
            loadingPanel.SetActive(false);
        }
        catch (RequestFailedException ex)
        {
            Debug.LogException(ex);
            loadingPanel.SetActive(false);
        }
    }
}
