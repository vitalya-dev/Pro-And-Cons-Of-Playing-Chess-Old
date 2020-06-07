using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Player : MonoBehaviour {
  public float speed;

  [HideInInspector]
  public Vector3 axis = Vector3.zero;

  public GameObject[] particles;

  Vector3 backup_position;
  Quaternion backup_rotation;

  PhysicBody pb;
  Animator am;

  void Awake() {
    pb = GetComponent<PhysicBody>();
    am = GetComponent<Animator>();
    /* ================================================== */
    backup_position = transform.position;
    backup_rotation = transform.rotation;
  }

  void Update() {
    axis.x = Input.GetAxisRaw("Horizontal");
    axis.z = Input.GetAxisRaw("Vertical");
    
    GetComponent<PlayMakerFSM>().FsmVariables.GetFsmFloat("axis").RawValue = axis.magnitude;
    /* ===================================================== */
    if (Input.GetButtonDown("Fire1"))
      GetComponent<PlayMakerFSM>().SendEvent("FIRE1");
    if (Input.GetButtonDown("Fire2"))
      GetComponent<PlayMakerFSM>().SendEvent("FIRE2");
    if (Input.GetButtonDown("Fire3"))
      GetComponent<PlayMakerFSM>().SendEvent("FIRE3");
  }

  public IEnumerator idle_state() {
    /* ===================================================== */
    am.Play("Idle");
    /* ===================================================== */
    while (true) {
      /* ===================================================== */
      Vector3 c_p = GameObject.FindObjectOfType<Crosshair>().transform.position;
      Vector3 look = Vector3.Scale(c_p - transform.position, new Vector3(1, 0, 1));
      transform.rotation = Quaternion.LookRotation(look);
      /* ===================================================== */
      yield return null;
    }
  }

  public IEnumerator sleep_state() {
    am.Play("Sleep");
    /* ===================================================== */
    GetComponent<PhysicBody>().enabled = false;
    /* ===================================================== */
    transform.Translate(new Vector3(0, -1, 0));
    yield return null;
    /* ===================================================== */
    transform.rotation = Quaternion.LookRotation(Vector3.up, -1 * transform.forward);
    /* ===================================================== */
    while (true) {
      yield return null;
    }
  }

  public IEnumerator wakeup_state() {
    /* ===================================================== */
    am.Play("Wakeup");
    /* ===================================================== */
    transform.Translate(new Vector3(0, 0.5f, 0));
    yield return null;
    /* ===================================================== */
    transform.rotation = Quaternion.LookRotation(transform.right, transform.up);
    /* ===================================================== */
    while (true) {
      yield return null;
    }
  }

  public IEnumerator getup_state() {
    am.Play("Getup");
    /* ===================================================== */
    transform.rotation = Quaternion.LookRotation(transform.forward, Vector3.up);
    yield return null;
    /* ===================================================== */
    transform.Translate(new Vector3(-1, 1, 0));
    /* ===================================================== */
    GetComponent<PhysicBody>().enabled = true;
    /* ===================================================== */
    while (true) {
      yield return null;
    }
  }


  public IEnumerator walk_state() {
    /* ===================================================== */
    am.Play("Walk");
    /* ===================================================== */
    while (true) {
      Vector3 offset_x = Vector3.Scale(axis, new Vector3(1, 0, 0)) * speed * Time.deltaTime;
      Vector3 offset_z = Vector3.Scale(axis, new Vector3(0, 0, 1)) * speed * Time.deltaTime;
      pb.move_position(transform.position + offset_x);
      pb.move_position(transform.position + offset_z);
      /* ===================================================== */
      Vector3 c_p = GameObject.FindObjectOfType<Crosshair>().transform.position;
      Vector3 look = Vector3.Scale(c_p - transform.position, new Vector3(1, 0, 1));
      transform.rotation = Quaternion.LookRotation(look);
      /* ===================================================== */
      yield return null;
    }
  }

  public IEnumerator kick_state() {
    /* ===================================================== */
    am.Play("Kick");
    /* ===================================================== */
    Area body_area = GetComponent<Area>();
    Area leg_left_area = transform.Find("Leg Left").GetComponent<Area>();
    /* ===================================================== */
    if (leg_left_area.overlap<Door>())
      leg_left_area.overlap<Door>().open(transform.forward);
    /* ===================================================== */
    while (true) {
      yield return null;
    }
  }

  /* ============================================================================================ */
  public IEnumerator jab_state() {
    /* ===================================================== */
    am.Play("Jab");
    /* ===================================================== */
    while (true) {
      attack();
      yield return null;
    }
  }

  public IEnumerator right_state() {
    /* ===================================================== */
    am.Play("Right");
    /* ===================================================== */
    while (true) {
      attack();
      yield return null;
    }
  }

  public IEnumerator hook_state() {
    /* ===================================================== */
    am.Play("Hook");
    /* ===================================================== */
    while (true) {
      attack();
      yield return null;
    }
  }

  void attack() {
        
  }
  /* ============================================================================================ */

  public IEnumerator dead_state() {
    /* ===================================================== */
    am.Play("Dead");
    /* ===================================================== */
    foreach (var particle in particles) {
      GameObject.Instantiate(particle, transform.position + Vector3.up, Quaternion.identity);
    }
    gameObject.SetActive(false);
    /* ===================================================== */
    return null;
  }

  public IEnumerator knock_state() {
    /* ===================================================== */
    am.Play("Kick");
    /* ===================================================== */
    while (true) {
      yield return null;
    }
  }
}
