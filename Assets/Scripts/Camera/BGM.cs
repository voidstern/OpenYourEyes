using UnityEngine;
using System.Collections;

public class BGM : MonoBehaviour {
	[SerializeField] protected GameObject lifeBGM;
	[SerializeField] protected GameObject dreamBGM;
	[SerializeField] protected float fadeTime;

	private AudioSource lifeSource;
	private AudioSource dreamSource;
	// Use this for initialization
	void Start () {
		lifeSource = lifeBGM.GetComponent<AudioSource>();
		dreamSource = dreamBGM.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

		if (dreamSource.volume < 1) {
			dreamSource.volume += fadeTime * Time.deltaTime;
		}

		if (lifeSource.volume > 0) {
			lifeSource.volume -= fadeTime * Time.deltaTime;
		}
	}
}
