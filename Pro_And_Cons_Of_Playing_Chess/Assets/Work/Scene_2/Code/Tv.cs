using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace scene_2 {
  [SelectionBase]
  public class Tv : MonoBehaviour {
    public void turn_on() {
      GameObject.Find("Tv Noise").GetComponent<AudioSource>().Play();
      /* ===================================================== */
      transform.Find("Screen").GetComponent<SpriteRenderer>().enabled = true;
      transform.Find("Light").GetComponent<Light>().enabled = true;
    }

    public void turn_off() {
      // Paste some turn on music
      /* ===================================================== */
      transform.Find("Screen").GetComponent<SpriteRenderer>().enabled = false;
      transform.Find("Light").GetComponent<Light>().enabled = false;
    }

    public void start_the_message_1() {
      GameObject.FindObjectOfType<shared.SceneCamera>().zoom("1,5");
      GameObject.FindObjectOfType<shared.SceneCamera>().point_on(gameObject);
    }

    public void start_the_message_2() {
      GameObject.Find("Tchaikovsky. 1812 Overture").GetComponent<AudioSource>().Play();
    }

  }
}
