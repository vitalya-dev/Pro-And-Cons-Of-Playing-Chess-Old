using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Phone : MonoBehaviour {
  public void pickup() {
    /* ===================================================== */
    foreach (var t_o in GameObject.FindObjectsOfType<TMPro.TextMeshPro>())
      if (t_o.name.StartsWith(GetInstanceID() + "_")) Destroy(t_o.gameObject);
    GameObject.Find("Phone Ring").GetComponent<AudioSource>().Stop();
    CancelInvoke("ring");
    /* ===================================================== */
    GameObject.Find("Phone Pickup").GetComponent<AudioSource>().Play();
  }

  public void hangup() {
    GameObject.Find("Phone Hangup").GetComponent<AudioSource>().Play();
  }

  public void ring() {
    foreach (var t_o in GameObject.FindObjectsOfType<TMPro.TextMeshPro>())
      if (t_o.name.StartsWith(GetInstanceID() + "_")) {
        Destroy(t_o.gameObject);
        /* ===================================================== */
        Invoke("ring", 1.0f);
        /* ===================================================== */
        return;
      }
    /* ===================================================== */
    GameObject text_object = Instantiate(Resources.Load("Etc/Text", typeof(GameObject))) as GameObject;
    /* ===================================================== */
    text_object.GetComponent<TMPro.TextMeshPro>().text = "<sprite index=0>";
    /* ===================================================== */
    text_object.transform.rotation = Quaternion.LookRotation(Vector3.down, Vector3.forward);
    /* ===================================================== */
    float width = Mathf.Min(16, "♪".Length);
    float height = 0;
    /* ===================================================== */
    text_object.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
    height = text_object.GetComponent<TMPro.TextMeshPro>().GetPreferredValues().y;
    text_object.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
    /* ===================================================== */
    text_object.transform.position = Vector3.Scale(transform.position, new Vector3(1, 0, 1));
    text_object.transform.position += Vector3.up * (Camera.main.transform.position.y - 1); 
    /* ===================================================== */
    text_object.name = GetInstanceID() + "_" + "♪";
    /* ===================================================== */
    GameObject.Find("Phone Ring").GetComponent<AudioSource>().Play();
    /* ===================================================== */
    Invoke("ring", 3.5f);
  }
}
