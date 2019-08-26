using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public GameObject shield;
    public GameObject healthBar;
    public ParticleSystem explosionParticles;

    private float health = 5f;
    public bool dead = false;

    void Update()
    {
        if (!dead)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                Vector3 cameraPointPosition = Camera.main.ScreenToWorldPoint(touch.position);
                Vector3 targetPosition = new Vector3(cameraPointPosition.x, cameraPointPosition.y, 0f);
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 4f);

            }

            if (health <= 0)
            {
                //StartCoroutine(PlayerDied());
                UnityEngine.SceneManagement.SceneManager.LoadScene(2);
            }

            RectTransform BarRectTransform = healthBar.transform as RectTransform;
            BarRectTransform.sizeDelta = new Vector2(health * 300f / 5f, BarRectTransform.sizeDelta.y);
        }
    }

    public void ActivateShieldPower(float t)
    {
        StartCoroutine(coroutineShield(t));
    }

    public IEnumerator coroutineShield(float t)
    {
        GameObject newShield = Instantiate(shield, new Vector2(transform.position.x, transform.position.y + 0.4f), Quaternion.identity);
        newShield.transform.parent = transform;
        yield return new WaitForSeconds(t);

        newShield.GetComponent<FadeSprite>().FadeOutDestroy();
    }


    public void addHealth(float amount)
    {
        if (health + amount <= 5f) {
            health += amount;
        }
    }

    //IEnumerator PlayerDied()
    //{
       // dead = true;
        //explosionParticles.Play();
        //gameObject.GetComponent<Renderer>().enabled = false;

        //yield return new WaitForSeconds(2f);

        //UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    //}
}
