﻿using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class BlendGreyscale : MonoBehaviour {
    public Grayscale gray;
    bool blending = false;
    bool unblending = false;
    bool grayscale = false;
    int lastFlash = 0;
    BGM bgmScript;
    public VignetteAndChromaticAberration vignette;
    public ScreenOverlay screenOverlay;

    public GameObject[] closedEyesObjects;
    public GameObject[] openEyesObjects;
	public float coolDown;
	private float coolDownTimer;
    
    public Animator animator;
    public RuntimeAnimatorController[] animController;

    // Use this for initialization
    void Start () {
        foreach (GameObject obj in closedEyesObjects)
        {
            obj.SetActive(true);
        }
        foreach (GameObject obj in openEyesObjects)
        {
            obj.SetActive(false);
        }
		bgmScript = GetComponent<BGM>();
		coolDownTimer = 0;
        animator.runtimeAnimatorController = animController[1];
    }
	
	// Update is called once per frame
	void Update () {
		if (coolDownTimer == 0 && !GetComponent<PauseScreen>().getState()) {
			if (!blending && Input.GetButtonDown("Fire1") && gray.enabled == false)
			{
				coolDownTimer = coolDown;
				blending = true;
				bgmScript.switchToDreamBGM();
			}
			if (!unblending && Input.GetButtonDown("Fire1") && gray.enabled == true)
			{
				coolDownTimer = coolDown;
				vignette.enabled = true;
				unblending = true;
				vignette.intensity = 0;
				bgmScript.switchToLifeBGM();
			}
		}

        if(lastFlash >= 1)
        {

            if (screenOverlay.intensity > 0)
            {
                screenOverlay.intensity -= 0.1f;
            }
            else
            {
                screenOverlay.intensity = 0;
                lastFlash = 0;
            }

        }
       

        if (unblending)
			{
				if (vignette.intensity <= 1)
				{
					vignette.intensity += 0.05f;
				}
				else
                {
                    vignette.intensity = 0;
                	screenOverlay.intensity = 1;
					lastFlash = 1;
					gray.enabled = false;
                    animator.runtimeAnimatorController = animController[0];
                	unblending = false;
					foreach (GameObject obj in closedEyesObjects) {
						obj.SetActive(false);
					}
					foreach (GameObject obj in openEyesObjects) {
						obj.SetActive(true);
					}

				}
			}

			if (blending)
			{
				if (vignette.intensity < 1)
				{
					vignette.intensity += 0.1f;
				}
           
				else
				{
					if (!grayscale)
					{
						gray.rampOffset = -1;
						gray.enabled = true;
						vignette.enabled = false;
						vignette.intensity = 0;
						grayscale = true;
                        animator.runtimeAnimatorController = animController[1];
                        foreach (GameObject obj in closedEyesObjects) {
					        obj.SetActive(true);
						}
						foreach (GameObject obj in openEyesObjects) {
							obj.SetActive(false);
						}
					}
					else
					{
						gray.rampOffset += 0.05f;
						if (gray.rampOffset >= 0)
						{
                            gray.rampOffset = 0;
                            blending = false;
                            grayscale = false;
						}
					}
				}

			}

		coolDownTimer = coolDownTimer <= 0 ? 0 : coolDownTimer - Time.deltaTime;
	}
}
