using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Area : MonoBehaviour {
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
        Collider[] colliders = Physics.OverlapBox(transform.TransformPoint(GetComponent<BoxCollider>().center), GetComponent<BoxCollider>().size / 2);
        List<T> ts = new List<T>();
        foreach (var collider in colliders)
            if (collider.gameObject != gameObject && collider.GetComponent<T>() != null) {
                ts.Add(collider.GetComponent<T>());
            }
        return ts.ToArray();
    }
}
