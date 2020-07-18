using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace scene_2 {
  [SelectionBase]
  public class Tv : shared.Tv {
    public void start_the_message_1() {
      GameObject.FindObjectOfType<shared.SceneCamera>().zoom("1,5");
      GameObject.FindObjectOfType<shared.SceneCamera>().point_on(gameObject);
    }

    public void start_the_message_2() {
      GameObject.Find("Tchaikovsky. 1812 Overture").GetComponent<AudioSource>().Play();
      /* ===================================================== */
      GameObject text_object = Instantiate(Resources.Load("Etc/UIText", typeof(GameObject))) as GameObject;
      text_object.transform.SetParent(GameObject.Find("Canvas").transform);
      text_object.GetComponent<RectTransform>().offsetMin = new Vector2(10, 0);
      text_object.GetComponent<RectTransform>().offsetMax = new Vector2(-10, 0);
      /* ===================================================== */
      GameObject.Find("Marine Mist").GetComponent<SpriteRenderer>().enabled = true;
    }
  }
}
