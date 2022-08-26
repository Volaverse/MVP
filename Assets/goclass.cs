using UnityEngine;
using UnityEngine.SceneManagement;

public class goclass : MonoBehaviour
{
    public void ChangeScene(int index)
	{
        Debug.Log("index:");
        Debug.Log(index);
        SceneManager.LoadScene(index);

	}
	public void Exit()
	{
		Application.Quit();
	}
}
