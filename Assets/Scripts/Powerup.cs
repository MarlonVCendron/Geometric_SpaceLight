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
        }
    }

    private void applyEffect()
    {
        switch (powerupType)
        {
            case "health":
                playerController.addHealth(amountHealthPowerUp);
                PlayParticle(powerupType);
                break;
            case "rage":
                gunController.ActivateRagePower(durationRagePowerUp);
                PlayParticle(powerupType);
                break;
            case "shield":
                playerController.ActivateShieldPower(durationShieldPowerUp);
                Destroy(gameObject);
                break;
            default:
                Debug.Log("Unknown powerup");
                Destroy(gameObject);
                break;
        }

    }


    void PlayParticle(string name)
    {
        ParticleSystem p = null;
        switch (name)
        {
            case "health":
                p = transform.GetChild(1).GetComponent<ParticleSystem>();
                break;
            case "rage":
                p = transform.GetChild(2).GetComponent<ParticleSystem>();
                break;

        }

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);

        p.Play();
        Destroy(gameObject, p.duration);
    }
}
