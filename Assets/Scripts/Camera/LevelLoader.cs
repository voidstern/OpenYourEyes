using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {
    public VignetteAndChromaticAberration vignette;
    public AudioClip stinger;

    private float coolDownTimer;
    private float deathTimer;
    private float loadTimer;
    private bool onStart;
    public int gameOverPosY = -1;
	private bool loading;
    // Use this for initialization

    void Start () {
        coolDownTimer = 0.5f;
        vignette.intensity = 1;
        deathTimer = 0.5f;
        loadTimer = 0.5f;
        onStart = true;
		loading = false;
    }
	
	// Update is called once per frame
	void Update () {
	    if(coolDownTimer == 0 && onStart)
        {
            if(vignette.intensity > 0)
            {
                vignette.intensity -= 0.05f;
            }
            else
            {
                vignette.intensity = 0;
                onStart = false;
            }
        }
        if (transform.position.y < gameOverPosY)
        {
			GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
			GetComponent<AudioSource>().PlayOneShot(stinger);
            if (vignette.intensity < 1)
            {
                vignette.intensity += 0.05f;
            }
            else
            {
                vignette.intensity = 1;
                if (deathTimer == 0)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
                deathTimer = deathTimer <= 0 ? 0 : deathTimer - Time.deltaTime;
            }
        }
        coolDownTimer = coolDownTimer <= 0 ? 0 : coolDownTimer - Time.deltaTime;
        if (loading) {
			if (vignette.intensity < 1)
			{
				vignette.intensity += 0.05f;
			}
			else
			{
				vignette.intensity = 1;
				if (loadTimer == 0)
				{
					SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
				}
				loadTimer = loadTimer <= 0 ? 0 : loadTimer - Time.deltaTime;
			}
		}
	}

    public void LoadNextLevel ()
    {
        loading = true;
    }
}
