using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeController : Singleton<RecipeController>
{
	public static List<Recipe> recipes = new List<Recipe>() {
		new Recipe(new List<FoodType>{ FoodType.Lettuce }),
		new Recipe(new List<FoodType>{ FoodType.Lettuce, FoodType.Tomato }),
		new Recipe(new List<FoodType>{ FoodType.BottomBun, FoodType.Lettuce, FoodType.Tomato, FoodType.Lettuce, FoodType.TopBun }),
		new Recipe(new List<FoodType>{ FoodType.BottomBun, FoodType.Meat, FoodType.TopBun }),
		new Recipe(new List<FoodType>{ FoodType.BottomBun, FoodType.Meat, FoodType.Lettuce, FoodType.TopBun }),
		new Recipe(new List<FoodType>{ FoodType.BottomBun, FoodType.Meat, FoodType.Tomato, FoodType.TopBun }),
		new Recipe(new List<FoodType>{ FoodType.BottomBun, FoodType.Meat, FoodType.Lettuce, FoodType.Tomato, FoodType.TopBun }),
		new Recipe(new List<FoodType>{ FoodType.BottomBun, FoodType.Meat, FoodType.Tomato, FoodType.Lettuce, FoodType.TopBun }),
		new Recipe(new List<FoodType>{ FoodType.BottomBun, FoodType.Meat, FoodType.BottomBun, FoodType.Meat, FoodType.TopBun }),
		new Recipe(new List<FoodType>{ FoodType.BottomBun, FoodType.Meat, FoodType.Lettuce, FoodType.Meat, FoodType.TopBun }),
		new Recipe(new List<FoodType>{ FoodType.BottomBun, FoodType.Lettuce, FoodType.Meat, FoodType.Lettuce, FoodType.TopBun })
	};

	public Recipe GetRandomRecipe() {
		int index = Random.Range(0, recipes.Count);
		return recipes[index];
	}
}
