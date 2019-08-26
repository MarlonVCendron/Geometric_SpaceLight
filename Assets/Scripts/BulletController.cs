using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    public Vector3 direction;
    public float speed;
    public float damage;
    public string targetTag;

    void Update()
    {
        transform.position += direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == targetTag)
        {
            if (targetTag == "Enemy")
            {
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                enemy.decreaseHealth(damage);
            }
            else if (targetTag == "Player")
            {
                PlayerController player = collision.gameObject.GetComponent<PlayerController>();
                player.addHealth(-damage);
            }

            Destroy(gameObject);
        }else if (collision.gameObject.tag == "Shield" && targetTag == "Player")
        {
            Destroy(gameObject);
        }
    }

}
