using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace scene_2 {
  public class Cord : MonoBehaviour {

    void Update() {
      LineRenderer lr = GetComponent<LineRenderer>();
      /* ===================================================== */
      Vector3 b = GameObject.Find("Player").transform.position;
      /* ===================================================== */
      lr.SetPosition(lr.positionCount - 1, b);
      /* ===================================================== */
      for (int i = lr.positionCount - 2; i > 0; i--) {
        Vector3 d = (lr.GetPosition(i + 1) - lr.GetPosition(i));
        d = Vector3.Scale(d, new Vector3(1, 0, 1));
        if (d.magnitude > 0.5f) {
          Vector3 new_pos = lr.GetPosition(i) + d * 5 * Time.deltaTime;
          foreach (var collider in Physics.OverlapBox(new_pos, new Vector3(0.1f, 0, 0.1f)))
            Debug.Log(collider.name);
          lr.SetPosition(i, new_pos);
        }
      }
    }
  }
}
