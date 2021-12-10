using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float speed = 5f;
	public SpriteRenderer spriteRenderer;

	SpriteCollection playerSprites;

	private Rigidbody2D rigidbody2D;
	public PlayerTeam playerTeam;
	public PlayerFaceDirection playerDirection;
	public PlayerHandPosition playerHandPosition;
	public PlayerWalkType playerWalkType;

	private Item heldItem;
	private float holdDistance = 0.75f;
	private float heldItemZ = -0.1f;
	public float stackOffset = 0.3f;

	private string horizontalInput = "";
	private string verticalInput = "";
	private string interactInput = "";
	private string itemInput = "";
	private string playerName = "";

	public void Init(string playerName, PlayerTeam playerTeam){
		this.horizontalInput = "Horizontal" + playerName;
		this.verticalInput = "Vertical" + playerName;
		this.interactInput = "Interact" + playerName;
		this.itemInput = "Item" + playerName;
		this.playerName = playerName;
		this.playerTeam = playerTeam;


		rigidbody2D = GetComponent<Rigidbody2D>();
		playerDirection = PlayerFaceDirection.Up;
		playerHandPosition = PlayerHandPosition.Free;
		playerWalkType = PlayerWalkType.Walk1;

		// Load player character sprites 
		playerSprites = new SpriteCollection("Sprites/PlayerSprites");
		SetSprite ();
	}

	// Update is called once per frame
	void Update() {
		float h = Input.GetAxisRaw(horizontalInput);

		float v = Input.GetAxisRaw(verticalInput);

		// if there is movement, change the walk type
		if (h != 0 || v != 0) {
			ToggleWalkState ();
		}

		Vector3 speedVector = Vector3.zero;
		if (h > 0) {
			speedVector.x += speed;
			playerDirection = PlayerFaceDirection.Right;
			SetSprite ();
		}
		else if (h < 0) {
			speedVector.x -= speed;
			playerDirection = PlayerFaceDirection.Left;
			SetSprite ();
		}

		if (v > 0) {
			speedVector.y += speed;
			playerDirection = PlayerFaceDirection.Up;
			SetSprite ();
		}
		else if (v < 0) {
			speedVector.y -= speed;
			playerDirection = PlayerFaceDirection.Down;
			SetSprite ();
		}

		if (h > 0 && v > 0) {
			playerDirection = PlayerFaceDirection.UpRight;
			SetSprite ();
		} else if (h > 0 && v < 0) {
			playerDirection = PlayerFaceDirection.DownRight;
			SetSprite ();
		} else if (h < 0 && v > 0) {
			playerDirection = PlayerFaceDirection.UpLeft;
			SetSprite ();
		} else if (h < 0 && v < 0) {
			playerDirection = PlayerFaceDirection.DownLeft;
			SetSprite ();
		}

		rigidbody2D.velocity = speedVector;
		SetItemRotation();

		if (Input.GetButtonDown(this.itemInput)) {
			Tile tileObject = GridController.instance.GetTileInFrontOfPlayer(playerDirection, this.transform.position);
			if (tileObject != null) {
				tileObject.FoodButton(this);
			}
		}
		else if (Input.GetButton(this.interactInput)) {
			Tile tileObject = GridController.instance.GetTileInFrontOfPlayer(playerDirection, this.transform.position);
			if (tileObject != null) {
				tileObject.InteractButton(this);
			}
		}
	}

	public void PickupItem(Item item) {
		if (heldItem == null) {
			heldItem = item;

			item.gameObject.transform.parent = this.transform;
			item.gameObject.GetComponent<BoxCollider2D>().enabled = false;
			SetItemRotation();
			playerHandPosition = PlayerHandPosition.Holding;

			item.OnPickup();
		} else {
			Debug.LogError("Already holding an item");
		}
	}

	public void DropItem() {
		if (IsHoldingItem()) {
			Tile tileObject = GridController.instance.GetTileInFrontOfPlayer(playerDirection, this.transform.position);
			if (tileObject != null) {
				if (tileObject.canHaveItemOnTop) {
					if (tileObject.itemOnTop == null) {
						heldItem.gameObject.transform.parent = tileObject.transform;
						heldItem.gameObject.GetComponent<BoxCollider2D>().enabled = true;
						heldItem.gameObject.transform.localPosition = new Vector2(0, 0);
						tileObject.itemOnTop = heldItem;
						heldItem.OnDrop();
						heldItem = null;
						playerHandPosition = PlayerHandPosition.Free;
					} else {
						// Try to combine items
						Food foodOnTop = tileObject.itemOnTop.GetComponent<Food>();
						Food myFood = GetHeldFood();

						if (foodOnTop.CanCombine() && myFood.CanCombine()) {
							foreach(FoodType type in myFood.stack) {
								foodOnTop.stack.Add(type);
							}
							foodOnTop.PrintStack();

							myFood.stackBottom.transform.parent = foodOnTop.stackTop.transform;
							myFood.stackBottom.transform.localPosition = new Vector3(0, stackOffset, -0.1f);
							foodOnTop.stackTop = myFood.stackTop;

							int order = 5;
							foreach(SpriteRenderer spriteRenderer in foodOnTop.stackBottom.GetComponentsInChildren<SpriteRenderer>()) {
								spriteRenderer.sortingOrder = order;
								order++;
							}
							DeleteHeldItem();
						}
					}
				}
			}
		}
	}

	private void SetItemRotation() {
		if (heldItem != null) {
			float diagDist = holdDistance / 2;

			if (playerDirection == PlayerFaceDirection.Up) {
				heldItem.transform.localPosition = new Vector3(0, holdDistance, heldItemZ);
			} else if (playerDirection == PlayerFaceDirection.Right) {
				heldItem.transform.localPosition = new Vector3(holdDistance, 0, heldItemZ);
			} else if (playerDirection == PlayerFaceDirection.Down) {
				heldItem.transform.localPosition = new Vector3(0, -holdDistance, heldItemZ);
			} else if (playerDirection == PlayerFaceDirection.Left) {
				heldItem.transform.localPosition = new Vector3(-holdDistance, 0, heldItemZ);
			} else if (playerDirection == PlayerFaceDirection.UpRight) {
				heldItem.transform.localPosition = new Vector3(diagDist, diagDist, heldItemZ);
			} else if (playerDirection == PlayerFaceDirection.DownRight) {
				heldItem.transform.localPosition = new Vector3(diagDist, -diagDist, heldItemZ);
			} else if (playerDirection == PlayerFaceDirection.DownLeft) {
				heldItem.transform.localPosition = new Vector3(-diagDist, -diagDist, heldItemZ);
			} else if (playerDirection == PlayerFaceDirection.UpLeft) {
				heldItem.transform.localPosition = new Vector3(-diagDist, diagDist, heldItemZ);
			}
		}
	}

	void SetSprite(){
		spriteRenderer.sprite = playerSprites.GetSprite (playerName + playerDirection.ToString () + playerWalkType.ToString() + playerHandPosition.ToString() + "Sprite", spriteRenderer.sprite);
	}

	float myTime = 0.0f;
	float nextPressTime = 0.3f;

	// Toggle the walk animation by switching walk state
	void ToggleWalkState(){
		myTime = myTime + Time.deltaTime;
		if (myTime > nextPressTime) {
			myTime = 0.0f;

			if(playerWalkType == PlayerWalkType.Walk1){
				playerWalkType = PlayerWalkType.Walk2;
			} else{
				playerWalkType = PlayerWalkType.Walk1;
			}
		}
	}

	public bool IsHoldingItem() {
		return heldItem != null;
	}

	public Food GetHeldFood() {
		if (IsHoldingItem()) {
			return heldItem.GetComponent<Food>();
		}

		return null;
	}

	public void DeleteHeldItem() {
		if (IsHoldingItem()) {
			Destroy(heldItem.gameObject);
			heldItem = null;
		}
	}
}
