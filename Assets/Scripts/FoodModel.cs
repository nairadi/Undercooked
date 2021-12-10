using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodModel : Singleton<FoodModel> {
	public Sprite BunTopSprite;
	public Sprite BunBottomSprite;
	public Sprite MeatCookedSprite;
	public Sprite LettuceChoppedSprite;
	public Sprite TomatorChoppedSprite;
	public Sprite BubbleSprite;

	public Sprite GetFoodTypeSprite(FoodType foodType) {
		switch(foodType) {
		case FoodType.TopBun: return BunTopSprite; break;
		case FoodType.BottomBun: return BunBottomSprite; break;
		case FoodType.Meat: return MeatCookedSprite; break;
		case FoodType.Lettuce: return LettuceChoppedSprite; break;
		case FoodType.Tomato: return TomatorChoppedSprite; break;
		case FoodType.Bubble: return BubbleSprite; break;
		}

		return null;
	}
}
