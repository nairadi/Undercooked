using UnityEngine;

public class Tile : MonoBehaviour {
	public bool canHaveItemOnTop = false;
	public Item itemOnTop = null;

	public virtual void InteractButton(PlayerController player) {
	}

	public virtual void FoodButton(PlayerController player) {
		if (player.IsHoldingItem()) {
			player.DropItem();
		}
		else if (itemOnTop) {
			player.PickupItem(itemOnTop);
			itemOnTop = null;
		}
	}
}