using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour {
	public T see<T>() where T : MonoBehaviour {
		Debug.Log("Try To See");
		if (GetComponent<Area>().overlap<T>()) 
			Debug.Log("Overlap " + typeof(T));
		return null;
	}
}