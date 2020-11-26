using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager SI;

    [SerializeField] private List<GameObject> popUps = new List<GameObject>();
    [SerializeField] public int popUpIndex = 0;
    [SerializeField] private float waitTime = 2f;

    [SerializeField] private GameObject goalHab1;
    [SerializeField] private GameObject goalHab4;
    [SerializeField] private PlayableDirector nextRoom;
    [SerializeField] private GameObject chronometerHUD;
    [SerializeField] private GameObject livesHUD;
    [SerializeField] private GameObject barraHUD;

    private void Awake()
    {
        SI = SI == null ? this : SI;
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerController.SI.jumpSpeed = 0;
        StartCoroutine(InitTutorial());
    }

    private void Update()
    {
    }

    public IEnumerator InitTutorial()
    {
        yield return new WaitUntil(() => GameManager.SI.currentGameState == GameState.InGame);
        yield return new WaitForSeconds(1f);

        while (true)
        {
            yield return new WaitForEndOfFrame();

            for (int i = 0; i < popUps.Count; i++)
            {
                if (i == popUpIndex)
                {
                    popUps[i].SetActive(true);
                }
                else
                {
                    popUps[i].SetActive(false);

                }
            }

            if (popUpIndex == 0)
            {
                if (waitTime <= 0)
                {
                    popUpIndex++;
                    waitTime = 5;

                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
            else if (popUpIndex == 1)
            {
                PlayerController.SI.jumpSpeed = 15;

                if (PlayerController.SI.jumpInput)
                {
                    popUpIndex++;
                }
            }
            else if (popUpIndex == 2)
            {
                if (waitTime <= 0)
                {
                    //escala
                    popUpIndex++;
                    waitTime = 5;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
            else if (popUpIndex == 3)
            {
                //Ve a la sig habitacion
                goalHab1.SetActive(true);

                
                yield return new WaitUntil(() => nextRoom.time > 0.3);
                popUpIndex++;
            }
            else if (popUpIndex == 4)
            {
                GameManager.SI.ChangeGameState(GameState.Pause);
                if (waitTime <= 0)
                {
                    //Platf fija
                    popUpIndex++;
                    waitTime = 5;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }else if(popUpIndex == 5)
            {
                if (waitTime <= 0)
                {
                    //Platf inestable
                    popUpIndex++;
                    waitTime = 5;

                    livesHUD.SetActive(true);

                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
            else if (popUpIndex == 6)
            {
                if (waitTime <= 0)
                {
                    //Muerte
                    popUpIndex++;
                    waitTime = 5;

                    chronometerHUD.SetActive(true);
                    UIManager.SI.ShowCountDown(0);
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
            else if (popUpIndex == 7)
            {
                //Cronometro
                if (waitTime <= 0)
                {
                
                    popUpIndex++;
                    waitTime = 5;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }else if (popUpIndex == 8)
            {
                //PopUp Ve a la sig habitacion
                GameManager.SI.ChangeGameState(GameState.InGame);
                yield return new WaitUntil(() => nextRoom.time > 0.3);
                popUpIndex++;
            }
            else if (popUpIndex == 9)
            {
                //popUp Nuestro mundo va girando y cada cierto tiempo realiza un giron
                if (waitTime <= 0)
                {
                
                    popUpIndex++;
                    waitTime = 5;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
            else if (popUpIndex == 10)
            {
                //popUp cierto tiempo realiza un giron
                if (waitTime <= 0)
                {

                    popUpIndex++;
                    waitTime = 5;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
            else if (popUpIndex == 11)
            {
                //PopUp Ve a la siguiente habitacion

                yield return new WaitUntil(() => nextRoom.time > 0.3);
                popUpIndex++;
            }
            else if (popUpIndex == 12)
            {
                //popUp Los giros pueden ser aleatorios
                if (waitTime <= 0)
                {

                    popUpIndex++;
                    waitTime = 5;

                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
            else if (popUpIndex == 13)
            {
                //PopUp Si no avanzas rapido te vas a marear, 
                if (waitTime <= 0)
                {
                    popUpIndex++;
                    waitTime = 5;
                    barraHUD.SetActive(true);

                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
            else if (popUpIndex == 14)
            {
                //la barra superior te indica
                //cuando va a suceder
                if (waitTime <= 0)
                {
                    popUpIndex++;
                    waitTime = 10;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
            else if (popUpIndex == 15)
            {
                //PopUp Toma una menta para regresar a tu estado normal

            }else if(popUpIndex == 16)
            {

                goalHab4.SetActive(true);
                //PopUp Ve a la siguiente habitacion

                yield return new WaitUntil(() => nextRoom.time > 0.3);

                popUps[popUpIndex].SetActive(false);

                yield break;
               
            }
        }
    }
}
