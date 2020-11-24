using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NextRoomController : MonoBehaviour
{
    [SerializeField] List<Transform> currentRoom = new List<Transform>();
    [SerializeField] List<Transform> levels = new List<Transform>();
    [SerializeField] int levelIndex = 0;

    private void Start()
    {
        InitGame();
    }

    public void NextRoom()
    {
        for (int i = 0; i < currentRoom.Count; i++)
        {
            if (currentRoom[i].gameObject.activeInHierarchy)
            {
                currentRoom[i].gameObject.SetActive(false);

                PlayerController.SI.transform.position = levels[levelIndex]
                    .GetComponentInChildren<Transform>().GetChild(i).Find("PlayerPosition")
                    .GetComponent<Transform>().position;


                if (i != currentRoom.Count - 1)
                {
                    currentRoom[i + 1].gameObject.SetActive(true);
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
        currentRoom = levels[levelIndex].GetComponentInChildren<Transform>()
            .GetComponentsInChildren<Transform>().ToList();
    }

    public void InitGame()
    {
        levelIndex = 0;
        currentRoom = levels[levelIndex].GetComponentInChildren<Transform>()
            .GetComponentsInChildren<Transform>().ToList();

    }
}
