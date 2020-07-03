using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace scene_2 {
  [SelectionBase]
  public class Cupboard : shared.Cupboard {
    public Sprite shoulder_sprite;
    public Sprite leg_sprite;
    public Sprite body_sprite;

    void Start() {
      shoulder_sprite = Resources.Load<Sprite>("Graphics/Layers/Layer_34_copy_2");
      leg_sprite = Resources.Load<Sprite>("Graphics/Layers/Layer_33_copy_2");
      body_sprite = Resources.Load<Sprite>("Graphics/Layers/Layer_29_copy");
    }
  }
}
