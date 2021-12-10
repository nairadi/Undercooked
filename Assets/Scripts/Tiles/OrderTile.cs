using UnityEngine;

public class OrderTile : Tile
{
	public GameObject recipeImageParent;
	public GameObject recipeImagePrefab;
	public Recipe recipe;
	public TextMesh pointsText;

	private float distanceBetweenImages = 0.3f;

	// Floating recipe image values
	private float speed = 2f;
	private float height = 0.005f;
	private bool movePointsText = false;
	private Vector3 startScalePointsText;

	public void Init() {
		GetNewRecipe();
		HidePointsText();
		startScalePointsText = pointsText.transform.localScale;
	}

	void Update() {
		Vector3 pos = recipeImageParent.transform.position;
		float newY = Mathf.Sin(Time.time * speed);
		recipeImageParent.transform.position = new Vector3(pos.x, newY * height + pos.y, pos.z);

		Vector3 pointTransformPositionVector = Vector3.zero;
		pointTransformPositionVector.y = 0.05f;

		if (movePointsText) {
			pointsText.transform.localPosition += pointTransformPositionVector;
			pointsText.color = pointsText.color - new Color(0, 0, 0, 0.025f);
		}
	}

	public override void FoodButton(PlayerController player) {
		if (recipe != null && player.IsHoldingItem()) {
			Food food = player.GetHeldFood();
			if (food != null) {
				int pointsGained;
				if (recipe.MatchesFoodStack(food.stack)) {
					pointsGained = recipe.GetPoints ();
				} else {
					pointsGained = -1;
				}
				movePointsText = true;
				pointsText.gameObject.SetActive (true);

				if (pointsGained > 0) {
					pointsText.text = "-" + pointsGained.ToString() + " enemy hp"; 
				} else {
					pointsText.text = pointsGained.ToString() + " self hp"; 
				}

				pointsText.transform.localScale = Vector2.one * 0.08f;

				// Reduce hp of other team, or our team if we fucked up
				PlayerTeam team = player.playerTeam;
				if (pointsGained < 0 && team == PlayerTeam.TeamLeft) {
					team = PlayerTeam.TeamRight;
					GameController.instance.AddPoints(team, -pointsGained);
				}
				else if (pointsGained < 0 && team == PlayerTeam.TeamRight) {
					team = PlayerTeam.TeamLeft;
					GameController.instance.AddPoints(team, -pointsGained);
				} else {
					GameController.instance.AddPoints(team, pointsGained);
				}

				player.DeleteHeldItem();
				this.ClearOutRecipeImages();
				this.recipe = null;

				Invoke("HidePointsText", 2f);
				Invoke("GetNewRecipe", Random.Range(3, 8));
			}
		}
	}

	public void GetNewRecipe() {
		this.recipe = RecipeController.instance.GetRandomRecipe();
		CreateRecipeImage();
		Debug.Log(recipe);
	}

	private void CreateRecipeImage() {
		ClearOutRecipeImages();

		float currentLoc = distanceBetweenImages;
		int sortingOrder = 1;
		foreach (FoodType foodType in this.recipe.ingredientList) {
			GameObject recipeImageGO = Instantiate(recipeImagePrefab, this.recipeImageParent.transform);
			recipeImageGO.transform.localPosition = new Vector3(currentLoc, 0, 0);
			currentLoc += distanceBetweenImages;

			SpriteRenderer spriteRenderer = recipeImageGO.GetComponent<SpriteRenderer>();
			spriteRenderer.sprite = FoodModel.instance.GetFoodTypeSprite(foodType);
			spriteRenderer.sortingOrder = sortingOrder;
			sortingOrder++;
		}
		GameObject bubbleImageGO = Instantiate(recipeImagePrefab, this.recipeImageParent.transform);
		SpriteRenderer spriteRend = bubbleImageGO.GetComponent<SpriteRenderer>();
		bubbleImageGO.transform.localPosition = new Vector3(currentLoc/2f, -0.15f, 0);
		spriteRend.sprite = FoodModel.instance.GetFoodTypeSprite(FoodType.Bubble);
		spriteRend.sortingOrder = 0;

		// Centre the images above the plate. -0.1f just cuz it seems to work out
		recipeImageParent.transform.localPosition = new Vector2((-distanceBetweenImages * this.recipe.ingredientList.Count / 2) - 0.1f, recipeImageParent.transform.localPosition.y);
	}

	private void ClearOutRecipeImages() {
		foreach (Transform child in recipeImageParent.transform) {
			Destroy(child.gameObject);
		}
	}

	private void HidePointsText() {
		pointsText.transform.localPosition = new Vector3(0, 1f, 0);
		pointsText.transform.localScale = startScalePointsText;
		movePointsText = false;
		pointsText.text = "";
		pointsText.color = new Color32(0xAB, 0x6E, 0x6E, 0xFF);
	}
}
