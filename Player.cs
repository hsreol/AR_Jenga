using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool isChoice = false;

    GameObject cube;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isChoice)
        {
            cube.transform.position = transform.position;
        }
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("drag"))
        {
            isChoice = true;

            cube = other.collider.gameObject;
        }

        else if(other.collider.CompareTag("score"))
        {
            isChoice = false;
        }
    }
}
