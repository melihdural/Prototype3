using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    //The point where the gameobjects leaving the scene will disappear
    private float leftBound = -20f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < leftBound)
        {
            Destroy(gameObject);
        }
    }
}
