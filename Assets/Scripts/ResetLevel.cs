using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetLevel : MonoBehaviour {

	public GameObject player;
	public int gameOverPosY = -1;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate() {
		if (player.transform.position.y < gameOverPosY) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}
}
