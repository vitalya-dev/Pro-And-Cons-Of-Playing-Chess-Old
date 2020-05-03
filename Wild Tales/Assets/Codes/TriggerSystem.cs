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
		GameObject.Destroy(message_object, duration);
		Task.current.Succeed();
	}

	[Task]
	void display_choices_2(string choice_1, string choice_2, string type, string position, int message_id) {
		GameObject message_object = Instantiate(Resources.Load(type + " Choices_2", typeof(GameObject))) as GameObject;
		message_object.transform.rotation = Quaternion.LookRotation(Vector3.down, Vector3.forward);
		message_object.transform.position = Vector3.Scale(GameObject.Find(position).transform.position, new Vector3(1, 0, 1)) + Vector3.up * (Camera.main.transform.position.y - 1);
		message_object.transform.Find("1").GetComponent<TMPro.TextMeshPro>().text = "1. " + choice_1;
		message_object.transform.Find("2").GetComponent<TMPro.TextMeshPro>().text = "2. " + choice_2;
		message_object.name = "Message_" + message_id;
		Task.current.Succeed();
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