using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowPlayer : MonoBehaviour
{
    // Set this reference in the inspector
    public GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Set the camera's position to that of the player without changing its z-position
        transform.position = new Vector3 (
            player.transform.position.x,
            player.transform.position.y,
            transform.position.z);
    }
}
