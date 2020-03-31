using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {

    public void up() {
        GetComponent<Camera>().orthographicSize *= 2;
    }

    public void down() {
        GetComponent<Camera>().orthographicSize /= 2;
    }
}
