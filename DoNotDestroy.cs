using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDestroy : MonoBehaviour
{
    void Awake()
    {
        GameObject[] musicObjs = GameObject.FindGameObjectsWithTag("GameMusic");

        if (musicObjs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
