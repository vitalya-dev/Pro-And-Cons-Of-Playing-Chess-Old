using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace shared {
  [SelectionBase]
  public class SceneCamera : MonoBehaviour {
    public Transform follow_target;
    Vector3 camera_velocity;

    void Update() {
      if (follow_target) {
        Vector3 dest = Vector3.Scale(follow_target.position, new Vector3(1, 0, 1)) + new Vector3(0, 20, 0);
        transform.position = Vector3.SmoothDamp(transform.position, dest, ref camera_velocity, 0.25f);
      }
    }

    public void zoom(string z) {
      GetComponent<Camera>().orthographicSize = float.Parse(z);
    }

    public void point_on(string target) {
      Camera c = GetComponent<Camera>();
      c.transform.position = Vector3.Scale(GameObject.Find(target).transform.position, new Vector3(1, 0, 1));
      c.transform.position += new Vector3(0, 20, 0);
    }
    
    public void point_on(GameObject target) {
      point_on(target.name);
    }


    public void follow(string target) {
      follow_target = target == "" ? null : GameObject.Find(target).transform;
    }

    public void follow(GameObject target) {
      follow(target.name);
    }
  }
}


