using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLight : MonoBehaviour
{

    Light light;
    float multiplier = 1f;

    private void Start()
    {
        light = GetComponent<Light>();
    }

    void Update()
    {
        light.intensity += 0.1f * multiplier; 
        
        if(light.intensity <= 2f || light.intensity >= 30f)
        {
            multiplier *= -1;
        }
    }
}
