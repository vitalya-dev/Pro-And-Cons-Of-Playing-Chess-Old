using UnityEngine;
using UnityEditor;

public class MenuItems {
    [MenuItem("Tools/Add Obstacle")]
    static void add_obstacle() {
        foreach (var go in Selection.gameObjects) {
            GameObject obstacle = new GameObject("Obstacle");
            obstacle.transform.parent = go.transform;
        }
    }

    [MenuItem("Tools/Snap &q")]
    static void snap() {
        foreach (var go in Selection.gameObjects) {
            go.transform.localPosition = new Vector3((int)go.transform.localPosition.x, (int)go.transform.localPosition.y, (int)go.transform.localPosition.z);
            go.transform.localScale = new Vector3(Mathf.RoundToInt(go.transform.localScale.x), Mathf.RoundToInt(go.transform.localScale.y), Mathf.RoundToInt(go.transform.localScale.z));
        }
    }

    [MenuItem("Tools/Select parent &w")]
    static void select_parent() {
        Selection.activeGameObject = Selection.activeGameObject.transform.parent.gameObject;

    }
}

