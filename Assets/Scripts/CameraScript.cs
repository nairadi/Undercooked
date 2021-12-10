using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : Singleton<CameraScript> {
	private Camera camera;

	void Awake() {
		camera = GetComponent<Camera>();
	}

	public Vector2 GetScreenDimensions() {
		float height = camera.orthographicSize * 2f;
		float width = height * camera.aspect;
		return new Vector2(width, height);
	}
}
