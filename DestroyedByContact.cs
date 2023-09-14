using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyedByContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject pickup;
    public int scoreValue;

    private GameController gameControllerObj;
    private Transform mainCamera;


    void Start()
    {
        mainCamera = Camera.main.transform;
        gameControllerObj = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerFire") Destroy(other.transform.gameObject); //remove lasers

        GameObject instantiatedPrefab = Instantiate(explosion, transform.position, transform.rotation); //create explosion
        instantiatedPrefab.transform.localScale = transform.localScale; // new Vector3(2.5f, 2.5f, 2.5f);

        if (pickup) Instantiate(pickup, transform.position, transform.rotation);

        if (other.tag == "PlayerFire") gameControllerObj.AddScore(scoreValue);

        Destroy(gameObject); //remove the object
    }
}
