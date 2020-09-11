using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelBasic : MonoBehaviour
{
    public int levelId;
    public bool async;

    public void LoadLevel()
    {
	if(async)
	{
	    SceneManager.LoadSceneAsync(levelId);
	}
	else
	{
	    SceneManager.LoadScene(levelId);
	}
    }
}
