using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityStandardAssets.ImageEffects;

public class PauseScreen : MonoBehaviour {
    public Blur blur;
    public Canvas pauseScreen;
    bool paused = false;
    ColorBlock colorBlock;

    public Button[] buttons;
    int active;
    private float coolDownTimer;


    // Use this for initialization
    void Start () {
        blur.enabled = false;
        pauseScreen.enabled = false;
        active = 0;
        colorBlock = buttons[active].colors;
        colorBlock.normalColor = new Color(0.87843137254901960784313725490196f, 0.94117647058823529411764705882353f, 0.18431372549019607843137254901961f);
        buttons[active].colors = colorBlock;
        coolDownTimer = 0;
    }
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetButtonDown("Cancel"))
        {

            if(paused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
			paused = !paused;
        }
        if (paused && Input.GetButtonDown("Jump"))
        {
            if (buttons[active].name.Equals("ResumeButton"))
            {
				paused = !paused;
                ResumeGame();
            }
            if (buttons[active].name.Equals("QuitButton"))
            {
                QuitGame();
            }
        }
      //  float cooldown = Time.time;

        if(paused && Input.GetAxisRaw("Vertical") < 0 && coolDownTimer == 0)
        {
            coolDownTimer = 0.25f;
            buttons[active].colors = ColorBlock.defaultColorBlock;
            active = (active + 1) % buttons.Length;
            buttons[active].colors = colorBlock;
        }
        if (paused && Input.GetAxisRaw("Vertical") > 0 && coolDownTimer == 0)
        {
            coolDownTimer = 0.25f;
            buttons[active].colors = ColorBlock.defaultColorBlock;
            active = (active - 1 + buttons.Length) % buttons.Length;
            buttons[active].colors = colorBlock;
        }
        Debug.Log(active);

        coolDownTimer = coolDownTimer <= 0 ? 0 : coolDownTimer - Time.unscaledDeltaTime;
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

    public bool getState()
    {
        return paused;
    }
}
