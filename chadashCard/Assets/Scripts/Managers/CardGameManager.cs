using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;

using UnityEngine;



public class CardGameManager : MonoBehaviour
{
    public UnityAction OnClick;
    public UnityAction OnPlayerMove;
    public UnityAction OnPlayerJoin;
    public UnityAction OnWrongClick;
    public UnityAction OnCorrectClick;
    public UnityAction SelectClass;
    //public UnityAction<ClassData> ClassSelected;
    public UnityAction<int> OnGift;
    public UnityAction Movilclass;
    public UnityAction TextsetActive;
    public UnityAction ViewClass;
    public UnityAction Leadingclass;
    public UnityAction GroupsTurn;
    public UnityAction NextPlayerTurn;
    public UnityAction ScoreChanged;
    public UnityAction<int> GoBack;

    [SerializeField] private GameObject allIcons1;
    [SerializeField] private GameObject allIcons2;
    [SerializeField] private GameObject allIcons3;
    //[SerializeField] private GameObject Winner;

    [SerializeField] private int correctScene;

    [SerializeField] private GameManager gameManager;
    public int playerNum = 0;
    [SerializeField] private  GameObject lockImage;

    public int CorrectScene
    {
        get { return correctScene; }
        set
        {
            correctScene = value;
        }
    }

    private bool setActiveText;
    private int correctClicks = 0;
    public bool SetActiveText
    {
        get { return setActiveText; }
        set
        {
            setActiveText = value;
        }
    }


    

    public int CorrectClicks
    {
        get { return correctClicks; }
        set
        {
            correctClicks = value;
            if (correctClicks >= 27)
            {

                NewScene3();

            }

            else if (correctClicks >= 18)
            {

                NewScene2();

            }

           else if (correctClicks >= 9)
            {

                NewScene();

            }

           
           

        }
    }

    private void Start()
    {
        OnPlayerJoin += PlayerJoin;
    }

    private void PlayerJoin()
    {
        playerNum++;
        if(playerNum == 2)
        {
            lockImage.SetActive(false);
            //gameManager.screanText.text = "мезл";
        }
    }


    public void NewScene()
    {

        allIcons1.SetActive(false);
        allIcons2.SetActive(true);

    }

    public void NewScene2()
    {

        
        allIcons2.SetActive(false);
        allIcons3.SetActive(true);

    }

    public void NewScene3()
    {

       
        allIcons3.SetActive(false);

        gameManager.Winner();

    }




}

