using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class BlendGreyscale : MonoBehaviour {
    public Grayscale gray;
    bool blending = false;
    bool unblending = false;
    bool grayscale = false;
	BGM bgmScript;
    public VignetteAndChromaticAberration vignette;

    public GameObject[] closedEyesObjects;
    public GameObject[] openEyesObjects;
	public float coolDown;
	private float coolDownTimer;

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
    }
	
	// Update is called once per frame
	void Update () {
		if (coolDownTimer == 0) {
			if (!blending && Input.GetButtonDown("Fire1") && gray.enabled == false)
			{
				//gray.enabled = !gray.enabled;
				coolDownTimer = coolDown;
				blending = true;
				bgmScript.switchToDreamBGM();
			}
			if (!unblending && Input.GetButtonDown("Fire1") && gray.enabled == true)
			{
				coolDownTimer = coolDown;
				gray.enabled = false;
				vignette.enabled = true;
				foreach (GameObject obj in closedEyesObjects) {
					obj.SetActive(false);
				}
				foreach (GameObject obj in openEyesObjects) {
					obj.SetActive(true);
				}
				unblending = true;
				vignette.intensity = 1;
				bgmScript.switchToLifeBGM();
				//blending = true;
			}
		}

			if (unblending)
			{
				if (vignette.intensity > 0)
				{
					vignette.intensity -= 0.1f;
				}
				else
				{
					vignette.intensity = 0;
					unblending = false;
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
							blending = false;
							grayscale = false;
						}
					}
				}

			}

		coolDownTimer = coolDownTimer <= 0 ? 0 : coolDownTimer - Time.deltaTime;
	}
}
