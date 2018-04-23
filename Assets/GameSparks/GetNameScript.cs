using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using GameSparks.Api;
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;

public class GetNameScript : MonoBehaviour {

	public Text outputDataName;

	public Button getButton;

	    void Start()
	    {
	        Button btn = getButton.GetComponent<Button>();
	        btn.onClick.AddListener(GetName);
	    }
		
		public void GetName()
	{
		Debug.Log ("Fetching Name...");

		new AccountDetailsRequest()
        .Send((response) => {
        string displayName = response.DisplayName; 
        outputDataName.text = displayName;
        });
       }
	
	
}
