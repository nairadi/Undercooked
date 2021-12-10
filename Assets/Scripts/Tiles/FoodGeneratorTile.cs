using UnityEngine;

public class FoodGeneratorTile : Tile
{
	public GameObject foodPrefab;
	public SpriteRenderer spriteRenderer;

	public Sprite BunTopCrateSprite;
	public Sprite BunBottomCrateSprite;
	public Sprite MeatCrateSprite;
	public Sprite LettuceCrateSprite;
	public Sprite TomatoCrateSprite;

	private FoodType foodType;

	public void Init(FoodType foodType) {
		this.foodType = foodType;
		UpdateSprite();
	}

	public override void FoodButton(PlayerController player) {
		if (!player.IsHoldingItem()) {
			GameObject foodGO = Instantiate(foodPrefab, this.transform);
			Food food = foodGO.GetComponent<Food>();
			food.Init(this.foodType);

			player.PickupItem(food);
		}
	}
		
	private void UpdateSprite() {
		spriteRenderer.sprite = GetSpriteFromFoodType();
	}

	private Sprite GetSpriteFromFoodType() {
		switch(foodType) {
		case FoodType.TopBun: return BunTopCrateSprite;
		case FoodType.BottomBun: return BunBottomCrateSprite;
		case FoodType.Meat: return MeatCrateSprite;
		case FoodType.Lettuce: return LettuceCrateSprite;
		case FoodType.Tomato: return TomatoCrateSprite;
		}

		return null;
	}
}
