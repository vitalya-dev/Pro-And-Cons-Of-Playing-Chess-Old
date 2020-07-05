using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace scene_2 {
  public class Cord : MonoBehaviour {
    NavMeshPath path;

    void Start() {
      path = new NavMeshPath();
    }

    void Update() {
      Vector3 a = GetComponent<LineRenderer>().GetPosition(0);
      Vector3 b = GameObject.Find("Player").transform.position +  -1 * GameObject.Find("Player").transform.right;
      if (NavMesh.CalculatePath(a, b, NavMesh.AllAreas, path)) {
        GetComponent<LineRenderer>().positionCount = path.corners.Length;
        GetComponent<LineRenderer>().SetPositions(path.corners);
      }
    }
  }
}
