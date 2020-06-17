using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Player : MonoBehaviour {
  public float speed;

  private float time = 0;

  [HideInInspector]
  public Vector3 axis = Vector3.zero;

  PhysicBody pb;
  Animator am;

  void Awake() {
    pb = GetComponent<PhysicBody>();
    am = GetComponent<Animator>();
  }

  void Update() {
    GetComponent<PlayMakerFSM>().FsmVariables.GetFsmFloat("time").RawValue = time;
    /* ===================================================== */
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

  public void say(string text) {
    if (text == "#NOTHING") {
      foreach (var t_o in GameObject.FindObjectsOfType<TMPro.TextMeshPro>())
        if (t_o.name.StartsWith(GetInstanceID() + "_")) Destroy(t_o.gameObject);
      return;
    }
    /* ===================================================== */
    GameObject text_object = Instantiate(Resources.Load("Etc/Text", typeof(GameObject))) as GameObject;
    /* ===================================================== */
    string[] lines = text.Split(char.Parse("#"));
    text_object.GetComponent<RectTransform>().sizeDelta = new Vector2(lines[0].Length * 0.5f, lines.Length);
    /* ===================================================== */
    text_object.GetComponent<TMPro.TextMeshPro>().text = text.Replace("#", "\n");
    text_object.transform.rotation = Quaternion.LookRotation(Vector3.down, Vector3.forward);
    /* ===================================================== */
    text_object.transform.position = Vector3.Scale(transform.position, new Vector3(1, 0, 1));
    text_object.transform.position += Vector3.up * (Camera.main.transform.position.y - 1); 
    /* ===================================================== */
    text_object.name = GetInstanceID() + "_" + text;
    /* ===================================================== */
    Debug.Log(text);
  }

  public void time_inc() {
    time += 1;
  }


  public IEnumerator wait_state() {
    while (true) {
      yield return null;
    }
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
    transform.position += new Vector3(0, 10, 0);
    yield return null;
    /* ===================================================== */
    transform.rotation = Quaternion.LookRotation(Vector3.up, Vector3.forward);
    /* ===================================================== */
    GameObject.Find("Starry Night").GetComponent<SpriteRenderer>().enabled = true;
    /* ===================================================== */
    while (true) {
      yield return null;
    }
  }

  public IEnumerator wakeup_state() {
    /* ===================================================== */
    am.Play("Wakeup");
    /* ===================================================== */
    transform.rotation = Quaternion.LookRotation(Vector3.right, Vector3.forward);
    GameObject.Find("Peace Of Mind").GetComponent<AudioSource>().Play();
    /* ===================================================== */
    while (true) {
      yield return null;
    }
  }


  public IEnumerator situp_state() {
    /* ===================================================== */
    am.Play("Situp");
    /* ===================================================== */
    transform.position += new Vector3(0, -10, 0.5f);
    yield return null;
    /* ===================================================== */
    transform.rotation = Quaternion.LookRotation(-1 * Vector3.right, Vector3.forward);
    /* ===================================================== */
    GameObject.Find("Starry Night").GetComponent<SpriteRenderer>().enabled = false;
    Camera.main.orthographicSize = 6.5f;
    yield return null;
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
    transform.position += new Vector3(2, 0, 0);
    yield return null;
    /* ===================================================== */
    GetComponent<PhysicBody>().enabled = true;
    /* ===================================================== */
    GameObject.Find("Stop The Music").GetComponent<AudioSource>().Play();
    GameObject.Find("Peace Of Mind").GetComponent<AudioSource>().Stop();
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

  public IEnumerator use_state() {
    am.Play("Use");
    /* ===================================================== */
    if (face_to_and_touch_to<Door>()) {
      GetComponent<PlayMakerFSM>().SendEvent("DOOR");
    } else if (face_to_and_touch_to<Exit>()) {
      GetComponent<PlayMakerFSM>().SendEvent("EXIT");
    } else if (face_to_and_touch_to<Bath>()) {
      GetComponent<PlayMakerFSM>().SendEvent("BATH");
    } else if (face_to_and_touch_to<Cupboard>()) {
      GetComponent<PlayMakerFSM>().SendEvent("CUPBOARD");
    }
    /* ===================================================== */
    while (true) {
      yield return null;
    }
  }
  
  public IEnumerator use_cupboard_state() {
    am.Play("Idle");
    /* ===================================================== */
    Cupboard cupboard = face_to_and_touch_to<Cupboard>();
    /* ===================================================== */
    Sprite old_player_shoulder = transform.Find("Shoulder Right").GetComponent<SpriteRenderer>().sprite;
    Sprite old_player_leg = transform.Find("Leg Right").GetComponent<SpriteRenderer>().sprite;
    Sprite old_player_body = transform.Find("Body 1").GetComponent<SpriteRenderer>().sprite;
    /* ===================================================== */
    transform.Find("Shoulder Right").GetComponent<SpriteRenderer>().sprite = cupboard.shoulder_sprite;
    transform.Find("Shoulder Left").GetComponent<SpriteRenderer>().sprite = cupboard.shoulder_sprite;
    transform.Find("Leg Right").GetComponent<SpriteRenderer>().sprite = cupboard.leg_sprite;
    transform.Find("Leg Left").GetComponent<SpriteRenderer>().sprite = cupboard.leg_sprite;
    transform.Find("Body 1").GetComponent<SpriteRenderer>().sprite = cupboard.body_sprite;
    transform.Find("Body 2").GetComponent<SpriteRenderer>().sprite = cupboard.body_sprite;
    transform.Find("Body 3").GetComponent<SpriteRenderer>().sprite = cupboard.body_sprite;
    /* ===================================================== */
    cupboard.shoulder_sprite = old_player_shoulder;
    cupboard.leg_sprite = old_player_leg;
    cupboard.body_sprite = old_player_body;
    /* ===================================================== */
    while (true) {
      transform.rotation *= Quaternion.Euler(0, 45, 0);
      yield return null;
    }
  }

  public IEnumerator use_door_state() {
    am.Play("Kick");
    /* ===================================================== */
    face_to_and_touch_to<Door>().open();
    /* ===================================================== */
    while (true) {
      yield return null;
    }
  }

  public IEnumerator use_exit_state() {
    am.Play("Kick");
    /* ===================================================== */
    face_to_and_touch_to<Exit>().open();
    /* ===================================================== */
    while (true) {
      yield return null;
    }
  }

  public IEnumerator use_bath_state_1() {
    am.Play("Idle");
    /* ===================================================== */
    GetComponent<PhysicBody>().enabled = false;
    /* ===================================================== */
    Bath bath = face_to_and_touch_to<Bath>();
    bath.fill_it();
    /* ===================================================== */
    backup_transform();
    /* ===================================================== */
    transform.rotation = Quaternion.LookRotation(Vector3.up, Vector3.right);
    /* ===================================================== */
    transform.position = bath.transform.position;
    transform.position += new Vector3(0, -0.475f, 0);
    /* ===================================================== */
    while (true) {
      yield return null;
    }
  }

  public IEnumerator use_bath_state_2() {
    am.Play("Idle");
    /* ===================================================== */
    GetComponent<PhysicBody>().enabled = true;
    /* ===================================================== */
    restore_transform();
    /* ===================================================== */
    yield return null;
    /* ===================================================== */
    face_to_and_touch_to<Bath>().drain_it();
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
    return null;
  }

  public IEnumerator knock_state() {
    am.Play("Kick");
    /* ===================================================== */
    while (true) {
      yield return null;
    }
  }

  T seen<T>() where T : MonoBehaviour {
    RaycastHit hit;
    if (Physics.Raycast(transform.position, transform.forward, out hit, 2) && hit.collider.GetComponent<T>())
      return hit.collider.GetComponent<T>();
    return null;
  }
    
  
  T face_to_and_touch_to<T>() where T : MonoBehaviour {
    Collider[] colliders = Physics.OverlapBox(
                                              transform.position + GetComponent<BoxCollider>().center,
                                              Vector3.Scale(GetComponent<BoxCollider>().size / 2, new Vector3(1f, 1f, 1f))
                                              );
    /* ============================================ */
    foreach (var collider in colliders)
      if (collider.gameObject != gameObject && collider.GetComponent<T>() != null) {
        if ((Vector3.Angle(transform.forward, collider.bounds.center - transform.position)) < 60)
          return collider.GetComponent<T>();
      }
    /* ============================================ */
    return null;
  }


  /* ============================================ */
  Vector3 backup_position;
  Quaternion  backup_rotation;

  void backup_transform() {
    backup_position = transform.position;
    backup_rotation = transform.rotation;
  }

  void restore_transform() {
    transform.position = backup_position;
    transform.rotation = backup_rotation;
  }
  /* ============================================ */

}
