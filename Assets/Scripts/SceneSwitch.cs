using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SceneSwitch : MonoBehaviour 
{
	public Button authButton;

    void Start()
    {
        Button btn = authButton.GetComponent<Button>();
        btn.onClick.AddListener(CreateAccountBttn);
    }
	
	public void CreateAccountBttn()
	{
		SceneManager.LoadScene("CreateAccount");		
	}
}