using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Cupboard : MonoBehaviour {
  public Sprite shoulder_sprite;
  public Sprite leg_sprite;
  public Sprite body_sprite;

  void Start() {
    shoulder_sprite = Resources.Load<Sprite>("Graphics/Layers/Layer_34_copy_2");
    leg_sprite = Resources.Load<Sprite>("Graphics/Layers/Layer_33_copy_2");
    body_sprite = Resources.Load<Sprite>("Graphics/Layers/Layer_29_copy");
  }

  public void open() {
    GameObject.Find("Cupboard Open").GetComponent<AudioSource>().Play();
  }

}
