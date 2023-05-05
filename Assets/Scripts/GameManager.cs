using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum ActiveTurn { Player, Enemy }

public class GameManager : MonoBehaviour
{
    public ActiveTurn activeTurn;
    public TextMeshProUGUI activeTurnText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI playerWinText;
    public Button restartBtn;
    private DicePlayer dicePlayer;
    private DiceEnemy diceEnemy;
    private PlayerController playerController;
    private Enemy enemy;

    public bool[,] spaceTaken = new bool[11, 11]; // Matrix of control of spaces taken

    public bool gameOver;

    void Start()
    {
        dicePlayer = GameObject.Find("Player Dice").GetComponent<DicePlayer>();
        diceEnemy = GameObject.Find("Enemy Dice").GetComponent<DiceEnemy>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        enemy = GameObject.Find("Enemy").GetComponent<Enemy>();
        diceEnemy.gameObject.SetActive(false);
        activeTurnText.text = "Player Turn";
        //PrintTakenSpaces();
    }

    private void Update()
    {
        EndGame();
    }

    void EndGame()
    {
        if (playerController.live <= 0)
        {
            gameOver = true;
            GameOver();
        }
        else if (enemy.live <= 0)
        {
            PlayerWin();
        }
    }

    // Method to switch turn
    public void SetTurn()
    {
        if (activeTurn == ActiveTurn.Player)
        {
            activeTurn = ActiveTurn.Enemy;
            dicePlayer.gameObject.SetActive(false);
            diceEnemy.gameObject.SetActive(true);
            diceEnemy.DiceThrow();
            activeTurnText.text = "Enemy Turn";
            //PrintTakenSpaces();
        }
        else if (activeTurn == ActiveTurn.Enemy)
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
    //                Debug.Log("La casilla en X: " + i + " y Z: " + j + " está ocupada.");
    //            }
    //        }
    //    }
    //}

    void GameOver()
    {
        restartBtn.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
    }

    void PlayerWin()
    {
        restartBtn.gameObject.SetActive(true);
        playerWinText.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
