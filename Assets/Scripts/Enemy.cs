using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float startingHealth;
    public float speed = 1f;
    public GameObject bullet;
    public float fireRate;
    public float bulletSpeed = 0.08f;
    public float nextTimeToShoot = 0f;
    public float bulletDamage = 1f;

    private float targetY;
    private float tempTargetX;
    private float targetX = 2.6f;
    private float health;


    // Start is called before the first frame update
    void Start()
    {
        targetY = Random.Range(4.5f, -1f);
        tempTargetX  = Random.Range(-2f, 2f);
        speed = Random.Range(0.8f, 2f);
        fireRate = 0.9f;
        health = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if (Time.time >= nextTimeToShoot)
        {
            nextTimeToShoot = Time.time + 1 / fireRate;
            Shoot();
        }

        if (health <= 0)
        {
            Camera.main.GetComponent<ScoreManager>().IncreaseScore();

            Destroy(gameObject);
        }
    }

    private void Move()
    {
        Vector3 p = transform.position;
        if(Mathf.Abs(p.y - targetY) > 0.2f)
        {
            transform.position = Vector3.Lerp(p, new Vector3(tempTargetX, targetY, 0f), Time.deltaTime);
        }
        else
        {
            if(Mathf.Abs(Mathf.Abs(p.x) - Mathf.Abs(targetX)) < 0.2f)
            {
                targetX *= -1;
                speed *= -1;
            }

            transform.position = Vector3.Lerp(p, new Vector3(p.x + speed, p.y, 0f), Time.deltaTime);
        }

    }

    private void Shoot()
    {
        GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        newBullet.transform.Rotate(0f, 0f, 180f, Space.Self);

        BulletController bulletController = newBullet.GetComponent<BulletController>();
        bulletController.speed = bulletSpeed;
        bulletController.damage = bulletDamage;
        bulletController.direction = new Vector3(0,-1,0);
        bulletController.targetTag = "Player";

        Destroy(newBullet, 4f);
    }

    public void decreaseHealth(float amount)
    {
        health -= amount;
    }

    public void setDifficulty(float d)
    {
        health = startingHealth * d;
        bulletDamage *= d;
        fireRate = d * 0.9f;
        speed += d / 1.1f;
    }
}
