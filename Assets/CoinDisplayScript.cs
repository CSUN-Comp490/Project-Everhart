using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using GameSparks;
using GameSparks.Core;
using GameSparks.Platforms;
using GameSparks.Platforms.WebGL;
using GameSparks.Platforms.Native;

public class CoinDisplayScript : MonoBehaviour 
{
	public Text coinData;
	public int coinValue;

	void Awake () 
	{
		new GameSparks.Api.Requests.LogEventRequest().SetEventKey("LOAD_PLAYER").Send((response) => {
		    if (!response.HasErrors) {
		        Debug.Log("Received Player Data From GameSparks...");
		        GSData data = response.ScriptData.GetGSData("player_Data");
		       coinValue = (int)data.GetInt("playerGold");
		       PlayerPrefs.SetInt("Coins", coinValue);
		       coinData.text = coinValue.ToString();

		    } else {
		        Debug.Log("Error Loading Player Data...");
		    }
});
	
	}
}

