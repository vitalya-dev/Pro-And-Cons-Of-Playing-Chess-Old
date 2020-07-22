using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace scene_2 {
  [SelectionBase]
  public class Player : MonoBehaviour {
    public float speed;
    /* ===================================================== */
    private float time;
    /* ===================================================== */
    [HideInInspector]
    public Vector3 axis = Vector3.zero;
    /* ===================================================== */
    PhysicBody pb;
    Animator am;

    void Awake() {
      pb = GetComponent<PhysicBody>();
      am = GetComponent<Animator>();
    }


    void Start() {
      GetComponent<MeshRenderer>().enabled = false;
      time = 0;
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
      /* ===================================================== */
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
        GameObject.Find("Pink Mist").GetComponent<SpriteRenderer>().enabled = false;
        /* ===================================================== */
        return;
      }
      /* ===================================================== */
      GameObject text_object = Instantiate(Resources.Load("Etc/GameText", typeof(GameObject))) as GameObject;
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
      GameObject.Find("Pink Mist").GetComponent<SpriteRenderer>().enabled = true;
    }

    public void time_inc() {
      time += 1;
    }


    void idle() {
      am.Play("Idle");
      /* ===================================================== */
      Vector3 c_p = GameObject.FindObjectOfType<Crosshair>().transform.position;
      Vector3 look = Vector3.Scale(c_p - transform.position, new Vector3(1, 0, 1));
      transform.rotation = Quaternion.LookRotation(look);
    }

    void walk() {
      am.Play("Walk");
      /* ===================================================== */
      Vector3 offset_x = Vector3.Scale(axis, new Vector3(1, 0, 0)) * speed * Time.deltaTime;
      Vector3 offset_z = Vector3.Scale(axis, new Vector3(0, 0, 1)) * speed * Time.deltaTime;
      pb.move_position(transform.position + offset_x);
      pb.move_position(transform.position + offset_z);
      /* ===================================================== */
      Vector3 c_p = GameObject.FindObjectOfType<Crosshair>().transform.position;
      Vector3 look = Vector3.Scale(c_p - transform.position, new Vector3(1, 0, 1));
      transform.rotation = Quaternion.LookRotation(look);
    }

    void interact() {
      am.Play("Interact");
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
      } else if (face_to_and_touch_to<Chess>()) {
        foreach (PlayMakerFSM fsm in GetComponents<PlayMakerFSM>()) fsm.SendEvent("CHESS");
      } else if (face_to_and_touch_to<Tv>()) {
        foreach (PlayMakerFSM fsm in GetComponents<PlayMakerFSM>()) fsm.SendEvent("TV");
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

    void sit_at_chair_1() {
      am.Play("Idle");
      /* ===================================================== */
      transform.position = GameObject.FindObjectOfType<Chair>().transform.position;
      transform.rotation = Quaternion.LookRotation(Vector3.right);    
      /* ===================================================== */
      GetComponent<PhysicBody>().enabled = false;
    }

    void sit_at_chair_2() {
      am.Play("Idle");
      /* ===================================================== */
      transform.position += 2 * Vector3.left;
      transform.position = Vector3.Scale(transform.position, new Vector3(1, 0, 1)) + new Vector3(0, 2.02f, 0);
      /* ===================================================== */
      GetComponent<PhysicBody>().enabled = true;
    }

    void sit_at_couch_1() {
      am.Play("Idle");
      /* ===================================================== */
      backup_transform();
      /* ===================================================== */
      transform.position = GameObject.FindObjectOfType<Couch>().transform.position;
      transform.rotation = Quaternion.LookRotation(Vector3.right);    
      /* ===================================================== */
      GetComponent<PhysicBody>().enabled = false;
    }

    void sit_at_couch_2() {
      am.Play("Idle");
      /* ===================================================== */
      transform.position += 3 * Vector3.forward;
      transform.position = Vector3.Scale(transform.position, new Vector3(1, 0, 1)) + new Vector3(0, 2.02f, 0);
      /* ===================================================== */
      GetComponent<PhysicBody>().enabled = true;
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


    void fill_bath() {
      face_to_and_touch_to<Bath>().fill();
    }

    void drain_bath() {
      face_to_and_touch_to<Bath>().drain();
    }

    void pass_out() {
      GameObject.Find("Black").GetComponent<SpriteRenderer>().enabled = true;
      GameObject.Find("Man In Me").GetComponent<AudioSource>().Play();
    }


    void take_bath_1() {
      am.Play("Idle");
      /* ===================================================== */
      backup_transform();
      /* ===================================================== */
      transform.position = face_to_and_touch_to<Bath>().transform.position;
      transform.rotation = Quaternion.LookRotation(Vector3.up, Vector3.right);
      transform.position += new Vector3(0, -0.475f, 0);
      /* ===================================================== */
      GetComponent<PhysicBody>().enabled = false;
      /* ===================================================== */
      GameObject.Find("Whistling").GetComponent<AudioSource>().Play();
    }

    void take_bath_2() {
      am.Play("Idle");
      /* ===================================================== */
      restore_transform();
      /* ===================================================== */
      GetComponent<PhysicBody>().enabled = true;
      /* ===================================================== */
      GameObject.Find("Whistling").GetComponent<AudioSource>().Stop();
    }

    void sleep_1() {
      GameObject.Find("Starry Night").GetComponent<SpriteRenderer>().enabled = true;
      /* ===================================================== */
      GameObject.FindObjectOfType<shared.SceneCamera>().follow("");
      /* ===================================================== */
      transform.rotation = Quaternion.LookRotation(Vector3.up, Vector3.forward);
      /* ===================================================== */
      transform.position = GameObject.FindObjectOfType<Couch>().transform.position + Vector3.forward * 3;
      transform.position = Vector3.Scale(transform.position, new Vector3(1, 0, 1));
      transform.position += Vector3.up * (Camera.main.transform.position.y - 2); 
      /* ===================================================== */
      GetComponent<PhysicBody>().enabled = false;
      /* ===================================================== */
      GameObject.FindObjectOfType<shared.SceneCamera>().zoom("1,5");
      GameObject.FindObjectOfType<shared.SceneCamera>().point_on("Couch");
      /* ===================================================== */
      GameObject.Find("Man In Me").GetComponent<AudioSource>().Play();
    }

    void sleep_2() {
      transform.position += Vector3.forward * -0.5f * Time.deltaTime;
    }

    void sleep_3() {
      transform.position = GameObject.FindObjectOfType<Couch>().transform.position + Vector3.left * 5;
      transform.position = Vector3.Scale(transform.position, new Vector3(1, 0, 1));
      transform.position += Vector3.up * (Camera.main.transform.position.y - 2); 
      /* ===================================================== */
      transform.rotation = Quaternion.LookRotation(Vector3.up, Vector3.right);
    }


    void sleep_4() {
      transform.position += Vector3.right * 0.5f * Time.deltaTime;
    }


    void sleep_5() {
      transform.position = GameObject.FindObjectOfType<Couch>().transform.position + Vector3.forward * 3;
      transform.position = Vector3.Scale(transform.position, new Vector3(1, 0, 1));
      transform.position += Vector3.up * (Camera.main.transform.position.y - 2); 
      /* ===================================================== */
      transform.rotation = Quaternion.LookRotation(Vector3.up, Vector3.down);
    }

    void sleep_6() {
      transform.position += Vector3.forward * -0.5f * Time.deltaTime;
    }

    void sleep_7() {
      GameObject.Find("Starry Night").GetComponent<SpriteRenderer>().enabled = false;
      /* ===================================================== */
      GameObject.Find("Stop The Music").GetComponent<AudioSource>().Play();
      GameObject.Find("Man In Me").GetComponent<AudioSource>().Stop();
      /* ===================================================== */
      transform.position = GameObject.FindObjectOfType<Couch>().transform.position;
      transform.rotation = Quaternion.LookRotation(Vector3.right);
      /* ===================================================== */
      GameObject.FindObjectOfType<shared.SceneCamera>().zoom("6,5");
    }

    void disable_all_fsm_except(string machine_name) {
      foreach (PlayMakerFSM fsm in GetComponents<PlayMakerFSM>()) {
        if (fsm.FsmName != machine_name) fsm.enabled = false;
      }
    }

    void enable_all_fsm_except(string machine_name) {
      foreach (PlayMakerFSM fsm in GetComponents<PlayMakerFSM>()) {
        if (fsm.FsmName != machine_name) fsm.enabled = true;
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
}
