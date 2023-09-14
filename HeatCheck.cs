using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeatCheck : MonoBehaviour
{
    public Slider blasterBar;
    public Image blasterFill;
    public Text blasterCharge;

    public AudioClip[] announcement;
    public AudioSource ComputerVoice;

    private int heatLevel = 0;

    public bool OverHeatCheck(float blasterTemp)
    {
        float lastHeatLevel = heatLevel;
        Color[] colorArray = new [] {Color.green,Color.yellow,Color.red};
        string[] messages = new string[] {"READY","OVERHEATED","OFFLINE"};

        if (blasterTemp <= 73 && heatLevel == 1 ) heatLevel = 0;
        else if (blasterTemp <= 88 && heatLevel == 2) heatLevel = 1;
        else if (blasterTemp > 75 && heatLevel == 0) heatLevel = 1;
        else if (blasterTemp > 90 && heatLevel == 1) heatLevel = 2;

        if (heatLevel != lastHeatLevel) 
        {
            blasterCharge.text = messages[heatLevel];
            blasterFill.color = colorArray[heatLevel];
            StartCoroutine(playMessage(heatLevel));
        }

        return heatLevel == 2 ? true : false;
    }

    IEnumerator playMessage(int msgNum)
    {

        while (ComputerVoice.isPlaying)
        {
            yield return new WaitForSeconds(1);
        }

        ComputerVoice.clip = announcement[msgNum];
        ComputerVoice.Play();
    }
}
