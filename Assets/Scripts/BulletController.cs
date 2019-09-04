using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    public Vector3 direction;
    public float speed;
    public float damage;
    public string targetTag;
    Vector2 screenPosition;

    void Update()
    {
        transform.position += direction * speed;

        screenPosition = Camera.main.WorldToScreenPoint(transform.position);

        if (screenPosition.y > Screen.height || screenPosition.y < 0)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == targetTag)
        {
            if (targetTag == "Enemy")
            {
                if (collision.gameObject.GetComponent<Enemy>())
                {
                    Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                    enemy.decreaseHealth(damage);
                }
                else 
                { 
                    Asteroid asteroid = collision.gameObject.GetComponent<Asteroid>();
                    asteroid.decreaseHealth(damage);
                }
            }
            else if (targetTag == "Player")
            {
                PlayerController player = collision.gameObject.GetComponent<PlayerController>();
                player.addHealth(-damage);
            }

            Explode();
        }else if (collision.gameObject.tag == "Shield" && targetTag == "Player")
        {
            Explode();
        }

    }

    void Explode()
    {
        //Camera.main.GetComponent<CameraShake>().ShakeCamera(0.01f, 0.005f);

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        ParticleSystem deathExplosion = transform.GetChild(0).GetComponent<ParticleSystem>();
        deathExplosion.Play();
        Destroy(gameObject, deathExplosion.duration);
    }

}
