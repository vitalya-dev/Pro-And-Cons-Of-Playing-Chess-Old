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
      GameObject.FindObjectOfType<shared.SceneCamera>().follow("");
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
        "\n" +
        "\n" +
        "\n" +
        "Но некоторые причины, которые " +
        "естественны и неизбежны как " +
        "восход солнца, заставляют меня " +
        "делать то, что я делаю." +
        "\n" +
        "\n" +
        "\n" +
        "Говоря про неизбежность, думаю Вы поняли " +
        "кто я, и что мне надо. Слишком " +
        "много сигарет, слишком много " +
        "алкоголя, слишком много всего " +
        "другого." +
        "\n" +
        "\n" +
        "\n" +
        "Прошу Вас, дышите глубже, и не " +
        "умирайте пока я не закончил. Этим " +
        "Вы меня поставите в глупое " +
        "положение, а мне это не совсем к " +
        "лицу." +
        "\n" +
        "\n" +
        "\n" +
        "Я знал Вашего " +
        "прапрапрапрапрапрадеда господин " +
        "Блок. Он предложил мне сыграть в " +
        "шахматы, и я согласился. Но " +
        "рыцарь обманул меня и не доиграл " +
        "до конца. А партия стала занозой " +
        "у меня в голове. Сводит с ума. " +
        "Не дает спокойно делать свое дело.\n" +
        "\n" +
        "\n" +
        "\n" +
        "\n" +
        "\n" +
        "\n" +
        "Хорошие новости, господин Блок. " +
        "Вы получили отсрочку. Правила " +
        "просты. Пока играешь - я тебя не " +
        "трогаю. Cтавишь мат - отпускаю. " +
        "Во всех остальных случаях - тебе " +
        "настает срок.";
      /* ===================================================== */
      text_object.name = GetInstanceID() + "_";
      /* ===================================================== */
      string[] chars = {"@", "%", "^", "&", "$"};
      foreach (var c in chars) text_object.name += chars[UnityEngine.Random.Range(0, chars.Length)];
      /* ===================================================== */
      GameObject.Find("Marine Mist").GetComponent<SpriteRenderer>().enabled = true;
    }
    
    public void message_3() {
      text_object.GetComponent<RectTransform>().offsetMax += new Vector2(0, 45) * Time.deltaTime;
    }

    public void message_4() {
      GameObject.Find("Marine Mist").GetComponent<SpriteRenderer>().enabled = false;
      GameObject.Find("Tchaikovsky. 1812 Overture").GetComponent<AudioSource>().Stop();
      GameObject.Find("Stop The Music").GetComponent<AudioSource>().Play();
      /* ===================================================== */
      GameObject.FindObjectOfType<shared.SceneCamera>().zoom("6,5");
      GameObject.FindObjectOfType<shared.SceneCamera>().follow("Player");
      /* ===================================================== */
      Destroy(text_object.gameObject);
    }

  }
}
