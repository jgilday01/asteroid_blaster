using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowards : MonoBehaviour
{
    public GameObject player;

    public float shipAttractDefault;
    public float shipAttractClose;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (GameObject.Find("Player") == false) return;

        float distance = Vector3.Distance(transform.position, player.transform.position);
        float atttaction = distance > 10 ? shipAttractDefault : shipAttractClose;
        float step = atttaction * Time.deltaTime;

        if (this.transform.position.z > -2.5f)
        {
            if (player)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
                GetComponent<Rigidbody>().AddForce((player.transform.position - transform.position) * (Time.deltaTime) / 5.0f);
            }
        }
    }
}
