using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    private GameController gameControllerObj;

    public float speed;
    public float waveMultiplier;
    public bool staticSpeed;
    public bool forward;


    void Start()
    {
        gameControllerObj = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        int direction = forward ? 1 : -1;

        if (gameControllerObj.wave > 1 && !staticSpeed) speed += (gameControllerObj.wave * waveMultiplier);

        GetComponent<Rigidbody>().velocity = (transform.forward * direction) * speed;
    }
}
