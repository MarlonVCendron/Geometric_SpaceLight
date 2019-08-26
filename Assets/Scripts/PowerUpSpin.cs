using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpin : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, transform.rotation.z + 0.5f, Space.Self); 
    }
}
