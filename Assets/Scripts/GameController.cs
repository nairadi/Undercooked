using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : Singleton<GameController> {
	public GameObject playerPrefab;

	public HPBar teamLeftHP;
	public HPBar teamRightHP;

	public PlayerController player1;
	public PlayerController player2;
	public PlayerTeam losingTeam;
	public AudioScript audioScript;
	void Awake() {
		// Hard code dat resolution lmao
		Screen.SetResolution(1920, 1080, true);
//		DontDestroyOnLoad(gameObject);
		audioScript.PlayMusicNormal ();
	}

	void Start(){
		CreatePlayers();
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			#if UNITY_EDITOR
			// Application.Quit() does not work in the editor so
			// UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
			UnityEditor.EditorApplication.isPlaying = false;
			#else
			Application.Quit();
			#endif
		}

		if (Input.GetKeyDown(KeyCode.Alpha5)) {
			Destroy(player1.gameObject);
			Destroy(player2.gameObject);
			SceneManager.LoadScene("PlayScene");
		}
			
	}

	private void CreatePlayers() {
		GameObject player1GO = Instantiate(playerPrefab, new Vector3(-1f, 2f, 0), Quaternion.identity);
		player1 = player1GO.GetComponent<PlayerController>();
		player1.Init("Player1", PlayerTeam.TeamLeft);

		GameObject player2GO = Instantiate(playerPrefab, new Vector3(1f, 2f, 0), Quaternion.identity);
		player2 = player2GO.GetComponent<PlayerController>();
		player2.Init("Player2", PlayerTeam.TeamRight);

	}

	// Adding points to a team reduces the health of the other team
	public void AddPoints(PlayerTeam team, int hp) {
		if (team == PlayerTeam.TeamLeft) {
			teamRightHP.ReduceHP(hp);
		} else {
			teamLeftHP.ReduceHP(hp);
		}
	}

	public void SwitchScene(string sceneName) {
		Destroy(player1.gameObject);
		Destroy(player2.gameObject);
		SceneManager.LoadScene(sceneName);
	}

	public void EndGame(PlayerTeam losingTeam) {
		this.losingTeam = losingTeam;
		SwitchScene("EndScene");
	}
}
