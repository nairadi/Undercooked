using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GridController : Singleton<GridController> {
	public int width = 20;
	public int height = 12;
	public float cellWidth = 1f;
	public float cellHeight = 1f;

	public GameObject tileFloor;
	public GameObject tileWall;
	public GameObject tileFoodGenerator;
	public GameObject tileCuttingBoard;
	public GameObject tileStove;
	public GameObject tileEmptyStove;
	public GameObject tileCounterTop;
	public GameObject tileOrder;
	public GameObject tileGarbage;
	private Tile[,] tileArray;


	// Use this for initialization
	void Awake() {
		InstanceController.RegisterInstance(this);
	}

	void Start() {
		LoadMap("Text/map1.txt");
	}

	private void LoadMap(string path) {
		tileArray = new Tile[width, height];

		float halfWidth = width / 2f;
		float halfHeight = height / 2f;

		TextAsset textAsset = (TextAsset)Resources.Load("Text/map1");
		StringReader stringReader = new StringReader(textAsset.text);
		for (float y = halfHeight - 1f; y >= -halfHeight; y--) {
			char[] chars = stringReader.ReadLine().ToCharArray();
			for (float x = -halfWidth; x < halfWidth; x++) {

				// Choose which game object to instantiate depending on the location
				GameObject tile = GetTilePrefabFromChar(chars[(int)(x + halfWidth)]);
				if (tile == null) continue;
				tileArray[(int)(x + halfWidth), (int)(halfHeight - y - 1)] = tile.GetComponent<Tile>();

				// Set scale and position of each tile
				tile.transform.localScale = new Vector3(cellWidth, cellHeight, 0f);
				tile.transform.localPosition = new Vector3(x * cellWidth + cellWidth / 2f, y * cellHeight + cellHeight / 2f, 0f);
			}
		}
	}

	public GameObject GetTilePrefabFromChar(char c) {
		GameObject tile = null;
		switch(c) {
		case 'w': 
			tile = Instantiate(tileWall, transform); 
			break;
		case 'l': 
			tile = Instantiate(tileFoodGenerator, transform); 
			tile.GetComponent<FoodGeneratorTile>().Init(FoodType.Lettuce);
			break;
		case 't': 
			tile = Instantiate(tileFoodGenerator, transform); 
			tile.GetComponent<FoodGeneratorTile>().Init(FoodType.Tomato);
			break;
		case 'm': 
			tile = Instantiate(tileFoodGenerator, transform); 
			tile.GetComponent<FoodGeneratorTile>().Init(FoodType.Meat);
			break;
		case 'B': 
			tile = Instantiate(tileFoodGenerator, transform); 
			tile.GetComponent<FoodGeneratorTile>().Init(FoodType.TopBun);
			break;
		case 'b': 
			tile = Instantiate(tileFoodGenerator, transform); 
			tile.GetComponent<FoodGeneratorTile>().Init(FoodType.BottomBun);
			break;
		case 'c': 
			tile = Instantiate(tileCuttingBoard, transform); 
			break;
		case 's':
			tile = Instantiate (tileStove, transform);
			break;
		case 'e':
			tile = Instantiate (tileEmptyStove, transform);
			break;
		case 'p':
			tile = Instantiate (tileCounterTop, transform);
			break;
		case 'o':
			tile = Instantiate(tileOrder, transform);
			tile.GetComponent<OrderTile>().Init();
			break;
		case 'g':
			tile = Instantiate(tileGarbage, transform);
			tile.GetComponent<GarbageTile>().Init(GarbageTile.GarbageFaceDirection.Left);
			break;
		case 'G':
			tile = Instantiate (tileGarbage, transform);
			tile.GetComponent<GarbageTile>().Init(GarbageTile.GarbageFaceDirection.Right);
			break;
		case '-': 
			tile = Instantiate(tileFloor, transform); 
			break;
		}
		return tile;
	}

	public Vector2 GetGridLocation(Vector3 position) {
		Vector2 gridPosition = new Vector2(position.x + (width / 2f * cellWidth), -position.y + (height / 2f * cellHeight));
		int x = (int)(gridPosition.x / cellWidth);
		int y = (int)(gridPosition.y / cellHeight);
		return new Vector2(x, y);
	}
		
	public Tile GetTileAtCell(int x, int y) {
		if (x < 0 || x > width || y < 0 || y > height) return null;

		return tileArray[x, y];
	}

	public Tile GetTileInFrontOfPlayer(PlayerFaceDirection playerDirection, Vector3 playerPosition) {
		Vector2 gridIndices = GetGridLocation(playerPosition);
		Vector2 positionToCheck = gridIndices;

		if (playerDirection == PlayerFaceDirection.Up) {
			positionToCheck = new Vector2(positionToCheck.x, positionToCheck.y - 1);
		} else if (playerDirection == PlayerFaceDirection.Right) {
			positionToCheck = new Vector2(positionToCheck.x + 1, positionToCheck.y);
		} else if (playerDirection == PlayerFaceDirection.Down) {
			positionToCheck = new Vector2(positionToCheck.x, positionToCheck.y + 1);
		} else if (playerDirection == PlayerFaceDirection.Left) {
			positionToCheck = new Vector2(positionToCheck.x - 1, positionToCheck.y);
		}
		// Diagonal picking up if we want it who knows
//		else if (playerDirection == PlayerFaceDirection.UpRight) {
//			positionToCheck = new Vector2(positionToCheck.x + 1, positionToCheck.y - 1);
//		} else if (playerDirection == PlayerFaceDirection.DownRight) {
//			positionToCheck = new Vector2(positionToCheck.x + 1, positionToCheck.y + 1);
//		} else if (playerDirection == PlayerFaceDirection.DownLeft) {
//			positionToCheck = new Vector2(positionToCheck.x - 1, positionToCheck.y + 1);
//		} else if (playerDirection == PlayerFaceDirection.UpLeft) {
//			positionToCheck = new Vector2(positionToCheck.x - 1, positionToCheck.y - 1);
//		}

//		if (positionToCheck.x > tileArray.GetLength(0) || positionToCheck.y > tileArray.GetLength(1)) {
//			return null;
//		}
//
//		return tileArray[(int)positionToCheck.x, (int)positionToCheck.y];
		return GetTileAtCell((int)positionToCheck.x, (int)positionToCheck.y);
	}
}
