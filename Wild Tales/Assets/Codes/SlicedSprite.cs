using System.Collections;
using UnityEngine;

[ExecuteInEditMode]
public class SlicedSprite : MonoBehaviour {
	void Update() {
		GetComponent<SpriteRenderer>().size = new Vector2(transform.parent.localScale.x, transform.parent.localScale.z);
		transform.localScale = new Vector3(
			1 / transform.parent.localScale.x,
			1 / transform.parent.localScale.z,
			1
		);
	}
}