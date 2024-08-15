using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticules : MonoBehaviour
{
    public float destropyTime = 3f;
    void Update()
    {
        Destroy(gameObject, destropyTime);
    }
}
