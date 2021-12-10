using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {
//	public Button playButton;
//	public Button exitButton;
	public SpriteRenderer playText;
	public SpriteRenderer player1Face;
	public SpriteRenderer player2Face;

//	public float rotationSpeed = 100000000f;
	private float blinkSpeed = 0.5f;

	private bool playTextVisible = true;
	private bool rotatedLeft = false;

	public void Awake() {
		Screen.SetResolution(1920, 1080, true);
//		playButton.onClick = () => {
//			Debug.Log("PLAY BUTTON");
//		};
//		Debug.Log("REGISTERING LISTENER");
//		playButton.onClick.AddListener(HandlePlayButton);
//		exitButton.onClick.AddListener(HandlePlayButton);

		foreach(string s in Input.GetJoystickNames()) {
			Debug.Log(s);
		}

		InvokeRepeating("BlinkPlayText", blinkSpeed, blinkSpeed);
		InvokeRepeating("BlinkPlayers", blinkSpeed, blinkSpeed);
	}

	public void HandlePlayButton() {
		SceneManager.LoadScene("PlayScene");
	}

	void Update() {
		if (Input.anyKeyDown) {
			SceneManager.LoadScene("PlayScene");
		}

//		float newAlpha = Mathf.Sin(Time.time * blinkSpeed);
//		playText.color = new Color(1f, 1f, 1f, newAlpha);

//		float rotation = Time.deltaTime * rotationSpeed;
//
//		if (!rotatingClockwise && player1Face.gameObject.transform.eulerAngles.z <= 180 && player1Face.gameObject.transform.eulerAngles.z >= 45) {
//			rotatingClockwise = true;
//		} else if (rotatingClockwise && player1Face.gameObject.transform.eulerAngles.z >= 180 && player1Face.gameObject.transform.eulerAngles.z <= 315) {
//			rotatingClockwise = false;
//		}
//
//		if (rotatingClockwise) {
//			rotation *= -1;
//		}
//
//		player1Face.gameObject.transform.Rotate(Vector3.forward * rotation);
//		player2Face.gameObject.transform.Rotate(Vector3.forward * -rotation);
	}

	void BlinkPlayText() {
//		Debug.Log("BKLINK");
		playTextVisible = !playTextVisible;
		playText.enabled = playTextVisible;
//		playText.color = new Color(1f, 1f, 1f, playTextVisible ? 1f : 0f);
	}

	void BlinkPlayers() {
		rotatedLeft = !rotatedLeft;
		float rotation = 22.5f;
		if (rotatedLeft) {
//			player1Face.gameObject.transform.Rotate(Vector3.forward * rotation);
//			player2Face.gameObject.transform.Rotate(Vector3.forward * -rotation);
			player1Face.gameObject.transform.eulerAngles = new Vector3(0, 0, rotation);
			player2Face.gameObject.transform.eulerAngles = new Vector3(0, 0, -rotation);
		} else {
			player1Face.gameObject.transform.eulerAngles = new Vector3(0, 0, -rotation);
			player2Face.gameObject.transform.eulerAngles = new Vector3(0, 0, rotation);
		}
	}
}
