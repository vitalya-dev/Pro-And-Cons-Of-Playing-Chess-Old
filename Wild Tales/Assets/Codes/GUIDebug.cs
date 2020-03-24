using UnityEngine;
using UnityEngine.UI;



public class GUIDebug {
    const int MAX_LINES = 20;
    /* ========================================== */
    static string[] lines = new string[MAX_LINES];
    static int current_line = 0;

    public static void log(string text) {
        if (current_line < MAX_LINES) {
            lines[current_line] = text;
            current_line += 1;
        }
        else {
            for (int i = 1; i < MAX_LINES; i++)
                lines[i - 1] = lines[i];
            lines[MAX_LINES - 1] = text;
        }
        /* ========================================== */
        Text text_comp = GameObject.Find("GUIDebug/Text").GetComponent<Text>();
        text_comp.text = "";
        foreach (var line in lines) {
            text_comp.text += line;
            text_comp.text += "\n";
        }
    }
}