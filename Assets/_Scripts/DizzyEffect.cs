using UnityEngine;
using UnityEngine.Rendering;

public class DizzyEffect : MonoBehaviour
{
    [SerializeField] private float speed = 1f;

    private Volume volume;
    private float targetValue;

    private void Awake()
    {
        volume = GetComponent<Volume>();
    }

    private void Update()
    {
        if (GameManager.SI.currentGameState != GameState.InGame)
        {
            return;
        }

        if (PlayerController.SI.State == PlayerState.Normal)
        {
            targetValue = 0f;
            volume.weight = Mathf.MoveTowards(volume.weight, targetValue, speed * Time.deltaTime);
            return;
        }

        if (GameManager.SI.currentGameState == GameState.GameOver) return;

        if (volume.weight == 1)
        {
            targetValue = 0.8f;
        }

        else if(volume.weight <= 0.8f)
        {
            targetValue = 1f;
        }

        volume.weight = Mathf.MoveTowards(volume.weight, targetValue, speed * Time.deltaTime);
    }
}
