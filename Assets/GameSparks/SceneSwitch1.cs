using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneSwitch1 : MonoBehaviour 
{

	public Button swButton;

    void Start()
    {
        Button btn = swButton.GetComponent<Button>();
        btn.onClick.AddListener(SwapSceneBttn);
    }
	

	public void SwapSceneBttn()
	{
		SceneManager.LoadScene("CreateAccount");	
	}
}

