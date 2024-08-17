using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowWeapons : MonoBehaviour
{
    public float SpeedRotate = 45f;
    
    void Update()
    {
       transform.rotation *= Quaternion.Euler(0,SpeedRotate * Time.deltaTime,0);   
    }
}
