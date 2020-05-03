using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Envi {
	public void knock() {
		FindObjectOfType<TriggerSystem>().door_knocked_stack.Push(this);
	}
}