using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class PlayerFloat : MonoBehaviour
 
 
{
// Position Storage Variables
Vector3 posOffset = new Vector3();
Vector3 tempPos = new Vector3();
public float amplitude = 0.5f;
public float frequency = 1f;
public float degreesPerSecond = 15.0f; //to rotate the object on Y axis
 
    void Start()
{
    // Store the starting position & rotation of the object
    posOffset = transform.position;
 
}
 
 
    void Update()
    {
 
        // Spin object around Y-Axis
        transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);
 
        //float up and down
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;
 
        transform.position = tempPos;
    }
}