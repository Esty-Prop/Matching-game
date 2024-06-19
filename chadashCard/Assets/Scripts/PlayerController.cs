using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{

    private PairData pairDataSelected;
    private CardManager cardManager;
    private CardGameManager cardGameManager;
    private GameManager gameManager;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    /*[SerializeField] private*/public float playerSpeed = 5f;

    private float gravityValue = -9.81f;
    private Vector3 move;
    public int score = 0;
    public int value = 10;

    [SerializeField] private Animator animEyes;

    public event System.Action<PlayerInput> PlayerSelectGame;
    public InputAction selectAction = new InputAction();


    public bool isSelect = false;
    public bool isCorrect = false;
    public bool isStay = false;
    public enum playerName
    {
        הלוחכ,
        הדורו,
        //הקורי,
        //המותכ
    }

    private List<playerName> playerNamesList = new List<playerName>();
    public int indexPlayerTurn = 0;

    private List<Color> color = new List<Color>();
    public playerName thisplayerName = playerName.הלוחכ;

    public event System.Action<int> OnScoreChanged;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        cardManager = FindObjectOfType<CardManager>();
        controller = GetComponent<CharacterController>();

        //selectAction.AddBinding("<Gamepad>/buttonEast"/*, group: "Gamepad"*/);

        //moveForward.ApplyBindingOverride("<Gamepad>/dpad/right", group: "Gamepad");
        //moveForward.ApplyBindingOverride("<Keyboard>/l", group: "KB");
        //selectAction.AddBinding("<Gamepad>/buttonEast");
        //selectAction.AddBinding("<Gamepad>/buttonEast");
        //selectAction.performed += OnActionPerformed;
        //selectAction.Enable();
    }

    private void Start()
    {

        //gameManager.screanText.text = thisplayerName.ToString();

        Debug.Log(thisplayerName);


        cardGameManager = FindObjectOfType<CardGameManager>();
        cardGameManager.NextPlayerTurn += NextPlayer;

        playerNamesList.Add(playerName.הלוחכ);
        playerNamesList.Add(playerName.הדורו);
        //playerNamesList.Add(playerName.הקורי);
        //playerNamesList.Add(playerName.המותכ);
        color.Add(Color.blue);
        color.Add(Color.red);
        //color.Add(Color.green);
        //color.Add(Color.yellow);


        if (OnScoreChanged != null)
        {
            OnScoreChanged(score);
        }

        //selectAction.Enable();
        //selectAction.performed += context => SelectAction(context);
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }


        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }



        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        //Debug.Log(Gamepad.current);

        //if (playerNamesList[indexPlayerTurn] == thisplayerName)
        //{
            

        //}

    }

    public void OnMove(InputAction.CallbackContext context)
    {

        if (playerNamesList[indexPlayerTurn] == thisplayerName && cardGameManager.playerNum == 2)
        {
            Vector2 movement = context.ReadValue<Vector2>();
            move = new Vector3(movement.x, 0, movement.y);

        }


    }

    public void OnPlayerSelect(InputAction.CallbackContext context)
    {
        if (playerNamesList[indexPlayerTurn] == thisplayerName)
        {

            isSelect = true;
            animEyes.SetBool("Eyes", true);
            Debug.Log("player press" + context);
            Debug.Log(isSelect);

        }
        else
        {
            return;
        }
        StartCoroutine(Fallse());

    }

    //void SelectAction(InputAction.CallbackContext context)
    //{
    //    if (playerNamesList[indexPlayerTurn] == thisplayerName  )
    //    {
    //        isSelect = true;
    //        Debug.Log("player press" + context);
    //        Debug.Log(isSelect);
            
    //    }
    //    else
    //    {
    //        return;
    //    }
        
    //}

    

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Icon"))
        {
            other.gameObject.GetComponentInChildren<SpriteRenderer>().color = new Color(0.9977173f, 1f, 0.6924528f, 1f);/*color[indexPlayerTurn]*/;

        }


        if (other.gameObject.CompareTag("Icon") && isSelect == true)
        {

            pairDataSelected = other.GetComponent<MultyData>().pairData;
          
            isSelect = false;
            Debug.Log(pairDataSelected);
            cardManager.OnClickCard(pairDataSelected);

            if (cardManager.currectClick == true)
            {
                ScoreCH();
            }

            else
            {
                return;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Icon") )
        {
            
            other.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;

        }
    }

    

        public void ScoreCH()
    {

        this.IncreasScore(10);

    }

    public void IncreasScore(int value)
    {
        score += value;
        if(OnScoreChanged != null)
        {
            OnScoreChanged(score);
        }
        Debug.Log(score);
       
        gameManager.screanText.text = "תודוקנ 10 + " + thisplayerName;
        //cardGameManager.ScoreChanged?.Invoke();
        StartCoroutine(TextPlayer());

    }

    public void NextPlayer()
    {

        isSelect = false;
        cardGameManager.GoBack?.Invoke(indexPlayerTurn);

        isCorrect = true;
        

        Debug.Log("i am here" + indexPlayerTurn);
        indexPlayerTurn = (indexPlayerTurn + 1) % playerNamesList.Count;
        gameManager.SetTrue(indexPlayerTurn);
        gameManager.screanText.text = playerNamesList[indexPlayerTurn] + " הצובק";
        //cardGameManager.ScoreChanged?.Invoke();
        //gameManager.StartCoroutine(Winner());
        //gameManager.SetTrue(indexPlayerTurn);


    }

    private IEnumerator TextPlayer()
    {
        yield return new WaitForSeconds(2.5f);
        gameManager.screanText.text = playerNamesList[indexPlayerTurn] + " הצובק"; ;
        //cardGameManager.ScoreChanged?.Invoke();
    }

    private IEnumerator Fallse()
    {
        yield return new WaitForSeconds(0.2f);
        //gameManager.screanText.text = playerNamesList[indexPlayerTurn] + " הצובק";
        isSelect = false;
        animEyes.SetBool("Eyes", false);
        //cardGameManager.ScoreChanged?.Invoke();
    }
    // public void Settt()
    //{
    //    //selectAction.AddBinding("<Gamepad>/buttonEast");
    //    selectAction.AddBinding("<Gamepad>/buttonEast");

    //    //selectAction.Enable();
    //}

    //public void SetttFalse()
    //{
    //    //selectAction.AddBinding("<Gamepad>/buttonEast");

    //    selectAction.performed += OnActionPerformed;

    //}

    //public void OnActionPerformed(InputAction.CallbackContext e)
    //{
    //    //Debug.Log($"Action performed: {e.control.name}");
    //    selectAction.ChangeBinding(0).Erase();
    //}

}
