using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Collections;

public class Area : MonoBehaviour {
    [SerializeField]
    private Color gizmo_color;
    [SerializeField]
    private bool selected_only;

    public T overlap<T>() {
        Collider[] colliders = Physics.OverlapBox(transform.TransformPoint(GetComponent<BoxCollider>().center), GetComponent<BoxCollider>().size / 2, transform.rotation);
        foreach (var collider in colliders) {
            if (collider.gameObject != gameObject && collider.GetComponent<T>() != null) {
                return collider.GetComponent<T>();
            }
        }
        return default;
    }

    public T[] overlap_all<T>() {
        Collider[] colliders = Physics.OverlapBox(transform.TransformPoint(GetComponent<BoxCollider>().center), GetComponent<BoxCollider>().size / 2, transform.rotation);
        List<T> ts = new List<T>();
        foreach (var collider in colliders)
            if (collider.gameObject != gameObject && collider.GetComponent<T>() != null) {
                ts.Add(collider.GetComponent<T>());
            }
        return ts.ToArray();
    }


    /* ============================================== */
    void OnDrawGizmos() {
        if (!selected_only) {
            draw_gizmo();
        }
    }

    void OnDrawGizmosSelected() {
        if (selected_only && (Selection.activeGameObject == gameObject)) {
            draw_gizmo();
        }
    }

    void draw_gizmo() {
        Gizmos.color = gizmo_color;
        Gizmos.DrawWireCube(transform.TransformPoint(GetComponent<BoxCollider>().center), GetComponent<BoxCollider>().size);
    }
    /* ============================================== */
}
