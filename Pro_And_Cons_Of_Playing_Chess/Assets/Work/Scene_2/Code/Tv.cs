using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace scene_2 {
  [SelectionBase]
  public class Tv : shared.Tv {
    GameObject text_object;

    public void message_1() {
      GameObject.FindObjectOfType<shared.SceneCamera>().zoom("1,5");
      GameObject.FindObjectOfType<shared.SceneCamera>().point_on(gameObject);
    }

    public void message_2() {
      GameObject.Find("Tchaikovsky. 1812 Overture").GetComponent<AudioSource>().Play();
      /* ===================================================== */
      text_object = Instantiate(Resources.Load("Etc/UIText", typeof(GameObject))) as GameObject;
      text_object.transform.SetParent(GameObject.Find("Canvas").transform);
      text_object.GetComponent<RectTransform>().offsetMin = new Vector2(10, 0);
      text_object.GetComponent<RectTransform>().offsetMax = new Vector2(-10, -Screen.height);
      /* ===================================================== */
      text_object.GetComponent<TMPro.TextMeshProUGUI>().text =
        "Здравствуйте господин Блок. Прошу " +
        "прощение за то что внес сумятицу " +
        "в ваш обычный распорядок дня. я " +
        "понимаю как спокойно и удобно " + 
        "однообразие, люблю его и " +
        "наслаждаюсь им так же как и Вы. " +
        "Но некоторые причины, которые " +
        "естественны и неизбежны как " +
        "восход солнца, заставляют меня " +
        "делать то, что я делаю. Говоря " +
        "про неизбежность, думаю Вы поняли " +
        "кто я, и что мне надо. Слишком " +
        "много сигарет, слишком много " +
        "алкоголя, слишком много всего " +
        "другого.";
      /* ===================================================== */
      text_object.name = GetInstanceID() + "_";
      /* ===================================================== */
      string[] chars = {"@", "%", "^", "&", "$"};
      foreach (var c in chars) text_object.name += chars[UnityEngine.Random.Range(0, chars.Length)];
      /* ===================================================== */
      GameObject.Find("Marine Mist").GetComponent<SpriteRenderer>().enabled = true;
    }
    
    public void message_3() {
      Debug.Log(Screen.height);
      text_object.GetComponent<RectTransform>().offsetMax += new Vector2(0, 25) * Time.deltaTime;
    }
  }
}
