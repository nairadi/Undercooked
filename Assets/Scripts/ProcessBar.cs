using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProcessBar : MonoBehaviour {
	public Image barFill;

	public void SetBarFill(float percent) {
		barFill.fillAmount = percent;
	}
}
