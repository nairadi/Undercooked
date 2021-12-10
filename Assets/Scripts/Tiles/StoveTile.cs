using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveTile : Tile {

	public float cookTime = 6f;
	public GameObject foodPrefab;
	public ParticleSystem ps;

	public override void FoodButton(PlayerController player) {
		if (player.IsHoldingItem()) {
			Food food = player.GetHeldFood();
			player.DropItem();
			if (food.type == FoodType.Meat && food.state == FoodState.Raw) {
				food.ProcessFoodAutomatic(cookTime, FoodState.Cooked, false);
				ps.Play();
			}
		}
		else if (itemOnTop) {
			player.PickupItem(itemOnTop);
			itemOnTop = null;
			ps.Stop();
		}
	}
}
