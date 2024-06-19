using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
   
    [SerializeField] private CardGameManager cardGameManager;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] sounds;


    void Start()
    {
        cardGameManager.OnCorrectClick += Correct4;
        cardGameManager.OnWrongClick += NotCorrect4;
        cardGameManager.OnPlayerJoin += PlayerJoin;
        cardGameManager.OnPlayerMove += PlayerMove;
        cardGameManager.OnClick += PlayerClick;
        audioSource.clip = sounds[2];
        audioSource.Play();
    }

    private void PlayerJoin()
    {
        audioSource.clip = sounds[3];
        audioSource.Play();
        //StartCoroutine(StopAudio());
    }

    private void PlayerClick()
    {
        audioSource.clip = sounds[5];
        audioSource.Play();
        //StartCoroutine(StopAudio());
    }

    private void PlayerMove()
    {
        audioSource.clip = sounds[4];
        audioSource.Play();
        //StartCoroutine(StopAudio());
    }

    private void Correct4()
    {
      
        StartCoroutine(Correct5());
        
    }

    private IEnumerator Correct5()
    {
        yield return new WaitForSeconds(0.5f);
        audioSource.clip = sounds[0];
        audioSource.Play();
        //StartCoroutine(StopAudio());
    }


    private void NotCorrect4()
    {
        StartCoroutine(NotCorrect5());
        
    }
    private IEnumerator NotCorrect5()
    {
        yield return new WaitForSeconds(0.5f);
        audioSource.clip = sounds[1];
        audioSource.Play();
        //StartCoroutine(StopAudio());


    }
    //private IEnumerator StopAudio()
    //{
    //    yield return new WaitForSeconds(8f);
    //    audioSource.clip = sounds[2];
    //    audioSource.Play();



    //}

}
