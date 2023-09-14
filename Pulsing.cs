using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulsing : MonoBehaviour
{
    public float pulseSpeed;
    public float pulseRange;
    public float pulseMinimum;

    void Update()
    {
        GetComponent<Light>().range = pulseMinimum + (Mathf.PingPong(Time.time * pulseSpeed, pulseRange));
    }
}
