using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniM : MonoBehaviour
{
    public Transform MiniMapTarget;


    void Update()
    {


    }

    void LateUpdate()
    {

        transform.position = new Vector3(MiniMapTarget.position.x, transform.position.y, MiniMapTarget.position.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, MiniMapTarget.eulerAngles.y, transform.eulerAngles.z);

    }
}
