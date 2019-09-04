using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpGenerator : MonoBehaviour
{
       
    public enum SpawnState {WAITING, COUNTING}; 

    public float timeBetweenPowerups;
    public GameObject powerup;
    public SpawnState state = SpawnState.COUNTING;

    private string[] PowerupTypes = {"health", "rage", "shield"};
    private float powerupCountdown;
    private float searchCountdown;

    void Start()
    {
        powerupCountdown = timeBetweenPowerups;
    }

    void Update()
    {
        if (state == SpawnState.WAITING)
        {
            if (!isPowerupSpawned())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if(powerupCountdown <= 0)
        {
            SpawnPowerup();
        }
        else
        {
            powerupCountdown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        state = SpawnState.COUNTING;
        powerupCountdown = timeBetweenPowerups;
    }


    private void SpawnPowerup()
    {
        float randX = Random.Range(-2, 2);
        float randY = Random.Range(-3, 4);

        GameObject newPowerup = Instantiate(powerup, new Vector2(randX, randY), Quaternion.identity);
        Powerup powerupScript = newPowerup.GetComponent<Powerup>();
        powerupScript.powerupType = getRandomPowerupType();

        state = SpawnState.WAITING;
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
