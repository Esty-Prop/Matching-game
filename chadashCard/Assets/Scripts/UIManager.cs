using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class UIManager : MonoBehaviour
{
    public GameObject[] playerUIPanel;
    public GameObject[] joinMessages;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.PlayerJoinedGame += PlayerJoinedGame;
        GameManager.instance.PlayerLeftGame += PlayerLeftGame;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayerJoinedGame(PlayerInput playerInput)
    {
        ShowUIpanel(playerInput);
    }

    void PlayerLeftGame(PlayerInput playerInput)
    {
        HideUIpanel(playerInput);
    }

    void ShowUIpanel(PlayerInput playerInput)
    {
        playerUIPanel[playerInput.playerIndex].SetActive(true);
        playerUIPanel[playerInput.playerIndex].GetComponent<PlayerUIpanel>().AssignPlayer(playerInput.playerIndex);
        joinMessages[playerInput.playerIndex].SetActive(false);
    }


    void HideUIpanel(PlayerInput playerInput)
    {
        playerUIPanel[playerInput.playerIndex].SetActive(false);
        joinMessages[playerInput.playerIndex].SetActive(true);
    }
}
