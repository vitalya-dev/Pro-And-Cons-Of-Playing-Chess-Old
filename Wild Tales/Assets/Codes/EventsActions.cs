using System.Collections;
using System.Collections.Generic;
using Panda;
using UnityEngine;
using UnityEngine.Events;


public class EventsActions : MonoBehaviour {
    [HideInInspector]
    public static Stack<Door> door_knocked_stack = new Stack<Door>();

    [HideInInspector]
    public static UnityEvent restart_event = new UnityEvent();

    [HideInInspector]
    public static UnityEvent control_point_event = new UnityEvent();

    void Awake() {
		
    }

    [Task]
    void door_knocked() {
        if (door_knocked_stack.Count > 0)
            Task.current.Succeed();
        else
            Task.current.Fail();
    }

    [Task]
    void door_knocked(string door_name) {
        if (door_knocked_stack.Count > 0 && door_knocked_stack.Peek().name == door_name)
            Task.current.Succeed();
        else
            Task.current.Fail();
    }

    [Task]
    void display_message(string message, string type, string position, float duration, int id) {
        GameObject display_object = Instantiate(Resources.Load(type + " Text", typeof(GameObject))) as GameObject;
        display_object.GetComponent<TMPro.TextMeshPro>().text = message;
        display_object.transform.rotation = Quaternion.LookRotation(Vector3.down, Vector3.forward);
        display_object.transform.position = Vector3.Scale(GameObject.Find(position).transform.position, new Vector3(1, 0, 1)) + Vector3.up * (Camera.main.transform.position.y - 1);
        display_object.name = "Display_" + id;
        justify_text(display_object);
        GameObject.Destroy(display_object, duration);
        Task.current.Succeed();
    }

    [Task]
    void display_message_for(string who, string message, string type, float duration, int id) {
        GameObject display_object = Instantiate(Resources.Load(type + " Text", typeof(GameObject))) as GameObject;
        display_object.GetComponent<TMPro.TextMeshPro>().text = message;
        display_object.transform.rotation = Quaternion.LookRotation(Vector3.down, Vector3.forward);
        display_object.transform.position = Vector3.Scale(GameObject.Find(who).transform.position, new Vector3(1, 0, 1)) + Vector3.up * (Camera.main.transform.position.y - 1);
        display_object.AddComponent<Follow>();
        display_object.GetComponent<Follow>().target = GameObject.Find(who).transform;
        display_object.name = "Display_" + id;
        justify_text(display_object);
        GameObject.Destroy(display_object, duration);
        Task.current.Succeed();
    }

    [Task]
    void display_choices_2(string choice_1, string choice_2, string type, string position, int id) {
        GameObject display_object =
            Instantiate(Resources.Load(type + " Text", typeof(GameObject))) as GameObject;
        display_object.GetComponent<TMPro.TextMeshPro>().text = "<b>1</b>. " + choice_1 + "\n";
        display_object.GetComponent<TMPro.TextMeshPro>().text += "<b>2</b>. " + choice_2 + "\n";
        display_object.transform.rotation = Quaternion.LookRotation(Vector3.down, Vector3.forward);
        display_object.transform.position = Vector3.Scale(GameObject.Find(position).transform.position, new Vector3(1, 0, 1)) + Vector3.up * (Camera.main.transform.position.y - 1);
        display_object.name = "Display_" + id;
        justify_text(display_object);
        Task.current.Succeed();
    }

    [Task]
    void display_choices_2_for(string who, string choice_1, string choice_2, string type, int id) {
        GameObject display_object = Instantiate(Resources.Load(type + " Text", typeof(GameObject))) as GameObject;
        display_object.GetComponent<TMPro.TextMeshPro>().text = "<b>1</b>. " + choice_1 + "\n";
        display_object.GetComponent<TMPro.TextMeshPro>().text += "<b>2</b>. " + choice_2 + "\n";
        display_object.transform.rotation = Quaternion.LookRotation(Vector3.down, Vector3.forward);
        display_object.transform.position = Vector3.Scale(GameObject.Find(who).transform.position, new Vector3(1, 0, 1)) + Vector3.up * (Camera.main.transform.position.y - 1);
        display_object.AddComponent<Follow>();
        display_object.GetComponent<Follow>().target = GameObject.Find(who).transform;
        display_object.name = "Display_" + id;
        justify_text(display_object);
        Task.current.Succeed();
    }

    // Helper method. Text will have the right width.
    void justify_text(GameObject display_object) {
        float w = display_object.GetComponent<RectTransform>().sizeDelta.x;
        float p_w = display_object.GetComponent<TMPro.TextMeshPro>().GetPreferredValues().x;
        float h = display_object.GetComponent<RectTransform>().sizeDelta.y;
        display_object.GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Min(p_w, w), h);
    }

    [Task]
    void destroy_display(int id) {
        GameObject.Destroy(GameObject.Find("Display_" + id));
        Task.current.Succeed();
    }

    [Task]
    void debug(string text) {
        Debug.Log(text);
        Task.current.Succeed();
    }

    [Task]
    void pop(string events_stack_name, bool all) {
        switch (events_stack_name) {
            case "door_knocked":
                if (all) door_knocked_stack.Clear();
                else door_knocked_stack.Pop();
                break;
        }
        Task.current.Succeed();
    }

    [Task]
    void actor_move(string actor, string position) {
        GameObject.Find(actor).GetComponent<Actor>().move(GameObject.Find(position).transform.position);
        Task.current.Succeed();
    }

    [Task]
    void actor_move_wait(string actor) {
        if (!GameObject.Find(actor).GetComponent<Actor>().moving)
            Task.current.Succeed();
    }
}
