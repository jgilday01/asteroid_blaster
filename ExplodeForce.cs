using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeForce : MonoBehaviour
{
    public float minDistance;
    public float power;
    public float multipler;
    public GameObject target;

    void Start()
    {
        target = GameObject.Find("Player");

        float radius = transform.localScale.y * multipler;
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);

        foreach (Collider hit in colliders)
        {
            if (target && (hit.transform.position.z - target.transform.position.z) < minDistance) return;

            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null) rb.AddExplosionForce(power, explosionPos, radius, 1.0f);
        }
    }
}
