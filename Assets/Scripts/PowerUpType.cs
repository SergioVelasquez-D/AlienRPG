using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpType : MonoBehaviour
{
    public PlayerController playerController;
    public enum Type
    {
      None,
      Damage,
      Heal,
      Stamina
    }

    public Type type;
    public bool hasPowerUp = false;
    int value;

    public PowerUpType(Type type, bool hasPowerUp, float value)
    {
        playerController = GetComponent<PlayerController>();

        
        //powerup Heal
        PowerUpType healPowerup = new PowerUpType(PowerUpType.Type.Heal, hasPowerUp, 5); 

        //powerup Damage

        PowerUpType damagePowerup = new PowerUpType(PowerUpType.Type.Damage, hasPowerUp, -5);

        //powerup Stamina
        PowerUpType staminaPowerup = new PowerUpType(PowerUpType.Type.Stamina, !hasPowerUp, 0);

    }
}
