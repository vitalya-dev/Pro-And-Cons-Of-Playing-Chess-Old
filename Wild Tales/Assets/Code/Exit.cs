using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : Door {
    override public void open() {
      GameObject.Find("Door Locked").GetComponent<AudioSource>().Play();
    }
}
