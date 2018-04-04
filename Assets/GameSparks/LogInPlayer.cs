using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//using UnityEngine.SceneManagement;

public class LogInPlayer : MonoBehaviour 
{
	public Text userNameInput, passwordInput; // these are set through the editor

	public Button authButton;

    void Start()
    {
        Button btn = authButton.GetComponent<Button>();
        btn.onClick.AddListener(AuthorizePlayerBttn);
    }
	
	public void AuthorizePlayerBttn()
	{
		Debug.Log ("Authorizing Player...");
		new GameSparks.Api.Requests.AuthenticationRequest ()
			.SetUserName (userNameInput.text)
			.SetPassword (passwordInput.value)
			.Send ((response) => {

					if(!response.HasErrors)
					{
						Debug.Log("Player Authenticated... \n User Name: "+response.DisplayName);
					//	SceneManager.LoadScene("Menu");
					}
					else
					{
						Debug.Log("Error Authenticating Player... \n "+response.Errors.JSON.ToString());
					}

		});
	}

	public void AuthenticateDeviceBttn()
	{
		Debug.Log ("Authenticating Device...");
		new GameSparks.Api.Requests.DeviceAuthenticationRequest ()
			.SetDisplayName ("Randy")
			.Send ((response) => {

					if(!response.HasErrors)
					{
						Debug.Log("Device Authenticated...");
					}
					else 
					{
						Debug.Log("Error Authenticating Device...");
					}
		});
	}
}


