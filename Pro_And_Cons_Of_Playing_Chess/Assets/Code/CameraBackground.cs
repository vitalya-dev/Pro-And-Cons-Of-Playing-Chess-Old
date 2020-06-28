using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class CameraBackground : MonoBehaviour {
  void Update() {
    float height = 2 * Camera.main.orthographicSize;
    float width = height * Camera.main.aspect;
    transform.localScale = new Vector3(width + 0.1f, height + 0.1f, 1);
  }
}
