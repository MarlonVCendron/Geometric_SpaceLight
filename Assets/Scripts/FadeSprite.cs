using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeSprite : MonoBehaviour
{

    SpriteRenderer rend;

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        Color c = rend.material.color;
        c.a = 0f;
        rend.material.color = c;

        StartFading();
    }

    void Update()
    {
        
    }

    IEnumerator FadeIn()
    {
        for (float f = 0.05f; f <= 1; f += 0.05f)
        {
            Color c = rend.material.color;
            c.a = f;
            rend.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }

    }

    void StartFading()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeOut()
    {
        for (float f = 1f; f >= 0; f -= 0.05f)
        {
            Color c = rend.material.color;
            c.a = f;
            rend.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }

        Destroy(gameObject);

    }

    public void FadeOutDestroy()
    {
        StartCoroutine(FadeOut());
    }


}
