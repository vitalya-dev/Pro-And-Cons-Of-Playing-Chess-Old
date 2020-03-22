using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class LevelManager {
    public static UnityEvent restart_event = new UnityEvent();
    public static void restart() {
        restart_event.Invoke();
    }

    public static UnityEvent control_point_event = new UnityEvent();
    public static void control_point() {
        control_point_event.Invoke();
    }

}
