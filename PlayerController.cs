using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float yMin;
    public float yMax;
    public float vSpeed;
    public float slope;
    public float blasterTemp;
    public float ammoCool;
    public float ammoHeat;

    public Slider blasterBar;
    public Image blasterFill;
    public Text blasterCharge;

    public Transform C_Shot;
    public GameObject shot;
    public GameObject playerExplosion;

    public GameController gameController;

    private bool offline = false;
    private HeatCheck heatCheckObj;

    void Start()
    {
        heatCheckObj = GetComponent<HeatCheck>();

        ResetSliders();
    }

    void Update()
    {
        float moveVertical = Input.GetAxis("Vertical");

        if (moveVertical != 0) PlayerMoving(moveVertical);

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("space")) PlayerFiring();

        float coolDwon = gameController.warping ? ammoCool * 3 : ammoCool;

        blasterTemp = blasterTemp <= 0 ? 0 : blasterTemp - coolDwon;
        blasterBar.value = blasterTemp;

        offline = heatCheckObj.OverHeatCheck(blasterTemp);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerFire" || other.tag == "Pickup") return;

        Instantiate(playerExplosion, transform.position, transform.rotation);
        gameController.GameOver();
        Destroy(gameObject);
    }

    public void ResetSliders()
    {
        blasterTemp = 1;
        blasterBar.value = blasterTemp;
        blasterFill.color = Color.green;
        blasterCharge.text = "READY";
    }

    public void PlayerMoving(float moveVertical)
    {
        float speedVertical = vSpeed;

        if (transform.position.y > yMax && moveVertical > 0 || transform.position.y < yMin && moveVertical < 0) moveVertical = 0;

        Vector3 movement = new Vector3(0, moveVertical * speedVertical, 0);
        GetComponent<Rigidbody>().velocity = movement;
        GetComponent<Rigidbody>().rotation = Quaternion.Slerp(GetComponent<Rigidbody>().rotation, Quaternion.Euler(moveVertical * -slope, 0.0f, 0.0f), .25f);
    }

    public void PlayerFiring()
    {
        if (gameController.warping || offline || Time.timeScale == 0) return;

        gameController.shotsFired++;

        Instantiate(shot, C_Shot.position, C_Shot.rotation);
        blasterTemp = blasterTemp + ammoHeat;
        blasterBar.value = blasterTemp;
    }
}
