using System;
using System.Collections;
using System.Collections.Generic;
using Panda;
using UnityEngine;
using UnityEngine.Events;

public class TriggerSystem : MonoBehaviour {
	[HideInInspector]
	public Stack<Door> door_knocked_stack = new Stack<Door>();

	[HideInInspector]
	public UnityEvent restart_event = new UnityEvent();

	[HideInInspector]
	public static UnityEvent control_point_event = new UnityEvent();

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
	void display_message(string message, string type, string position, float duration, int message_id) {
		GameObject message_object = Instantiate(Resources.Load(type + " Message", typeof(GameObject))) as GameObject;
		message_object.GetComponent<TMPro.TextMeshPro>().text = message;
		message_object.transform.rotation = Quaternion.LookRotation(Vector3.down, Vector3.forward);
		message_object.transform.position = Vector3.Scale(GameObject.Find(position).transform.position, new Vector3(1, 0, 1)) + Vector3.up * (Camera.main.transform.position.y - 1);
		message_object.name = "Message_" + message_id;
		justify_message(message_object);
		GameObject.Destroy(message_object, duration);
		Task.current.Succeed();
	}

	[Task]
	void display_choices_2(string choice_1, string choice_2, string type, string position, int message_id) {
		GameObject message_object = Instantiate(Resources.Load(type + " Message", typeof(GameObject))) as GameObject;
		message_object.GetComponent<TMPro.TextMeshPro>().text = "<b>1</b>. " + choice_1 + "\n";
		message_object.GetComponent<TMPro.TextMeshPro>().text += "<b>2</b>. " + choice_2 + "\n";
		message_object.transform.rotation = Quaternion.LookRotation(Vector3.down, Vector3.forward);
		message_object.transform.position = Vector3.Scale(GameObject.Find(position).transform.position, new Vector3(1, 0, 1)) + Vector3.up * (Camera.main.transform.position.y - 1);
		message_object.name = "Message_" + message_id;
		justify_message(message_object);
		Task.current.Succeed();
	}

	void justify_message(GameObject message_object) {
		float w = message_object.GetComponent<RectTransform>().sizeDelta.x;
		float p_w = message_object.GetComponent<TMPro.TextMeshPro>().GetPreferredValues().x;
		float h = message_object.GetComponent<RectTransform>().sizeDelta.y;
		message_object.GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Min(p_w, w), h);
	}

	[Task]
	void destroy_message(int message_id) {
		GameObject.Destroy(GameObject.Find("Message_" + message_id));
		Task.current.Succeed();
	}

	[Task]
	void wait_for_choice() {

	}

	[Task]
	void say(string text) {
		Debug.Log(text);
		Task.current.Succeed();
	}

	[Task]
	void pop(string name, bool all) {
		switch (name) {
			case "door_knocked":
				if (all) door_knocked_stack.Clear();
				else door_knocked_stack.Pop();
				break;
		}
		Task.current.Succeed();
	}
}