using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Area : MonoBehaviour {

    public T overlap<T>(int mask = ~0) {
        ContactFilter2D contact_filter = new ContactFilter2D();
        contact_filter.SetLayerMask(mask);
        /* ========================================================== */
        Collider2D[] colliders = new Collider2D[1000];
        int count = GetComponent<Collider2D>().OverlapCollider(contact_filter, colliders);
        System.Array.Resize(ref colliders, count);
        /* ========================================================== */
        foreach (var collider in colliders) {
            if (collider.GetComponent<T>() != null)
                return collider.GetComponent<T>();
        }
        return default;
    }

    public T[] overlap_all<T>(int mask = ~0) {
        ContactFilter2D contact_filter = new ContactFilter2D();
        contact_filter.SetLayerMask(mask);
        /* ========================================================== */
        Collider2D[] colliders = new Collider2D[1000];
        int count = GetComponent<Collider2D>().OverlapCollider(contact_filter, colliders);
        System.Array.Resize(ref colliders, count);
        /* ========================================================== */
        List<T> ts = new List<T>();
        foreach (var collider in colliders) {
            if (collider.GetComponent<T>() != null)
                ts.Add(collider.GetComponent<T>());
        }
        return ts.ToArray();
    }
}
