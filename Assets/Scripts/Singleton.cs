using UnityEngine;
using System.Collections;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
	private static T _instance;
	public static T instance {
		get {
			if (_instance == null) {
				_instance = InstanceController.GetInstance(typeof(T)) as T;
			}
			return _instance;
		}
	}
}