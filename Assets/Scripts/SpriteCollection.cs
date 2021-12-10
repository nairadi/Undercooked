using UnityEngine;
using System.Collections;

public class SpriteCollection
{
	private Sprite[] sprites;
	private string[] names;
	
	public SpriteCollection(string spritesheet)
	{
		sprites = Resources.LoadAll<Sprite>(spritesheet);
		names = new string[sprites.Length];
		
		for(var i = 0; i < names.Length; i++)
		{
			names[i] = sprites[i].name;
		}
	}
	
	public Sprite GetSprite(string name, Sprite currentSprite)
	{
		Sprite returnSprite = currentSprite;
		if (System.Array.IndexOf (names, name) != -1) {
			returnSprite = sprites [System.Array.IndexOf (names, name)];
		}
		return returnSprite;
	}
}
