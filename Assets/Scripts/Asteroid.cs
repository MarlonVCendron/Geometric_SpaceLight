using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    private float MaxVelocity;
    private float MaxForce = 1f;
    private Vector3 velocity = Vector2.zero;
    private float health;
    private bool dead = false;

    private int size;

    private void Start()
    {
        size = Mathf.FloorToInt(Random.Range(1, 5));

        health = size * 8f;
        MaxVelocity = 2.5f / size;
        transform.localScale = new Vector3((float) size/ 30, (float) size / 30, 0);
        UpdateColliderSize();
    }

    void Update()
    {
        if (!dead)
        {
            seekPlayer();

            if (health <= 0)
            {
                Camera.main.GetComponent<ScoreManager>().IncreaseScore();

                dead = true;
                Explode();
            }
        }
    }

    void Explode()
    {
        Camera.main.GetComponent<CameraShake>().ShakeCamera(0.5f, 0.05f);

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        ParticleSystem deathExplosion = transform.GetChild(0).GetComponent<ParticleSystem>();
        deathExplosion.Play();
        Destroy(gameObject, deathExplosion.duration);
    }

    public void decreaseHealth(float amount)
    {
        health -= amount;
    }

    void seekPlayer()
    {
        transform.Rotate(0, 0, transform.rotation.z + MaxVelocity, Space.Self);

        Vector3 desiredVelocity = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
        desiredVelocity = desiredVelocity.normalized * MaxVelocity;

        Vector3 steering = desiredVelocity - velocity;
        steering = Vector3.ClampMagnitude(steering, MaxForce);

        velocity = Vector3.ClampMagnitude(velocity + steering, MaxVelocity);
        transform.position += velocity * Time.deltaTime;
        //transform.forward = velocity.normalized;

    }

    private void UpdateColliderSize() {
        Vector3 spriteHalfSize = GetComponent<SpriteRenderer>().sprite.bounds.extents;
        GetComponent<CircleCollider2D>().radius = spriteHalfSize.x > spriteHalfSize.y ? spriteHalfSize.x - 1f : spriteHalfSize.y - 1f;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.addHealth(-size);

            Explode();
        }else if (collision.tag == "Shield")
        {
            Explode();
        }
    }

}
