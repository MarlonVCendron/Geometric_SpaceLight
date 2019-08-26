using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{

    private float changeCountdown = 0f;
    Vector3 randomPosition;

    private void Start()
    {
        changeCountdown = Random.Range(0f, 5f);
    }

    void Update()
    {
        StartCoroutine(RandomLerp());
    }

    IEnumerator RandomLerp()
    {
        if (changeCountdown <= 0f)
        {
            randomPosition = new Vector2(Random.Range(2.8f, -2.8f), Random.Range(5f, -5f));
            changeCountdown = 10f;
        }
        else
        {
            changeCountdown -= 0.1f;
        }
        transform.position = Vector2.Lerp (transform.position, randomPosition, Time.deltaTime);
        yield return new WaitForSeconds(1f);
    }
}

