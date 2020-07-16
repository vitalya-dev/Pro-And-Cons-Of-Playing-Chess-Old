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
    }

  }
}
