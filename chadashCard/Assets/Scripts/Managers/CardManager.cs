using System.Collections;
using UnityEngine.UI;
using UnityEngine;



public class CardManager : MonoBehaviour
{


    public bool firstImageSelected = false;
    public bool currectClick = false;

    [SerializeField] public GameObject[] sprite1;
    [SerializeField] private GameObject[] sprite2;

    [SerializeField] private Image feadBackCorrect;
    [SerializeField] private Image feadBackNotCorrect;

    private Sprite currectImg;


    [SerializeField] private PlayerController playerController;
    [SerializeField] private CardGameManager cardgameManager;
    [SerializeField] private GameManager gameManager;


    [SerializeField] private PairData[] pairData2;

   

    private int indexButtonClicked1;
    private int indexButtonClicked2;

    private int groopSelected;
  
    public int IndexButtonClicked1 => indexButtonClicked1;
    public int IndexButtonClicked2 => indexButtonClicked2;

    public int numClick = 0;

    public int setTRUE = 0;

    public void Start()
    {
        for (int i = 0; i < pairData2.Length; i++)
        {
            pairData2[i].Sprite2[cardgameManager.CorrectScene] = null;

        }
        for (int i = 0; i < pairData2.Length; i++)

        {
            if (i % 2 == 0)
            {
                pairData2[i].Sprite2[cardgameManager.CorrectScene] = sprite1[i].GetComponentInChildren<SpriteRenderer>().sprite;

            }
            else
            {
                pairData2[i].Sprite2[cardgameManager.CorrectScene] = sprite2[i].GetComponentInChildren<SpriteRenderer>().sprite;

            }

        }
       
    }
   

    public void OnClickCard(PairData pairData)
    {

        cardgameManager.OnClick?.Invoke();

        if (/*classManager.selectClass == true &&*/ firstImageSelected == false)
        {
            groopSelected = pairData.Group;

            currectClick = false;
            firstImageSelected = true;


            indexButtonClicked1 = pairData.IndexPair;


            if (pairData.Group == 1)
            {
                currectImg = sprite1[pairData.IndexPair].GetComponentInChildren<SpriteRenderer>().sprite;

                sprite1[pairData.IndexPair].GetComponentInChildren<SpriteRenderer>().color = Color.gray;
                sprite1[pairData.IndexPair].tag = "Finish";

                for (int i = 0; i < sprite1.Length; i++)
                {
                    sprite1[i].GetComponent<BoxCollider>().enabled = false;

                }

            }
            else 
            {
                currectImg = sprite2[pairData.IndexPair].GetComponentInChildren<SpriteRenderer>().sprite;

                sprite2[pairData.IndexPair].GetComponentInChildren<SpriteRenderer>().color = Color.gray;
                
                sprite2[pairData.IndexPair].tag = "Finish";
                for (int i = 0; i < sprite1.Length; i++)
                {
                    sprite2[i].GetComponent<BoxCollider>().enabled = false;

                }
            }

            currectImg = pairData.Sprite2[cardgameManager.CorrectScene];
           
            
        }

        else if (firstImageSelected == true && pairData.Group != groopSelected)
        {
            Debug.Log(pairData.IndexPair);
            indexButtonClicked2 = pairData.IndexPair;

            firstImageSelected = false;
            //classManager.selectClass = false;

            //if (pairData.Group == 2)
            //{
               

            //}
            //else 
            //{
            //    sprite1[pairData.IndexPair].GetComponentInChildren<SpriteRenderer>().color = Color.white;

            //}
         

            if (indexButtonClicked1 == indexButtonClicked2 )
            {
                Debug.Log("correct");
               
                sprite1[pairData.IndexPair].tag = "Finish";
                sprite2[pairData.IndexPair].tag = "Finish";

                feadBackNotCorrect.gameObject.SetActive(false);
                feadBackCorrect.gameObject.SetActive(true);
                //StartCoroutine(FeadBackKo());

                StartCoroutine(FeadBackCo());

                cardgameManager.CorrectClicks++;
                currectClick = true;
                cardgameManager.OnCorrectClick?.Invoke();
                StartCoroutine(correctBool());

                sprite1[pairData.IndexPair].GetComponentInChildren<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.25f); 
                sprite2[pairData.IndexPair].GetComponentInChildren<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.25f); 
               


            }
           
           
            else
            {

                Debug.Log("notCorrect");

               

                cardgameManager.OnWrongClick?.Invoke();
                currectClick = false;
                
               
                feadBackNotCorrect.gameObject.SetActive(true);
                feadBackCorrect.gameObject.SetActive(false);
                StartCoroutine(FeadBackKo());
                //sprite1[indexButtonClicked1].tag = "Icon";
                //sprite2[indexButtonClicked2].tag = "Icon";
                //sprite2[indexButtonClicked1].tag = "Icon";
                //sprite1[indexButtonClicked2].tag = "Icon";
                //sprite1[indexButtonClicked1].GetComponentInChildren<SpriteRenderer>().color = Color.white;
                //sprite2[indexButtonClicked2].GetComponentInChildren<SpriteRenderer>().color = Color.white;
                //sprite2[indexButtonClicked1].GetComponentInChildren<SpriteRenderer>().color = Color.white;
                //sprite1[indexButtonClicked2].GetComponentInChildren<SpriteRenderer>().color = Color.white;
                ////yield return new WaitForSeconds(4f);
                //feadBackNotCorrect.gameObject.SetActive(false);
                //feadBackCorrect.gameObject.SetActive(false);



            }

        }
        else if (firstImageSelected == true && pairData.Group == groopSelected)
        {
            //sprite1[indexButtonClicked1].tag = "Icon";
            //sprite2[indexButtonClicked2].tag = "Icon";

            //sprite1[indexButtonClicked1].GetComponentInChildren<SpriteRenderer>().color = Color.white;
            //sprite2[indexButtonClicked2].GetComponentInChildren<SpriteRenderer>().color = Color.white;

            //sprite1[indexButtonClicked1].tag = "Icon";
            //sprite2[indexButtonClicked2].tag = "Icon";
            //sprite2[indexButtonClicked1].tag = "Icon";
            //sprite1[indexButtonClicked2].tag = "Icon";
            //sprite1[indexButtonClicked1].GetComponentInChildren<SpriteRenderer>().color = Color.white;
            //sprite2[indexButtonClicked2].GetComponentInChildren<SpriteRenderer>().color = Color.white;
            //sprite2[indexButtonClicked1].GetComponentInChildren<SpriteRenderer>().color = Color.white;
            //sprite1[indexButtonClicked2].GetComponentInChildren<SpriteRenderer>().color = Color.white;


            cardgameManager.OnWrongClick?.Invoke();
            currectClick = false;
            Debug.Log("notCorrect");
            firstImageSelected = false;



            feadBackNotCorrect.gameObject.SetActive(true);
            feadBackCorrect.gameObject.SetActive(false);
            StartCoroutine(FeadBackKo());
        }
      
        numClick++;
        if (numClick == 2)
        {
            //setTRUE = (playerController.indexPlayerTurn ) % 2 /*444*/;
            //cardgameManager.GoBack?.Invoke();
            //gameManager.SetTrue(setTRUE);
            cardgameManager.NextPlayerTurn?.Invoke();
            numClick = 0;
            

}

    }


   public IEnumerator correctBool()
    {
        yield return new WaitForSeconds(0.05f);
        currectClick = false;
    }


    private IEnumerator FeadBackKo()
    {
        for (int i = 0; i < sprite1.Length; i++)
        {
            sprite1[i].GetComponent<BoxCollider>().enabled = true;
            sprite2[i].GetComponent<BoxCollider>().enabled = true;

        }

        yield return new WaitForSeconds(1.2f);
        sprite1[indexButtonClicked1].tag = "Icon";
        sprite2[indexButtonClicked2].tag = "Icon";
        sprite2[indexButtonClicked1].tag = "Icon";
        sprite1[indexButtonClicked2].tag = "Icon";
        sprite1[indexButtonClicked1].GetComponentInChildren<SpriteRenderer>().color = Color.white;
        sprite2[indexButtonClicked2].GetComponentInChildren<SpriteRenderer>().color = Color.white;
        sprite2[indexButtonClicked1].GetComponentInChildren<SpriteRenderer>().color = Color.white;
        sprite1[indexButtonClicked2].GetComponentInChildren<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(4f);
        feadBackNotCorrect.gameObject.SetActive(false);
        feadBackCorrect.gameObject.SetActive(false);
    }
    private IEnumerator FeadBackCo()
    {
        for (int i = 0; i < sprite1.Length; i++)
        {
            sprite1[i].GetComponent<BoxCollider>().enabled = true;
            sprite2[i].GetComponent<BoxCollider>().enabled = true;

        }

        yield return new WaitForSeconds(1.2f);
        //sprite1[indexButtonClicked1].tag = "Icon";
        //sprite2[indexButtonClicked2].tag = "Icon";
        //sprite2[indexButtonClicked1].tag = "Icon";
        //sprite1[indexButtonClicked2].tag = "Icon";
        //sprite1[indexButtonClicked1].GetComponentInChildren<SpriteRenderer>().color = Color.white;
        //sprite2[indexButtonClicked2].GetComponentInChildren<SpriteRenderer>().color = Color.white;
        //sprite2[indexButtonClicked1].GetComponentInChildren<SpriteRenderer>().color = Color.white;
        //sprite1[indexButtonClicked2].GetComponentInChildren<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(4f);
        feadBackNotCorrect.gameObject.SetActive(false);
        feadBackCorrect.gameObject.SetActive(false);
    }





}





























