using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Phone : MonoBehaviour {
  public void pickup() {
    GameObject.Find("Phone Ring").GetComponent<AudioSource>().Stop();
    GameObject.Find("Phone Pickup").GetComponent<AudioSource>().Play();
  }

  public void hangup() {
    GameObject.Find("Phone Hangup").GetComponent<AudioSource>().Play();
  }

  public void ring() {
    GameObject.Find("Phone Ring").GetComponent<AudioSource>().loop = true;
    GameObject.Find("Phone Ring").GetComponent<AudioSource>().Play();
  }

}
