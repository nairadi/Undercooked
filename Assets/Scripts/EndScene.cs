using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScene : MonoBehaviour {
	public SpriteRenderer loserFace;
	public Sprite player1Face;
	public Sprite player2Face;
	public AudioScript audioScript;
	void Awake() {
		if (SceneController.instance.losingTeam == PlayerTeam.TeamLeft) {
			loserFace.sprite = player1Face;
		} else {
			loserFace.sprite = player2Face;
		}
	}
}
