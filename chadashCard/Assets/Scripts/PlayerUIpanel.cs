using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIpanel : MonoBehaviour
{
    public Text playerName;
    public Text playerScore;

    PlayerController player;
  
    public void AssignPlayer(int index)
    {
        StartCoroutine(AssignPlayerDelay(index)); 
    }

    IEnumerator AssignPlayerDelay(int index)
    {
        yield return new WaitForSeconds(0.01f);
        player = GameManager.instance.playerList[index].GetComponent<PlayerInputHandler>().playerController;
        SetUpInfoPanel();
    }

   
    void SetUpInfoPanel()
    {
      if(player != null)
        {
            player.OnScoreChanged += UpDateScore;
            playerName.text = player.thisplayerName.ToString();
        }
    }

   private void UpDateScore(int score)
    {
        playerScore.text = score.ToString();
    }
}
