using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class pointerscript : MonoBehaviour
{
    public GameObject targetObj;
    public GameObject gameManager;
    Vector3 targetEuler;

    public GameObject centerCube;
    public GameObject playerCube;
    public GameObject targetCube;

    float hearingThreshold = 50f;
    float pingThreshold = 20f;

    AudioSource leftSignal;
    AudioSource rightSignal; 
    AudioSource leftSignal2;
    AudioSource rightSignal2;

    AudioSource blaster;

    float faderate = 0.01f;

    public bool active = false;

    float ghostdir = 1f;

    // danke danke https://www.raywenderlich.com/532-audio-tutorial-for-unity-the-audio-mixer 
    public AudioMixer chorusmixer;
    // Start is called before the first frame update
    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        blaster = gameObject.GetComponent<AudioSource>();

        AudioSource[] sources = GameObject.Find("LR_effect").GetComponents<AudioSource>();
        leftSignal = sources[0];
        rightSignal = sources[1];
        leftSignal.volume = 0f;
        rightSignal.volume = 0f;
        leftSignal2 = sources[2];
        rightSignal2 = sources[3];
        leftSignal2.volume = 0f;
        rightSignal2.volume = 0f;
        newTargetPos();
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {

            targetEuler = new Vector3(
            0f,
            targetEuler.y + ghostdir * Random.Range(0.01f, 0.05f),
            0f
            );

            targetObj.transform.rotation = Quaternion.Euler(targetEuler);



            Vector3 diff = transform.rotation.eulerAngles - targetEuler;
            //Debug.Log(diff);

            Vector3 spokeToActual = playerCube.transform.position - centerCube.transform.position,
                        spokeToCorrect = targetCube.transform.position - centerCube.transform.position;
            float angleFromCenter = Vector3.Angle(spokeToActual, spokeToCorrect);
            //Debug.Log(angleFromCenter);

            // bool collided = ;
            // if (angleFromCenter<6f)
            //  {
            // playerCube.GetComponent<playerCubeScript>().collided = false;
            //      newTargetPos();
            //  }

            bool xTargetReached = false;
            bool yTargetReached = false;

            xTargetReached = sonifyAxis(diff.y, leftSignal, rightSignal, false);

           // if (xTargetReached)
            //{
            //   // yTargetReached = sonifyAxis(diff.x, leftSignal2, rightSignal2, true);
            //}
          //  else
            //{
              //  fade2();
           // }


            if ( (gameManager.GetComponent<gameseq>().checkSwipe(1,"left") 
                || gameManager.GetComponent<gameseq>().checkSwipe(1, "right"))
                && xTargetReached )
            {
                // cAPTURED YAAAAAAAAA
                gameManager.GetComponent<gameseq>().ghostBusted();
                newTargetPos();
                blaster.Play();
            }
        }

        else fadeout();
        
    }

    void fadeout()
    {
        fade1();
        fade2();
    }

    void fade1()
    {
        if (leftSignal.volume - faderate > 0f) leftSignal.volume -= faderate;
        if (rightSignal.volume - faderate > 0f) rightSignal.volume -= faderate;
    }

    void fade2()
    {
        if (leftSignal2.volume - faderate > 0f) leftSignal2.volume -= faderate;
        if (rightSignal2.volume - faderate > 0f) rightSignal2.volume -= faderate;
    }
    public void newTargetPos()
    {
        if (Random.Range(0f, 1f) > 0.5) ghostdir *= -1;
        Quaternion randomRot = Random.rotation;
        targetEuler = new Vector3(
            0f,
            randomRot.eulerAngles.y,
            0f
            );

        targetObj.transform.rotation = Quaternion.Euler(targetEuler);
    }

    bool sonifyAxis(float angle, AudioSource source1, AudioSource source2, bool updateChorus)
    {
        bool result = false;
        if (angle > 180f) angle -= 360f;
        else if (angle < -180f) angle += 360f;

        float absAngle = Mathf.Abs(angle);
        float vol = 1f - absAngle / hearingThreshold;

        if (absAngle < pingThreshold)
        {
            source2.volume = 0.5f;
            source1.volume = 0.5f;

            result = true;
        }
        else if (absAngle < hearingThreshold)
        {
            
            if (angle > 0)
            {
                source1.volume = vol;
                source2.volume = 0f;
            }
            else
            {
                source2.volume = vol;
                source1.volume = 0f;
            }

            
        }
        else
        {
            source1.volume = 0f;
            source2.volume = 0f;
        }

        if (updateChorus)
        {
            chorusmixer.SetFloat("chorusRate", vol * 20f);
        }

        return result;
    }

}
