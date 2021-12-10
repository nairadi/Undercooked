using UnityEngine;

public class CuttingBoardTile : Tile
{
	public override void InteractButton(PlayerController player) {
		if (itemOnTop != null) {
			Food food = itemOnTop.GetComponent<Food>();
			if (food.state == FoodState.Raw && (food.type == FoodType.Lettuce || food.type == FoodType.Tomato)) {
				food.ProcessFoodManual(0.05f, 3f, FoodState.Chopped);
			}
		}
	}
}
