using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int xPos;
    public int zPos;
    public int moveDiceValue;

    private Quaternion currentRotation = Quaternion.identity;
    private DicePlayer dicePlayer;

    void Start()
    {
        dicePlayer = GameObject.Find("Player Dice").GetComponent<DicePlayer>();
        xPos = (int)transform.position.x;
        zPos = (int)transform.position.z;
        currentRotation = transform.rotation;
    }

    
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (moveDiceValue > 0)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && zPos < 10)
            {
                currentRotation = Quaternion.Euler(0f, 0f, 0f);
                transform.rotation = currentRotation;
                transform.Translate(Vector3.forward, Space.World);
                zPos++;
                moveDiceValue--;
                dicePlayer.UpdateDice(moveDiceValue);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && zPos > 1)
            {
                currentRotation = Quaternion.Euler(0f, 180f, 0f);
                transform.rotation = currentRotation;
                transform.Translate(Vector3.back, Space.World);
                zPos--;
                moveDiceValue--;
                dicePlayer.UpdateDice(moveDiceValue);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && xPos < 10)
            {
                currentRotation = Quaternion.Euler(0f, 90f, 0f);
                transform.rotation = currentRotation;
                transform.Translate(Vector3.right, Space.World);
                xPos++;
                moveDiceValue--;
                dicePlayer.UpdateDice(moveDiceValue);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && xPos > 1)
            {
                currentRotation = Quaternion.Euler(0f, 270f, 0f);
                transform.rotation = currentRotation;
                transform.Translate(Vector3.left, Space.World);
                xPos--;
                moveDiceValue--;
                dicePlayer.UpdateDice(moveDiceValue);
            }
        }

    }
}
