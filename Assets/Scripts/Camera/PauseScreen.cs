using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityStandardAssets.ImageEffects;

public class PauseScreen : MonoBehaviour {
    public Blur blur;
    public Canvas pauseScreen;
    // Use this for initialization
    void Start () {
        blur.enabled = false;
        pauseScreen.enabled = false;

    }
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
	}

    void PauseGame()
    {
        Time.timeScale = 0;
        blur.enabled = true;
        pauseScreen.enabled = true;
    }

    public void ResumeGame ()
    {
        blur.enabled = false;
        pauseScreen.enabled = false;
        Time.timeScale = 1;
    }

    public void QuitGame ()
    {
        Application.Quit();
    }
}
