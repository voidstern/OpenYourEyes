using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class BGM : MonoBehaviour {
	[SerializeField] protected GameObject lifeBGM;
	[SerializeField] protected GameObject dreamBGM;
	[SerializeField] protected float fadeTime;

	private AudioSource lifeSource;
	private AudioSource dreamSource;

	private bool switchBGM;
	// Use this for initialization
	void Start () {
		lifeSource = lifeBGM.GetComponent<AudioSource>();
		dreamSource = dreamBGM.GetComponent<AudioSource>();
		switchBGM = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (switchBGM) {
			if (dreamSource.volume > 0) {
				dreamSource.volume -= fadeTime * Time.deltaTime;
			}

			if (lifeSource.volume < 1) {
				lifeSource.volume += fadeTime * Time.deltaTime;
			}
		} else {
			if (dreamSource.volume < 1) {
				dreamSource.volume += fadeTime * Time.deltaTime;
			}

			if (lifeSource.volume > 0) {
				lifeSource.volume -= fadeTime * Time.deltaTime;
			}
		}

	}

	public void switchToDreamBGM() {
		switchBGM = false;
	}

	public void switchToLifeBGM() {
		switchBGM = true;
	}
}
