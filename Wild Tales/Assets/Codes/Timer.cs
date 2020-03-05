using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Timer : MonoBehaviour {
    public UnityEvent on_finish = new UnityEvent();
    public float sec;

    // Use this for initialization
    IEnumerator Start() {
        yield return new WaitForSeconds(sec);
        on_finish.Invoke();
    }
}
