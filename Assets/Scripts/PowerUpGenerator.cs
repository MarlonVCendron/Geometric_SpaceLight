using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpGenerator : MonoBehaviour
{
       

    public float timeBetweenPowerups;
    public GameObject powerup;

    private string[] PowerupTypes = {"health", "rage", "shield"};
    private float powerupCountdown;
    private float searchCountdown;

    void Start()
    {
        powerupCountdown = timeBetweenPowerups;
    }

    void Update()
    {
        if(powerupCountdown <= 0f)
        {
            spawnPowerup();
        }
        else
        {
            powerupCountdown -= Time.deltaTime;
        }
    }

    private void spawnPowerup()
    {
        if (!isPowerupSpawned())
        {
            float randX = Random.Range(-2, 2);
            float randY = Random.Range(-3, 4);

            GameObject newPowerup = Instantiate(powerup, new Vector2(randX, randY), Quaternion.identity);
            Powerup powerupScript = newPowerup.GetComponent<Powerup>();
            powerupScript.powerupType = getRandomPowerupType();
        }
    }

    private string getRandomPowerupType()
    {
        int index = Random.Range(0, PowerupTypes.Length);
        return PowerupTypes[index];
    }

    bool isPowerupSpawned()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectsWithTag("Powerup").Length == 0f)
            {
                return false;
            }
        }

        return true;
    }


}
