using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OpenCvSharp.Demo;
using Intel.RealSense;
using System;

public class HandMoving : MonoBehaviour
{
    HandDetection hand;

    public GameObject etsstes;

    // Start is called before the first frame update
    void Start()
    {
        hand = GameObject.Find("RawImage").GetComponent<HandDetection>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        try
        {
            if(etsstes.GetComponent<RsDevice>().map != null)
            {
                float x = (hand.cX - 160) / 6 + 5;
                float y = 18 - (hand.cY / 8);
                float z = (etsstes.GetComponent<RsDevice>().map[640 - hand.cX * 2 - 1, 480 - (int)(hand.cY * 2.6) - 1] - 1) * (-20) - 10;

                Vector3 move = new Vector3((int)x, (int)y, (int)z);
                transform.position = Vector3.MoveTowards(transform.position, move, 0.5f);
            }
            //Debug.Log("Width : " + etsstes.GetComponent<RsDevice>().map.GetLength(0));
            //Debug.Log("Height : " + etsstes.GetComponent<RsDevice>().map.GetLength(1));
            //Debug.Log("x: " + x + "y: " + y + "z: " + z);
        }
        catch(Exception e)
        {
            Debug.LogException(e, this);
        }
    }

    /*IEnumerator ScreenShotAndSpoid()
    {
        //스크린샷을 찍어, 그것을 Texture2D로 반환시키고 그곳에서 색을 추출!!
        Texture2D tex = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        yield return new WaitForEndOfFrame();
        tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        tex.Apply();

        //추출된 색
        Color color = tex.GetPixel(940 - hand.cX, 360 - hand.cY);

        Debug.Log("x" + hand.cX + "y" + hand.cY + "color.r" + color.ToString());
    }*/
}
