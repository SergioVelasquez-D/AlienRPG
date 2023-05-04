using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    // Variables that store the enemy's position
    public int xPos;
    public int zPos;
    public int live;
    public TextMeshProUGUI liveText;
    public int stamina;
    public TextMeshProUGUI staminaText;

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        // Update the start position of the element
        xPos = (int)transform.position.x;
        zPos = (int)transform.position.z;
        // Set the space taken to true
        gameManager.spaceTaken[xPos, zPos] = true;
        UpdateLive();
        UpdateStamina();
    }

    public void UpdateStamina()
    {
        staminaText.text = "Stamina: " + stamina;
    }

    public void UpdateLive()
    {
        liveText.text = "Live: " + live;
    }
}
