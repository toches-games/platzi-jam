using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextRoomController : MonoBehaviour
{
    //[SerializeField] List<Transform> currentRooms = new List<Transform>();
    [SerializeField] List<Transform> levels = new List<Transform>();
    [SerializeField] int levelIndex = 0;

    bool levelActive = false;

    private void Start()
    {
        InitGame();
    }

    public void NextRoom()
    {
        for (int i = 0; i < levels[levelIndex].GetChild(0).childCount ; i++)
        {
            if (levels[levelIndex].GetChild(0).GetChild(i).gameObject.activeSelf)
            {
                levels[levelIndex].GetChild(0).GetChild(i).gameObject.SetActive(false);

                if (i < levels[levelIndex].GetChild(0).childCount - 1)
                {
                    levels[levelIndex].GetChild(0).GetChild(i + 1).gameObject.SetActive(true);

                    PlayerController.SI.transform.position = levels[levelIndex]
                        .GetChild(0).GetChild(i + 1).Find("PlayerPosition")
                        .GetComponent<Transform>().position;
                    PlayerController.SI.ResetState();
                    levelActive = true;
                    break;
                }
                else
                {
                    NextLevel();
                }
                
            }
        }

        if (!levelActive)
        {
            levels[levelIndex].GetChild(0).GetChild(0).gameObject.SetActive(true);
        }
    }


    private void NextLevel()
    {
        levelIndex++;
        levelActive = false;
        PlayerController.SI.transform.position = levels[levelIndex]
                        .GetChild(0).GetChild(0).Find("PlayerPosition")
                        .GetComponent<Transform>().position;
        PlayerController.SI.ResetState();

        NextRoom();
        StopCoroutine(UIManager.SI.coroutine);
        UIManager.SI.ResetChoronometer();
        UIManager.SI.ShowCountDown(0);

        //currentRoom = levels[levelIndex].GetComponentInChildren<Transform>()
        //    .GetComponentsInChildren<Transform>().ToList();
    }

    public void InitGame()
    {
        levelIndex = 0;
        //currentRoom = levels[levelIndex].GetComponentInChildren<Transform>()
        //    .GetComponentsInChildren<Transform>().ToList();


    }
}
