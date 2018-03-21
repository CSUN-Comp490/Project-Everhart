using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RegisterPlayer_Script : MonoBehaviour {

	public Text userNameInput, passwordInput; // these are set through the editor

	public Button yourButton;

    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(RegisterPlayerBttn);
    }

	public void RegisterPlayerBttn()
	{
		Debug.Log ("Registering Player...");
		new GameSparks.Api.Requests.RegistrationRequest ()
			.SetDisplayName (userNameInput.text)
			.SetUserName (userNameInput.text)
			.SetPassword (passwordInput.text)
			.Send ((response) => {

					if(!response.HasErrors)
					{
						Debug.Log("Player Registered \n User Name: "+response.DisplayName);
						SceneManager.LoadScene("Menu");
					}
					else
					{
						Debug.Log("Error Registering Player... \n "+response.Errors.JSON.ToString());
					}

		});

	}

}