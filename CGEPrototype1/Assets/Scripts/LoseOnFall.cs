using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseOnFall : MonoBehaviour
{
    // Set this in the inspector
    public float lowestY;

    public PlayerHealth playerHealth;

    private void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        // If player is lower than death barrier...
        if (transform.position.y < lowestY)
        {
            // ...trigger player loss
            playerHealth.Die();
        }
    }
}
