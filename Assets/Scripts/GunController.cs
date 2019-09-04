using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{

    public GameObject bullet;
    public float fireRate;
    public float speed;
    public float damage;
    bool buttonPressed;
    
    private float nextTimeToShoot = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // PROVISÓRIO
        buttonPressed = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (buttonPressed && Time.time >= nextTimeToShoot && !GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().dead)
        {
            nextTimeToShoot = Time.time + 1 / fireRate;
            Shoot();
        }
        
    }

    public void Shoot()
    {
        GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);

        BulletController bulletController = newBullet.GetComponent<BulletController>();
        bulletController.speed = speed;
        bulletController.damage = damage;
        bulletController.direction = new Vector3(0,1,0);
        bulletController.targetTag = "Enemy";
    }

    public void ActivateRagePower(float t)
    {
        StartCoroutine(coroutineRage(t));
    }

    public IEnumerator coroutineRage(float t)
    {
        fireRate *= 2f;
        yield return new WaitForSeconds(t);
        fireRate /= 2f;
    }

    public void buttonDown()
    {
        buttonPressed = true;
    }

    public void buttonUp()
    {
        buttonPressed = false;
    }
}
