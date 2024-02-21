using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    // Ctrl + Shift + Alt + F2 => opens Unity Scripting Reference
    
    // Set this reference in the inspector
    public TMP_Text output;

    // Enter the text you want to display in the inspector
    public string textToDisplay;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log("Triggered by " + collision.gameObject.name);

        // Set the Player tag on the player in the inspector
        if (collision.gameObject.tag == "Player")
        {
            output.text = textToDisplay;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
