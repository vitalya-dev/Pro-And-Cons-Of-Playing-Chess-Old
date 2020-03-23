﻿using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour {
    [HideInInspector]
    public Area area;

    // Use this for initialization
    void Start() {
        area = transform.Find("Area").GetComponent<Area>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (!area.overlap<Enemy>(LayerMask.GetMask("Top Layer"))) {
            GetComponent<PlayMakerFSM>().SendEvent("CLEAN");
        }
    }
}