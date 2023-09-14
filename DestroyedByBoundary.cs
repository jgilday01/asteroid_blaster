using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyedByBoundary : MonoBehaviour
{
    public int boundaryTopY;
    public int boundaryBotY;
    public int boundaryMinZ;

    void Update()
    {
        if (transform.position.x > boundaryTopY
        || transform.position.y < boundaryBotY
        || transform.position.z < boundaryMinZ)
        {
            Destroy(gameObject, 1);
        }
    }
}
