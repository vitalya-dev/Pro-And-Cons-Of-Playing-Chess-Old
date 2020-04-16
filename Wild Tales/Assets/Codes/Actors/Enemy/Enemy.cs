using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[SelectionBase]
public class Enemy : MonoBehaviour {
	public static UnityEvent kill_event = new UnityEvent();

	public float speed;

	[HideInInspector]
	public Player player;

	public int health;

	public GameObject[] particles;

	void Update() {
		if (transform.Find("Sight") && transform.Find("Sight").GetComponent<Sight>().see<Player>())
			Debug.Log("I See You");
	}

}