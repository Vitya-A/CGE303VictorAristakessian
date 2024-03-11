using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public static bool onJumpPad;

    private void Start()
    {
        onJumpPad = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Determines if the player has stepped on a jump pad
        if (collision.gameObject.tag == "Player")
        {
            onJumpPad = true;
        }
    }

    //private void Update()
    //{
    //    onJumpPad = false;
    //}
}
