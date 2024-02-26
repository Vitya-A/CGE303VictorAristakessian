using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseOnFall : MonoBehaviour
{
    // Set this in the inspector
    public float lowestY;
    
    // Update is called once per frame
    void Update()
    {
        // If player is lower than death barrier...
        if (transform.position.y < lowestY)
        {
            // ...Trigger player loss
            ScoreManager.gameOver = true;
        }
    }
}
