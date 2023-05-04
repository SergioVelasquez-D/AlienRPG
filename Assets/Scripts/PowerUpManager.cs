using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public PowerUpType powerupType;
    public PlayerController playerController;

    public void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
           
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                Debug.Log("Caja colisionó con el jugador");
                switch (powerupType.type)
                {
                    case PowerUpType.Type.Damage:
                        // Reduce la vida del jugador según el valor del powerup

                        break;
                    case PowerUpType.Type.Heal:
                        // Aumenta la vida del jugador según el valor del powerup
                        break;
                    case PowerUpType.Type.Stamina:
                        // Aumenta la stamina del jugador según el valor del powerup
                        break;
                    default:
                        break;
                }
                // Destruye el powerup después de que el jugador lo haya recogido
                //Destroy(gameObject);
                StartCoroutine(DestroyCrate());
            }
        }
    }

    IEnumerator DestroyCrate()
    {
        yield return new WaitForSeconds(0.5f); // Esperar medio segundo
        Destroy(gameObject); // Destruir el objeto
    }
}

