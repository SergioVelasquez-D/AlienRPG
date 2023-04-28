using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DicePlayer : MonoBehaviour
{
    private Button diceButton;
    private PlayerController playerController;
    public TextMeshProUGUI diceNumber;

    void Start()
    {
        diceButton = GetComponent<Button>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        diceButton.onClick.AddListener(DiceThrow);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DiceThrow()
    {
        int throwValue = Random.Range(0, 4);
        Debug.Log(throwValue);
        UpdateDice(throwValue);
        playerController.moveDiceValue = throwValue;
    }

    public void UpdateDice(int numberToShow)
    {
        switch (numberToShow) 
        {
            case 1:
                diceNumber.text = "•";
                break;
            case 2:
                diceNumber.text = "••";
                break;
            case 3:
                diceNumber.text = "•••";
                break;
            case 4:
                diceNumber.text = "••\n••";
                break;
            default:
                diceNumber.text = "-";
                break;

        }

        
    }

}
