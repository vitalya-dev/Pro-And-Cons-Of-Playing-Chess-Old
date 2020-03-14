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

    [MenuItem("Tools/Snap")]
    static void snap() {
        foreach (var go in Selection.gameObjects) {
            go.transform.localPosition = new Vector3((int)go.transform.localPosition.x, (int)go.transform.localPosition.y, (int)go.transform.localPosition.z);
        }
    }
}