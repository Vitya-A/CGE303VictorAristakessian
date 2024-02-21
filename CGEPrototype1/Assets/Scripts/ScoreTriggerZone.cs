using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTriggerZone : MonoBehaviour
{
    // Create a variable to keep track of whether the trigger zone is active
    bool active = true;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (active && collision.gameObject.tag == "Player")
        {
            // Deactivates trigger zone so player can only use once
            active = false;
            
            // Add 1 to the score when the player enters the trigger zone
            ScoreManager.score++;
        }
    }
}
