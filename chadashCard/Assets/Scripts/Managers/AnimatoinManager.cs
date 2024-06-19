using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatoinManager : MonoBehaviour
{
    [SerializeField] private Animator animFeadBack;
    //[SerializeField] private Animator animScore;
    [SerializeField] private CardGameManager cardGameManager;

    private void Start()
    {
        cardGameManager.OnCorrectClick += Correct10;
        cardGameManager.OnWrongClick += NotCorrect10;
        
    }

    public void Correct10()
    {
        animFeadBack.SetInteger("AnimNum", 1);
        StartCoroutine(StopAnim10());
    }
    public void NotCorrect10()
    {
        animFeadBack.SetInteger("AnimNum", 2);
        StartCoroutine(StopAnim10());
    }

    private IEnumerator StopAnim10()
    {
        yield return new WaitForSeconds(0.2f);
        animFeadBack.SetInteger("AnimNum", 0);
    }

    
}
