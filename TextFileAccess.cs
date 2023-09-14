using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextFileAccess : MonoBehaviour
{
    public TMP_Text infoText;
    public TextAsset infoFile;

    void Start()
    {
        infoText.text = infoFile.text;
    }
}
