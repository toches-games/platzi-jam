using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    Rigidbody2D rig;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            rig.velocity = new Vector2(rig.velocity.x, 10f);
        }
    }

    private void FixedUpdate()
    {
        rig.velocity = new Vector2(5f * Input.GetAxisRaw("Horizontal"), rig.velocity.y);
    }
}
