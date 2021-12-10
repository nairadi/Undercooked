using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Item {
	public FoodType type;
	public FoodState state;
	SpriteCollection foodSprites;

	public List<FoodType> stack = new List<FoodType>();
	public GameObject stackBottom;
	public GameObject stackTop;

	public ProcessBar processBar;
	public float timer;

	public Sprite BunTopSprite;
	public Sprite BunBottomSprite;
	public Sprite MeatCookedSprite;
	public Sprite MeatRawSprite;
	public Sprite LettuceChoppedSprite;
	public Sprite LettuceRawSprite;
	public Sprite TomatorChoppedSprite;
	public Sprite TomatorRawSprite;
	public SpriteRenderer sprite;

	public void Init(FoodType type) {
		this.type = type;
		this.state = FoodState.Raw;

		if (type == FoodType.TopBun) {
			stack.Add(type);
		}

		if (type == FoodType.BottomBun) {
			stack.Add(type);
		}

		UpdateSprite();
	}

	public void SetType(FoodType type) {
		this.type = type;
		UpdateSprite();
	}

	public void SetState(FoodState state) {
		Debug.Log(state.ToString());
		this.state = state;
		UpdateSprite();

		if (state == FoodState.Chopped || state == FoodState.Cooked) {
			stack.Add(type);
			PrintStack();
		}
	}

	private void UpdateSprite() {
		switch(type) {
		case FoodType.TopBun: sprite.sprite = BunTopSprite; break;
		case FoodType.BottomBun: sprite.sprite = BunBottomSprite; break;
		case FoodType.Meat:
			switch(state) {
			case FoodState.Raw: sprite.sprite = MeatRawSprite; break;
			case FoodState.Cooked: sprite.sprite = MeatCookedSprite; break;
			}
			break;
		case FoodType.Lettuce:
			switch(state) {
			case FoodState.Raw: sprite.sprite = LettuceRawSprite; break;
			case FoodState.Chopped: sprite.sprite = LettuceChoppedSprite; break;
			}
			break;
		case FoodType.Tomato:
			switch(state) {
			case FoodState.Raw: sprite.sprite = TomatorRawSprite; break;
			case FoodState.Chopped: sprite.sprite = TomatorChoppedSprite; break;
			}
			break;
		}
	}

	public override void OnPickup() {
		StopProcessingFood(resetTimer: false);
	}

	public override void OnDrop() {
	}

	public void ProcessFoodManual(float time, float duration, FoodState state) {
		if (this.state == state) return;

		processBar.gameObject.SetActive(true);
		timer += time;
		processBar.SetBarFill(timer / duration);
		if (timer > duration) {
			SetState(state);
			processBar.gameObject.SetActive(false);
		}
	}

	public void ProcessFoodAutomatic(float duration, FoodState state, bool resetTimer = true) {
		if (resetTimer) {
			timer = 0f;
		}
		processBar.gameObject.SetActive(true);

		StopAllCoroutines();
		StartCoroutine(ProcessFoodLoop(duration, state));
	}

	public void StopProcessingFood(bool resetTimer = true) {
		if (resetTimer) {
			timer = 0f;
			processBar.gameObject.SetActive(false);
		}
		StopAllCoroutines();
	}

	IEnumerator ProcessFoodLoop(float duration, FoodState state) {
		while (timer < duration) {
			timer += Time.deltaTime;
			processBar.SetBarFill(timer / duration);
			yield return null;
		}

		SetState(state);
		processBar.gameObject.SetActive(false);
	}

	public bool CanCombine() {
		return stack.Count > 0;
	}

	public void PrintStack() {
		Debug.Log("PRINTING STACK OF SIZE " + stack.Count);
		foreach(FoodType type in stack) {
			Debug.Log(type);
		}
		Debug.Log("DONE PRINTING STACK");
	}
}
