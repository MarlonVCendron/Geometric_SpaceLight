using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{

    public string powerupType;

    public float amountHealthPowerUp = 1f;
    public float durationRagePowerUp = 4f;
    public float durationShieldPowerUp = 6f;
    
    private GameObject player;
    private PlayerController playerController;
    private GunController gunController;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        playerController = player.GetComponent<PlayerController>();
        gunController = player.GetComponent<GunController>();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            applyEffect();
            Destroy(gameObject);
        }
    }

    private void applyEffect()
    {
        switch (powerupType)
        {
            case "health":
                playerController.addHealth(amountHealthPowerUp);
                break;
            case "rage":
                gunController.ActivateRagePower(durationRagePowerUp);
                break;
            case "shield":
                playerController.ActivateShieldPower(durationShieldPowerUp);
                break;
            default:
                Debug.Log("Unknown powerup");
                break;
        }
    }


}
