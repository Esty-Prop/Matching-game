using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private CardGameManager cardGameManager;
    private PlayerController playerController;
    public GameObject[] spawnPoints;
    public static GameManager instance = null;

    public List<PlayerInput> playerList = new List<PlayerInput>();
    //public List<InputDevice> playerDevice = new List<InputDevice>();

    public event System.Action<PlayerInput> PlayerJoinedGame;
    public event System.Action<PlayerInput> PlayerLeftGame;
  
    [SerializeField] InputAction joinAction;
    
    [SerializeField] InputAction leaveAction;

    [SerializeField] private GameObject winManager;
    [SerializeField] private Image winImage;
    [SerializeField] private Image groupImg;
    [SerializeField] private Sprite[] groupImges;
    [SerializeField] private Text scoreWinText;
    [SerializeField] private Text nameWinText;
    public Text screanText;
    //[SerializeField] private Image ourLogoImage;
    //[SerializeField] private Button exit;

    Vector3 currentPos;

    private int index1;
    private int index2;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
        spawnPoints = GameObject.FindGameObjectsWithTag("Spawen");

        joinAction.Enable();
        joinAction.performed += context => JoinAction(context);
        leaveAction.Enable();
        leaveAction.performed += context => LeaveAction(context);
        cardGameManager = FindObjectOfType<CardGameManager>();
        //groupImg = null;

        //StartCoroutine(Winner());
    }




    private void Start()
    {
       
        //PlayerInputManager.instance.JoinPlayer(0, -1, null);
       cardGameManager = FindObjectOfType<CardGameManager>();
        playerController = FindObjectOfType<PlayerController>();
        cardGameManager.GoBack += PlayerGoBack;

        
     }



   
    void OnPlayerJoined(PlayerInput playerInput)
    {
        playerList.Add(playerInput);
        if (PlayerJoinedGame != null)
        {
            PlayerJoinedGame(playerInput);
            //Debug.Log(Gamepad.current);
            //playerDevice.Add(Gamepad.current);
            //Debug.Log(playerDevice);

            
        }

    
        cardGameManager.OnPlayerJoin?.Invoke();
    }

    void OnPlayerLeft(PlayerInput playerInput)
    {

    }

   

    void JoinAction(InputAction.CallbackContext context)
    {
        PlayerInputManager.instance.JoinPlayerFromActionIfNotAlreadyJoined(context);

    }

   

    void LeaveAction(InputAction.CallbackContext context)
    {
        if(playerList.Count > 1)
        {
            foreach (var player in playerList)
            {
                foreach (var device in player.devices)
                {
                    if (device != null && context.control.device == device)
                    {
                        UnregisterPlayer(player);
                        return;
                    }
                }
                
            }
        }
           
    }

    void UnregisterPlayer(PlayerInput playerInput)
    {
        playerList.Remove(playerInput);
        if(PlayerLeftGame != null)
        {
            PlayerLeftGame(playerInput);
        }
        Destroy(playerInput.transform.parent.gameObject);
        
    }

    public void PlayerGoBack(int index)
    {
        //StartCoroutine(SetGoBack(index));
        playerList[index].gameObject.GetComponentInParent<CharacterController>().enabled = false;
        //playerList[index].gameObject.GetComponentInParent<PlayerController>().SetttFalse();

        //action.ChangeBindingWithId(...).Erase();
        //action.ChangeBindingWithPath(...).Erase();
        //action.ChangeBindingWithGroup(...).Erase();
        //action.ChangeBinding(0).Erase();



        playerList[index].transform.parent.rotation = Quaternion.Euler(0, -90, 0);
        playerList[index].transform.parent.position = spawnPoints[index].transform.position;

    }

    public IEnumerator SetGoBack(int index)
    {
        yield return new WaitForSeconds(1f);
        //playerList[index].gameObject.GetComponentInParent<PlayerController>().selectAction.ChangeBinding(0).Erase();
        playerList[index].gameObject.GetComponentInParent<CharacterController>().enabled = false;
        //playerList[index].gameObject.GetComponentInParent<PlayerController>().SetttFalse();

        //action.ChangeBindingWithId(...).Erase();
        //action.ChangeBindingWithPath(...).Erase();
        //action.ChangeBindingWithGroup(...).Erase();
        //action.ChangeBinding(0).Erase();



        playerList[index].transform.parent.rotation = Quaternion.Euler(0, -90, 0);
        playerList[index].transform.parent.position = spawnPoints[index].transform.position;
    }

        public void SetTrue(int index)
    {
       
        StartCoroutine(SetActiveParent(index));
    }

    public IEnumerator SetActiveParent(int index)
    {
        //playerList[index].gameObject.GetComponentInParent<PlayerController>().selectAction.AddBinding("<Gamepad>/buttonEast");
        playerList[index].gameObject.GetComponentInParent<PlayerController>().playerSpeed = 0;



        playerList[index].transform.parent.rotation = Quaternion.Euler(0, 0, 0);
        playerList[index].transform.parent.position = spawnPoints[index].transform.position;

        //playerList[index].gameObject.GetComponentInParent<PlayerController>().selectAction.AddBinding("<Gamepad>/buttonEast");


        playerList[index].gameObject.GetComponentInParent<CharacterController>().enabled = true;
                yield return new WaitForSeconds(2f);
        playerList[index].gameObject.GetComponentInParent<PlayerController>().playerSpeed = 5;
        

    }
    public void Winner()
    {

        StartCoroutine(Win());
    }

    
    public IEnumerator Win()
    {
        yield return new WaitForSeconds(2f);
        int max = playerList[0].gameObject.GetComponentInParent<PlayerController>().score;
        string maxName = playerList[0].gameObject.GetComponentInParent<PlayerController>().thisplayerName.ToString();

        int max1 = 0;
        string maxName1 = "";

        for (int i = 1; i < playerList.Count; i++)
        {


            if (playerList[i].gameObject.GetComponentInParent<PlayerController>().score == max)
            {
                Debug.Log("2222222222222222222222222222222222222222222222222222");
                max1 =  playerList[i].gameObject.GetComponentInParent<PlayerController>().score;
                maxName1 =  playerList[i].gameObject.GetComponentInParent<PlayerController>().thisplayerName.ToString();
            
            }

           

            else if (playerList[i].gameObject.GetComponentInParent<PlayerController>().score >= max)
            {
                max = playerList[i].gameObject.GetComponentInParent<PlayerController>().score;
                maxName = playerList[i].gameObject.GetComponentInParent<PlayerController>().thisplayerName.ToString();
            }

        }

       if( max == max1 )
      
       {
            Debug.Log("2222222222222222222222WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW");
            
            nameWinText.text = maxName + " " + maxName1 ;
            scoreWinText.text = "תודוקנ  " + max.ToString();

           

       }

       else
       {
            Debug.Log(maxName + " score" + max);

            nameWinText.text = maxName;
            scoreWinText.text = "תודוקנ  " + max.ToString();
       }



        winManager.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);

        scoreWinText.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);
        winImage.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        winImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        winImage.gameObject.SetActive(false);

        StartCoroutine(OurLogo());
    }

    public IEnumerator OurLogo()
    {
        yield return new WaitForSeconds(40f);
        SceneManager.LoadScene(2);
      
    }

    
}
