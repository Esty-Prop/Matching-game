using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public GameObject[] playerPrefab;
    public PlayerController playerController;
    Vector3 startPos = new Vector3(0, 0, 0);
    private CardGameManager cardGameManager;
    private PlayerController playerControllers;

    private void Awake()
    {

        if (playerPrefab != null)
        {
            playerController = GameObject.Instantiate
                (playerPrefab[GetComponent<PlayerInput>().playerIndex],
                GameManager.instance.spawnPoints[GetComponent<PlayerInput>().playerIndex].transform.position,
                transform.rotation).GetComponent<PlayerController>();
            transform.parent = playerController.transform;
            transform.position = playerController.transform.position;
        }
    }

    private void Start()
    {
        
        playerControllers = FindObjectOfType<PlayerController>();
        cardGameManager = FindObjectOfType<CardGameManager>();
        cardGameManager.NextPlayerTurn += NextPlayer;


    }
    public void OnMove(InputAction.CallbackContext context)
    {
       
            playerController.OnMove(context);
        
        
    }

    public void OnSelect(InputAction.CallbackContext context)
    {

        playerController.OnPlayerSelect(context);


    }

    public void NextPlayer()
    {

        
              
       
    }

   
}
