using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetBtn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        print("This scene has been loaded");
    }
     
    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetScene()
    {
        StartCoroutine(Reset());
    }

    IEnumerator Reset()
    {
        SceneManager.LoadScene("Jenga");
        yield return new WaitForEndOfFrame();
    }
}
