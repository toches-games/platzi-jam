using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NextRoomController : MonoBehaviour
{
    //[SerializeField] List<Transform> currentRooms = new List<Transform>();
    [SerializeField] List<Transform> levels = new List<Transform>();
    [SerializeField] int levelIndex = 0;

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

                    break;
                }
                else
                {
                    NextLevel();
                }
            }
        }

    }

    private void NextLevel()
    {
        levelIndex++;
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
