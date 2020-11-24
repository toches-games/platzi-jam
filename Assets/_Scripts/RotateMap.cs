using System.Collections;
using UnityEngine;

public class RotateMap : MonoBehaviour
{
    public static RotateMap Instance;
    public int CurrentRotationCount { get; set; }

    [SerializeField] private float initialRotationSpeed = 1f;
    [SerializeField] private float rotateTime = 5f;
    [SerializeField] private float rotateDurationTime = 1f;
    [SerializeField] private int rotationDegrees = 90;
    [SerializeField] private int dizzyRotationCount = 3;
    //[SerializeField] Transform mintPositions = default;
    [SerializeField] private Transform mint = default;

    public int DizzyCount
    {
        get
        {
            return dizzyRotationCount;
        }
    }

    private Rigidbody2D rig;
    private float currentSpeed;
    private float targetSpeed;
    private float currentVelocity;
    private int[] directions = { -1, 1 };
    [SerializeField] private int direction;
    [SerializeField] bool randomDirection;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        rig = GetComponent<Rigidbody2D>();
    }

    private IEnumerator Start()
    {
        while (true)
        {
            targetSpeed = initialRotationSpeed;
            yield return new WaitForSeconds(rotateTime);
            if (randomDirection)
            {
                direction = directions[Random.Range(0, directions.Length)];
            }
            targetSpeed = (rig.rotation + rotationDegrees - rig.rotation) * rotateDurationTime * direction;
            yield return new WaitForSeconds(rotateDurationTime);

            if(PlayerController.SI.State == PlayerState.Normal)
            {
                CurrentRotationCount++;

                if(CurrentRotationCount >= dizzyRotationCount)
                {
                    //mint.position = mintPositions.GetChild(Random.Range(0, mintPositions.childCount)).position;
                    mint.gameObject.SetActive(true);
                    PlayerController.SI.State = PlayerState.Dizzy;
                }
            }
        }
    }

    private void Update()
    {
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref currentVelocity, 0.5f);
    }

    private void FixedUpdate()
    {
        rig.MoveRotation(rig.rotation - currentSpeed * Time.deltaTime);
    }
}
