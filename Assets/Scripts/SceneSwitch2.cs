using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SceneSwitch2 : MonoBehaviour 
{
	public Button backButton;

    void Start()
    {
        Button btn = backButton.GetComponent<Button>();
        btn.onClick.AddListener(BackBttn);
    }
	
	public void BackBttn()
	{
		SceneManager.LoadScene("LogIn");		
	}
}