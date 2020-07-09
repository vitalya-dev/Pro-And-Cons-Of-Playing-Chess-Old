using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace scene_1 {
  public class Exit : shared.Exit {
    override public void open() {
      GameObject.Find("Door Locked").GetComponent<AudioSource>().Play();
    }
  }
}
