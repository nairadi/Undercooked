using UnityEngine;

public class GarbageTile : Tile
{
	public SpriteRenderer garbageSprite;
	public Sprite garbageLeftSprite;
	public Sprite garbageRightSprite;

	public enum GarbageFaceDirection
	{
		Left,
		Right,
	}

	public void Init(GarbageFaceDirection garbageFaceDirection) {
		if (garbageFaceDirection == GarbageFaceDirection.Left) {
			garbageSprite.sprite = garbageLeftSprite;
		} else if (garbageFaceDirection == GarbageFaceDirection.Right) {
			garbageSprite.sprite = garbageRightSprite;
		}
	}

	public override void FoodButton(PlayerController player) {
		if (player.IsHoldingItem()) {
			Food food = player.GetHeldFood();
			if (food != null) {
				player.DeleteHeldItem();
			}
		}
	}
}
