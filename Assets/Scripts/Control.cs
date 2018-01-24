using UnityEngine;
using UnityEngine.SceneManagement;

public class Control : MonoBehaviour
{
	public void NextScene(string scene)
	{
		Application.LoadLevel (scene);
	}
}
