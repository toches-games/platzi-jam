using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlatformState
{
    empty,
    bit,
    some,
    medium,
    almost,
    full
}

public class UnstablePlatform : MonoBehaviour
{
    public PlatformState currentState;

    [SerializeField] List<Sprite> sprites;
    [SerializeField] SpriteRenderer sRenderer;

    private bool endChorometer = false;
    private Color playerColor;


    Coroutine coroutineCheck;

    // Start is called before the first frame update
    void Start()
    {
        playerColor = PlayerController.SI.GetComponent<SpriteRenderer>().color;
        ResetVariables();
    }

    public void SetState(int newState)
    {
        //Debug.Log(newState);
        currentState = (PlatformState)newState;

        switch (currentState)
        {
            case PlatformState.empty:
                sRenderer.sprite = sprites[0];
                break;
            case PlatformState.bit:
                sRenderer.sprite = sprites[1];
                ChangeColor(playerColor);

                break;
            case PlatformState.some:
                sRenderer.sprite = sprites[2];

                break;
            case PlatformState.medium:
                sRenderer.sprite = sprites[3];

                break;
            case PlatformState.almost:
                sRenderer.sprite = sprites[4];

                break;
            case PlatformState.full:
                sRenderer.sprite = sprites[5];

                GetComponent<BoxCollider2D>().enabled = false;
                StartCoroutine(Dead());
                break;
            default:
                break;
        }

    }

    public void NextState()
    {
        int state = (int)currentState;
        state++;
        SetState(state);
    }

    public IEnumerator Chronometer(int sec)
    {
        for (int i = 0; i < sec; i++)
        {
            yield return new WaitForSeconds(1);
            //Debug.Log(i);
        }

        endChorometer = true;

    }

    public IEnumerator Check()
    {
        for (int i = 0; i < 5; i++)
        {

            yield return StartCoroutine(Chronometer(1));

            if (!endChorometer)
            {
                yield break;
            }

            NextState();
            endChorometer = false;
        }
 
    }

    private IEnumerator Dead()
    {
        Color tmp = sRenderer.color;
        SpriteRenderer sRendererParent = GetComponentInParent<SpriteRenderer>();

        while(tmp.a > 0)
        {
            tmp.a -= 0.008f;
            sRenderer.color = tmp;
            sRendererParent.color = tmp;

            yield return new WaitForEndOfFrame();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            coroutineCheck = StartCoroutine(Check());
            NextState();
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            StopCoroutine(coroutineCheck);
        }
    }

    private void FixedUpdate()
    {
    }

    public void ResetVariables()
    {
        currentState = 0;
    }

    private void ChangeColor(Color color)
    {
        sRenderer.color = color;
    }
}
