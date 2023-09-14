using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickup : MonoBehaviour
{
    private BonusMessage bonusMessage;
    private PlayerController playerContollerObj;
    public GameObject powerUpSound;

    public int pickupScore;


    void Start()
    {
        if (GameObject.FindGameObjectsWithTag("Player").Length == 1)
        {
            playerContollerObj = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }

        bonusMessage = GameObject.FindGameObjectWithTag("GameController").GetComponent<BonusMessage>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!playerContollerObj) return;

        if (other.tag == "Player")
        {
            Vector3 originLocation = new Vector3(0, 0, 0);
            GameObject playSound = Instantiate(powerUpSound, originLocation, Quaternion.identity);

            playerContollerObj.ResetSliders();
            bonusMessage.PickupTrigger(pickupScore);

            Destroy(gameObject);
        }
    }
}
