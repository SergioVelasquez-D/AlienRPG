using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public int cantidadDeCura = 10; // La cantidad de curación que otorga el cofre

    private void OnTriggerEnter(Collider other)
    {
        // Si el objeto colisiona con el personaje
        if (other.CompareTag("Player"))
        {
            // Aumentar la vida del personaje por la cantidad de curación del cofre
            //other.GetComponent<VidaDelPersonaje>().vida += cantidadDeCura;
            Debug.Log("Agarro el cofree");
            // Destruir el cofre
            Destroy(gameObject);
        }
    }
}