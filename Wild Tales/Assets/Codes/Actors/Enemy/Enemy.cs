using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[SelectionBase]
public class Enemy : MonoBehaviour {
	public static UnityEvent kill_event = new UnityEvent();

	public float speed;

	[HideInInspector]
	public Player player;

	public int health;

	public GameObject[] particles;

	NavMeshPath path;

	void Awake() {
		path = new NavMeshPath();
	}


	public NavMeshPath calculate_path(Vector3 source, Vector3 target) {
		NavMesh.CalculatePath(source, target, NavMesh.AllAreas, path);
		return path;
	}

}