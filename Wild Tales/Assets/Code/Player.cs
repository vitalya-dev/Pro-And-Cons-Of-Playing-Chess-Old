using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Player : MonoBehaviour {
  public float speed;

  private float time = 1;

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
      foreach (PlayMakerFSM fsm in GetComponents<PlayMakerFSM>()) fsm.SendEvent("FIRE1");
    if (Input.GetButtonDown("Fire2"))
      foreach (PlayMakerFSM fsm in GetComponents<PlayMakerFSM>()) fsm.SendEvent("FIRE2");
    if (Input.GetButtonDown("Fire3"))
      foreach (PlayMakerFSM fsm in GetComponents<PlayMakerFSM>()) fsm.SendEvent("FIRE3");
  }

  void wait() {
    am.Play("Idle");
  }


  void say(string text) {
    am.Play("Idle");
    /* ===================================================== */
    if (text == "#NOTHING") {
      foreach (var t_o in GameObject.FindObjectsOfType<TMPro.TextMeshPro>())
        if (t_o.name.StartsWith(GetInstanceID() + "_")) Destroy(t_o.gameObject);
      /* ===================================================== */
      GameObject.Find("Mist").GetComponent<SpriteRenderer>().enabled = false;
      /* ===================================================== */
      return;
    }
    /* ===================================================== */
    GameObject text_object = Instantiate(Resources.Load("Etc/Text", typeof(GameObject))) as GameObject;
    /* ===================================================== */
    text_object.GetComponent<TMPro.TextMeshPro>().text = text;
    /* ===================================================== */
    text_object.transform.rotation = Quaternion.LookRotation(Vector3.down, Vector3.forward);
    /* ===================================================== */
    float width = Mathf.Min(16, text.Length);
    float height = 0;
    /* ===================================================== */
    text_object.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
    height = text_object.GetComponent<TMPro.TextMeshPro>().GetPreferredValues().y;
    text_object.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
    /* ===================================================== */
    text_object.transform.position = Vector3.Scale(transform.position, new Vector3(1, 0, 1));
    text_object.transform.position += Vector3.up * (Camera.main.transform.position.y - 1); 
    /* ===================================================== */
    text_object.name = GetInstanceID() + "_" + text;
    /* ===================================================== */
    GameObject.Find("Mist").GetComponent<SpriteRenderer>().enabled = true;
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
    if (face_to_and_touch_to<Exit>()) {
      foreach (PlayMakerFSM fsm in GetComponents<PlayMakerFSM>()) fsm.SendEvent("EXIT");
    } else if (face_to_and_touch_to<Door>()) {
      foreach (PlayMakerFSM fsm in GetComponents<PlayMakerFSM>()) fsm.SendEvent("DOOR");
    } else if (face_to_and_touch_to<Bath>()) {
      foreach (PlayMakerFSM fsm in GetComponents<PlayMakerFSM>()) fsm.SendEvent("BATH");
    } else if (face_to_and_touch_to<Cupboard>()) {
      foreach (PlayMakerFSM fsm in GetComponents<PlayMakerFSM>()) fsm.SendEvent("CUPBOARD");
    } else if (face_to_and_touch_to<Phone>()) {
      foreach (PlayMakerFSM fsm in GetComponents<PlayMakerFSM>()) fsm.SendEvent("PHONE");
    }
    /* ===================================================== */
    while (true) {
      yield return null;
    }
  }

  void get_dressed() {
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
  }

  void open_door() {
    am.Play("Kick");
    face_to_and_touch_to<Door>().open();
  }

  void pickup_phone() {
    am.Play("Idle");
    face_to_and_touch_to<Phone>().pickup();
  }

  void hangup_phone() {
    am.Play("Idle");
    face_to_and_touch_to<Phone>().hangup();
  }

  void open_cupboard() {
    am.Play("Idle");
    face_to_and_touch_to<Cupboard>().open();
  }

  void change_graphic(string graphic) {
    string[] graphic_2 = graphic.Split(',');
    /* ===================================================== */
    GameObject body = transform.Find(graphic_2[0]).gameObject;
    Sprite sprite = Resources.Load<Sprite>("Graphics/Layers/" + graphic_2[1]);
    /* ===================================================== */
    body.GetComponent<SpriteRenderer>().sprite = sprite;
  }


  void rotate(string angles) {
    string[] angles_3 = angles.Split(',');
    /* ===================================================== */
    float x = float.Parse(angles_3[0]);
    float y = float.Parse(angles_3[1]);
    float z = float.Parse(angles_3[2]);
    /* ===================================================== */
    transform.rotation *= Quaternion.Euler(x, y, z);
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

  void fill_bath() {
    face_to_and_touch_to<Bath>().fill();
  }

  void drain_bath() {
    face_to_and_touch_to<Bath>().drain();
  }

  void take_bath_start() {
    am.Play("Idle");
    /* ===================================================== */
    backup_transform();
    /* ===================================================== */
    transform.position = face_to_and_touch_to<Bath>().transform.position;
    transform.rotation = Quaternion.LookRotation(Vector3.up, Vector3.right);
    transform.position += new Vector3(0, -0.475f, 0);
    /* ===================================================== */
    GetComponent<PhysicBody>().enabled = false;
  }

  void take_bath_finished() {
    am.Play("Idle");
    /* ===================================================== */
    restore_transform();
    /* ===================================================== */
    GetComponent<PhysicBody>().enabled = true;
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
    BoxCollider bc = GetComponent<BoxCollider>();
    /* ============================================ */
    Collider[] colliders = Physics.OverlapBox(transform.position + bc.center, bc.size / 2);
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
