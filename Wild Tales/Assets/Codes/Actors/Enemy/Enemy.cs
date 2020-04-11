using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[SelectionBase]
public class Enemy : MonoBehaviour {
	public static UnityEvent kill_event = new UnityEvent();

	//public float speed;

	[HideInInspector]
	public Player player;

	[HideInInspector]
	public Eye eye;

	public int health;

	[Sirenix.OdinInspector.AssetsOnly]
	public GameObject[] particles;

	/* ================================ */
	void Start() {
		//eye = transform.Find("Eye").GetComponent<Eye>();
	}
	/* ================================ */

	/* ================================ */
	public void hit(Vector2 direction) {
		foreach (var particle in particles) {
			GameObject.Instantiate(particle, transform.position + Vector3.back * 0.5f, Quaternion.identity);
		}
		health -= 1;
		if (health <= 0) {
			GameObject.Destroy(this.gameObject);
			kill_event.Invoke();
		} else
			GetComponent<Animator>().SetTrigger("hurt");
	}
	/* ================================ */

	/* ================================ */
	public void stun() {
		GetComponent<Animator>().SetTrigger("stun");
	}
	/* ================================ */

	/* ================================ */
	public void look_at(Vector2 direction) {
		transform.rotation = Quaternion.LookRotation(Vector3.forward, -1 * direction.normalized);
	}
	/* ================================ */

	/* ================================ */
	void Update() {}
	/* ================================ */

}