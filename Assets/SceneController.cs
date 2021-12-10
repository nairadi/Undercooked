using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController> {
	public PlayerTeam losingTeam;
	public bool canRestart = false;

	void Awake() {
		DontDestroyOnLoad(this);

		SceneManager.activeSceneChanged += (_, scene) => {
			if (scene.name == "EndScene") {
				Invoke("AllowRestart", 3f);
			}
		};
	}

	void Update() {
		if (Input.anyKeyDown && canRestart) {
			losingTeam = PlayerTeam.None;
			canRestart = false;
			SceneManager.LoadScene("PlayScene");
		}
	}

	private void AllowRestart() {
		canRestart = true;
	}
}
