using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class ButtonScript : MonoBehaviour {

	public void Play()
    {
        SceneManager.LoadScene("tutorialLevel");
    }
    

    public void Exit()
    {
#if UNITY_EDITOR
        Application.Quit();//Only for built exe files
#else
        EditorApplication.isPlaying = false;
#endif
    }
}
