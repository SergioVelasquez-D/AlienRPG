using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum ActiveTurn { Player, Enemy }

public class GameManager : MonoBehaviour
{
    public ActiveTurn activeTurn;
    public TextMeshProUGUI activeTurnText;
    private DicePlayer dicePlayer;
    private DiceEnemy diceEnemy;
    //private AttackDicePlayer attackDicePlayer;
    public bool[,] spaceTaken = new bool[11, 11]; // Matrix of control of spaces taken

    void Start()
    {
        dicePlayer = GameObject.Find("Player Dice").GetComponent<DicePlayer>();
        diceEnemy = GameObject.Find("Enemy Dice").GetComponent<DiceEnemy>();
        //attackDicePlayer = GameObject.Find("Attack Dice Player").GetComponent<AttackDicePlayer>();
        diceEnemy.gameObject.SetActive(false);
        //attackDicePlayer.gameObject.SetActive(false);
        activeTurnText.text = "Player Turn";
        //PrintTakenSpaces();
    }

    // Method to switch turn
    public void SetTurn()
    {
        if (activeTurn == ActiveTurn.Player)
        {
            activeTurn = ActiveTurn.Enemy;
            dicePlayer.gameObject.SetActive(false);
            diceEnemy.gameObject.SetActive(true);
            activeTurnText.text = "Enemy Turn";
            //PrintTakenSpaces();
        }
        else if(activeTurn == ActiveTurn.Enemy)
        {            
            activeTurn = ActiveTurn.Player;
            diceEnemy.gameObject.SetActive(false);
            dicePlayer.gameObject.SetActive(true);
            dicePlayer.hasThrow = false;
            activeTurnText.text = "Player Turn";
            //PrintTakenSpaces();
        }
    }

    // Prints the spaces taken
<<<<<<< Updated upstream
    public void PrintTakenSpaces()
    {
        // Loop through all the X spaces of the matrix
        for (int i = 1; i < spaceTaken.GetLength(0); i++)
        {
            // Loop through all the Z spaces of the matrix
            for (int j = 1; j < spaceTaken.GetLength(1); j++)
            {
                // Check if the space is taken
                if (spaceTaken[i, j] == true)
                {
                    Debug.Log("La casilla en X: " + i + " y Z: " + j + " está ocupada.");
                }
            }
        }
    }
=======
    //public void PrintTakenSpaces()
    //{
    //    // Loop through all the X spaces of the matrix
    //    for (int i = 1; i < spaceTaken.GetLength(0); i++)
    //    {
    //        // Loop through all the Z spaces of the matrix
    //        for (int j = 1; j < spaceTaken.GetLength(1); j++)
    //        {
    //            // Check if the space is taken
    //            if (spaceTaken[i, j] == true)
    //            {
    //                Debug.Log("La casilla en X: " + i + " y Z: " + j + " estÃ¡ ocupada.");
    //            }
    //        }
    //    }
    //}
>>>>>>> Stashed changes

 
}
