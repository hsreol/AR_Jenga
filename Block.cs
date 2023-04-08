using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public GameManager Manager;
    public bool isEnter;
    public bool isLive;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if(!isLive) Destroy(gameObject);
        if (!isLive)
        {
            gameObject.tag = "dead";
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "drop")
        {
            Manager.GameOver();
        }

        else if (other.tag == "score")
        {
            isEnter = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "score")
        {
            isEnter = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isEnter && isLive)
        {
            Manager.score++;
            isLive = false;
        }

    }
}
