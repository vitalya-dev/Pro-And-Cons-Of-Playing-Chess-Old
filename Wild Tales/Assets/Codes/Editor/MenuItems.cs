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
}