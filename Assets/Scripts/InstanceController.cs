using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class InstanceController : MonoBehaviour 
{
	private static Dictionary<Type, object> allInstances = new Dictionary<Type, object>();

	public static object GetInstance (Type instanceType)
	{
		object result = null;

		// Get cached reference
		if (allInstances.ContainsKey(instanceType)) {
			result = allInstances[instanceType];
		}

		// Update reference from scene, if necessary
		if (result == null || result.Equals(null)) {
			UnityEngine.Object obj = GameObject.FindObjectOfType(instanceType);
			if (obj != null) {
				result = obj;
				allInstances[instanceType] = result;
			}
		}
		return result;
	}

	public static void RegisterInstance (object instance)
	{
		allInstances[instance.GetType()] = instance;
	}
}
