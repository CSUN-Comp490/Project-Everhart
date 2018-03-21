using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RegisterPlayer_HARDCODETEST : MonoBehaviour {

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
			.SetDisplayName ("DisplayMan")
			.SetUserName ("UziUser")
			.SetPassword ("passw0rd")
			.Send ((response) => {

					if(!response.HasErrors)
					{
						Debug.Log("Player Registered \n User Name: "+response.DisplayName);
					}
					else
					{
						Debug.Log("Error Registering Player... \n "+response.Errors.JSON.ToString());
					}

		});

	}

}
