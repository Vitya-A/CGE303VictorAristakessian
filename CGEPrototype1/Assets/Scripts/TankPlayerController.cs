using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPlayerController : MonoBehaviour
{
    public float speed, turnSpeed, horizontalInput, verticalInput;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector2.right * Time.deltaTime * speed * verticalInput);

        // Reverses inputs if player is travelling backwards
        if (verticalInput < 0)
        {
            transform.Rotate(Vector3.forward, Time.deltaTime * -turnSpeed * horizontalInput);
        } else
        {
            transform.Rotate(Vector3.forward, Time.deltaTime * turnSpeed * horizontalInput);
        }
    }
}
