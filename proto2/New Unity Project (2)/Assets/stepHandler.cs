using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stepHandler : MonoBehaviour
{
    // duper simple step counter made by moi. saves a few bucks lol

    float lastmag = 0f;
    int timecounter = 0;
    string trend = "up";
    public float greyzone = 1.0f;
    float trendOrigin = 0f;

    public int stepCount = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 acc = Input.acceleration;
        float mag = acc.magnitude;

        // Debug.Log("DATA:");
        // Debug.Log(acc);
        // Debug.Log(mag);

        // Debug.Log(stepCount);

        if (timecounter > 0)
        {
            string direction = "up";

            if( mag - lastmag < 0)
            {
                direction = "down";
            }

            if( direction != trend)
            {
                if(Mathf.Abs(trendOrigin - mag) > greyzone)
                {
                    trendOrigin = mag;
                    trend = direction;

                    if (trend == "up") stepCount++;
                }
            }


        }


        timecounter++;
        lastmag = mag;
    }
}
