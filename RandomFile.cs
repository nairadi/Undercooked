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
		startScalePointsText = pointsText.transform.localScale;
	}

	void Update() {
		Vector3 pos = recipeImageParent.transform.position;
		float newY = Mathf.Sin(Time.time * speed);
		recipeImageParent.transform.position = new Vector3(pos.x, newY * height + pos.y, pos.z);

		Vector3 pointTransformPositionVector = Vector3.zero;
		pointTransformPositionVector.y = 0.02f;
		Vector3 pointTransformScaleVector = Vector2.one * 0.001f;
		if (movePointsText) {
			pointsText.transform.localPosition += pointTransformPositionVector;
			pointsText.transform.localScale += pointTransformScaleVector;
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
				player.points += pointsGained;
				movePointsText = true;
				pointsText.gameObject.SetActive (true);
				pointsText.text = pointsGained.ToString() + " hp"; 
				Debug.Log("Player has " + player.points + " points");
				player.DeleteHeldItem();
				this.ClearOutRecipeImages();
				this.recipe = null;

				Invoke("GetNewRecipe", Random.Range(3, 8));
			}
		}
	}

	public void GetNewRecipe() {
		this.recipe = RecipeController.instance.GetRandomRecipe();
		CreateRecipeImage();
		Debug.LogError ("Setting points to false");
		pointsText.transform.localPosition = Vector3.zero;
		pointsText.transform.localScale = startScalePointsText;
		movePointsText = false;
		pointsText.text = "";
		Debug.Log(recipe);
	}

	private void CreateRecipeImage() {
		ClearOutRecipeImages();

		float currentLoc = distanceBetweenImages;
		int sortingOrder = 1;
		GameObject bubbleImageGO = Instantiate(recipeImagePrefab, this.recipeImageParent.transform);
		bubbleImageGO.transform.localPosition = new Vector3(currentLoc, 0, 0);
		SpriteRenderer spriteRend = bubbleImageGO.GetComponent<SpriteRenderer>();
		spriteRend.sprite = FoodModel.instance.GetFoodTypeSprite(FoodType.Bubble);
		foreach (FoodType foodType in this.recipe.ingredientList) {
			GameObject recipeImageGO = Instantiate(recipeImagePrefab, this.recipeImageParent.transform);
			recipeImageGO.transform.localPosition = new Vector3(currentLoc, 0, 0);
			currentLoc += distanceBetweenImages;

			SpriteRenderer spriteRenderer = recipeImageGO.GetComponent<SpriteRenderer>();
			spriteRenderer.sprite = FoodModel.instance.GetFoodTypeSprite(foodType);
			spriteRenderer.sortingOrder = sortingOrder;
			sortingOrder++;
		}

		// Centre the images above the plate. -0.1f just cuz it seems to work out
		recipeImageParent.transform.localPosition = new Vector2((-distanceBetweenImages * this.recipe.ingredientList.Count / 2) - 0.1f, 1.15f);
	}

	private void ClearOutRecipeImages() {
		foreach (Transform child in recipeImageParent.transform) {
			Destroy(child.gameObject);
		}
	}
}
