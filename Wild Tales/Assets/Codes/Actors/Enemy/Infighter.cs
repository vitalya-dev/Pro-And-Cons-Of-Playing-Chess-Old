using UnityEngine;
using System.Collections;

public class Infighter : Enemy {
    [HideInInspector]
    public Area attack_area;

    override protected void Start() {
        base.Start();
        attack_area = transform.Find("Attack Area").GetComponent<Area>();
    }
}
