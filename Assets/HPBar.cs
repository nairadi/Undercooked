using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HPBar : MonoBehaviour {
	public PlayerTeam team;

	public Slider slider;
	public Image fillBar;
	public Text text;

	public Image faceImage;
	public Sprite happyFace;
	public Sprite neutralFace;
	public Sprite sadFace;
	public Sprite devastatedFace;

	const float minGreenPercent = 0.5f;
	const float minYellowPercent = 0.2f;
	const float minRedPercent = 0f;

	float currentHP = 1f;
	float maxHP = 15f;

	public void Awake() {
		slider.minValue = 0;
		slider.maxValue = maxHP;
		slider.value = currentHP;
		ReduceHP(0);
	}

	public void ReduceHP(float hp) {
		currentHP -= hp;
		currentHP = Mathf.Clamp(currentHP, 0, maxHP);
		slider.value = currentHP;

		float percent = currentHP / maxHP;
		if (percent > minGreenPercent) {
			fillBar.color = Color.green;
		}
		else if (percent > minYellowPercent) {
			GameController.instance.audioScript.PlayMusicSlow ();
			fillBar.color = Color.yellow;
		}
		else {
			GameController.instance.audioScript.PlayMusicSuperSlow ();
			fillBar.color = Color.red;
		}

		text.text = currentHP + " / " + maxHP;

		SetFace();

		if (currentHP <= 0) {
			GameController.instance.audioScript.PlayMusicEndGame ();
			GameController.instance.EndGame(team);
		}
	}

	private void SetFace() {
		float percent = currentHP / maxHP;
		if (percent >= 0.75f) {
			faceImage.sprite = happyFace;
		} else if (percent >= 0.5f) {
			faceImage.sprite = neutralFace;
		} else if (percent >= 0.25f) {
			faceImage.sprite = sadFace;
		} else {
			faceImage.sprite = devastatedFace;
		}
	}
}
