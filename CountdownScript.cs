using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownScript : MonoBehaviour
{
    [SerializeField] private Text uiText;
    [SerializeField] private float mainTimer;


    private float timer;
    private bool canCount = true;
    private bool doOnce = false;

    // Start is called before the first frame update
    void Start()
    {
        timer = mainTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer>=0.0f && canCount)
        {
            timer -= Time.deltaTime;
            uiText.text = timer.ToString("F0");
        }
        else if(timer <=0.0f && !doOnce)
        {
            canCount = false;
            doOnce = true;
            uiText.text = "0";
            timer = 0.0f;
        }
    }

    public void ResetBtn()
    {
        timer = mainTimer;
        canCount = true;
        doOnce = false;
    }
}