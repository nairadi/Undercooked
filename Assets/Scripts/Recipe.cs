using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Recipe
{
	public List<FoodType> ingredientList = new List<FoodType>();

	public Recipe(List<FoodType> ingredientList) {
		this.ingredientList = ingredientList;
	}

	public int GetPoints() {
		return this.ingredientList.Count;
	}

	public override string ToString() {
		string recipeStr = "";
		foreach (FoodType foodType in ingredientList) {
			recipeStr += foodType.ToString() + ", ";
		}
		return recipeStr;
	}

	public bool MatchesFoodStack(List<FoodType> foodStack) {
		return this.ingredientList.SequenceEqual(foodStack);
	}
}
