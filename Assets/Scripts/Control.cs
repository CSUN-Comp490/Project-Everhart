using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Control : MonoBehaviour
{
	public void NextScene(string scene)
	{
		Application.LoadLevel(scene);
	}

	public void quit()
	{
		Application.Quit();
	}
}
