using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelfAfterDelay : MonoBehaviour
{
    // The delay before the game object is destroyed
    public float delay = 2.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, delay);
    }

}
