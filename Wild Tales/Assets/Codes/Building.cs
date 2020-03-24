using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour {
    [HideInInspector]
    public Area area;

    // Use this for initialization
    void Start() {
        area = transform.Find("Area").GetComponent<Area>();
        Enemy.kill_event.AddListener(enemy_killed);
    }

    public void enemy_killed() {
        if (!area.overlap<Enemy>(LayerMask.GetMask("Top Layer"))) {
            GetComponent<PlayMakerFSM>().SendEvent("CLEAN");
        }
    }
}
