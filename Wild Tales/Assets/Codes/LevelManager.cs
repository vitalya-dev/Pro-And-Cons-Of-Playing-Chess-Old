using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class LevelManager {
    public static UnityEvent restart_event = new UnityEvent();
    public static void restart() {
        restart_event.Invoke();
    }
}
