using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace shared {
  [SelectionBase]
  public class SceneCamera : MonoBehaviour {
    public void zoom(string z) {
      GetComponent<Camera>().orthographicSize = float.Parse(z);
    }

    public void point_on(string target) {
      Camera c = GetComponent<Camera>();
      c.transform.position = Vector3.Scale(GameObject.Find(target).transform.position, new Vector3(1, 0, 1));
      c.transform.position += new Vector3(0, 20, 0);
    }
  }
}


