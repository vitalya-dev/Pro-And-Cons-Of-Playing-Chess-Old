using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour {
    [HideInInspector]
    public Area area;

    // Use this for initialization
    void Start() {
        if (transform.Find("Area"))
            area = transform.Find("Area").GetComponent<Area>();
        Enemy.kill_event.AddListener(enemy_killed);
    }

    void enemy_killed() {
        if (area && !area.overlap<Enemy>(LayerMask.GetMask("Top Layer"))) {
            GetComponent<PlayMakerFSM>().SendEvent("CLEAN");
        }
    }

    void burn() {
        Debug.Log("Burn It Down " + Time.time);
    }
}

