using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants{
	public static Color32 bunRaw = new Color32(178, 131, 28, 255);
	public static Color32 meatRaw = new Color32(178, 60, 28, 255);
	public static Color32 meatCooked = new Color32(99, 54, 36, 255);
	public static Color32 lettuceRaw = new Color32(53, 99, 36, 255);
	public static Color32 lettuceChopped = new Color32(79, 214, 29, 255);
	public static Color32 tomatoRaw = new Color32(214, 29, 29, 255);
	public static Color32 tomatoChopped = new Color32(255, 5, 5, 255);
}

public enum PlayerTeam {
	None = 0,
	TeamLeft,
	TeamRight
}

public enum PlayerFaceDirection{
	None = 0,
	Up,
	Down,
	Left,
	Right,
	UpRight,
	UpLeft,
	DownRight,
	DownLeft
}

public enum PlayerHandPosition{
	None = 0,
	Free,
	Holding
}

public enum PlayerWalkType{
	None = 0,
	Walk1,
	Walk2
}

public enum FoodType {
	None,
	TopBun,
	BottomBun,
	Meat,
	Lettuce,
	Tomato,
	Bubble
}

public enum FoodState {
	None,
	Raw,
	Chopped,
	Cooked,
	Burned
}

public enum MusicState{
	None,
	Normal,
	Slow,
	SuperSlow,
	EndGame
}