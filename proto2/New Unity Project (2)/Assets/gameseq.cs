using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameseq : MonoBehaviour
{
    int stepCount = 1;
    public GameObject stepper;
    public GameObject pointer;
    public GameObject narrator;
    public GameObject ghostSFX;

    int lastghoststep = 0;

    bool previouslyTouching = false;
    float swipeYThreshold = 100f;
    float swipeXThreshold = 50f;
    int lastTouchCount = 0;
    int stepTrigger = 0;
    bool instructionsHeard = false;

    string gameState = "swipeToStart";

    List<Vector2> touches = new List<Vector2>();
    // Start is called before the first frame update
    void Start()
    {
        stopPointer();
    }

    // Update is called once per frame
    void Update()
    {

        stepCount = stepper.GetComponent<stepHandler>().stepCount;
        Debug.Log(stepCount);
        updateTouch();

        switch (gameState)
        {
            case "swipeToStart": 
                if(checkSwipe(1, "up"))
                {
                    // stop narration
                    //narrator.GetComponent<listPlayer>().StopSound(0);
                    // setup next state
                    gameState = "gameIntro";
                    narrator.GetComponent<listPlayer>().playFile(1, false);
                    gameObject.GetComponent<AudioSource>().Play();
                    stepTrigger = stepCount + 30;
                }
                else if (checkSwipe(2, "down"))
                {
                   // Debug.Log("EXIIT!");
                    ExitGame();
                }
                break;

            case "gameIntro":
                // narration:this place is teeming with dark spirits.. You must get them before they get you!
                // walk around and surely you will find one.

                // NEXT:
                // a ghost is nearby! point your phone towards the area you want to scan and touch your screen. 
                // this sound will point you left or right: {}. when you hear it in both ears, the ghost is in front of you.
                // hold that direction and swipe left or right to shoot it!
                if (stepCount % 10 == 0) triggerGhostSFX();
                if (stepCount >= stepTrigger)
                {
                    startPointerAction();
                }
                break;

            case "ghostFound":
                if (Input.touchCount > 0)
                {
                    activatePointer();
                }
                else stopPointer();
                break;

            case "ghostBusted":
                if (stepCount % 10 == 0) triggerGhostSFX();
                if(stepCount >= stepTrigger)
                {
                    startPointerAction();
                }
                break;
        }
        //Debug.Log("DOWN: " + checkSwipe(2, "down") + ", UP: " + checkSwipe(2, "up")+ ", LEFT: " + checkSwipe(2, "left") + ", RIGHT: " + checkSwipe(2, "right"));
    }

    void triggerGhostSFX()
    {
        if (stepCount != lastghoststep)
        {
            ghostSFX.GetComponent<ghostspawner>().triggerGhost();
            lastghoststep = stepCount;
        }
       
    }

    void startPointerAction()
    {
        gameState = "ghostFound";
        //narrator.GetComponent<listPlayer>().StopSound(1);
        if(!instructionsHeard) narrator.GetComponent<listPlayer>().playFile(2, false);
        else narrator.GetComponent<listPlayer>().playFile(4, false);
        instructionsHeard = true;
        activatePointer();
    }

    public void ghostBusted()
    {
        if (gameState == "ghostFound")
        {
            stopPointer();
            // play ghost bust sound
            gameState = "ghostBusted";
            stepTrigger = stepCount + 30;
            narrator.GetComponent<listPlayer>().playFile(3, false);
        }
    }

    void ExitGame()
    {
        Application.Quit();
    }

    // get touch start pos
    void updateTouch()
    {
        int count = Input.touchCount;
        if (count > 0)
        {
            if (!previouslyTouching || count != lastTouchCount)
            {
                touches = new List<Vector2>();
                for (int i = 0; i < count; i++)
                {
                    touches.Add(Input.GetTouch(i).position);
                }
            }
            previouslyTouching = true;

        }
        else
        {
            if (previouslyTouching)
            {
                touches = new List<Vector2>();
                for (int i = 0; i < count; i++)
                {
                    touches.Add(Input.GetTouch(i).position);
                }
            }
            previouslyTouching = false;
        }

        lastTouchCount = count;
    }

    public bool checkSwipe(int numFingers, string dir)
    {
        bool result = false;
        if (numFingers == touches.Count)
        {

            if (dir == "up")
            {
                bool swiped = true;
                for (int i = 0; i < Input.touchCount; i++)
                {
                    Vector2 pos = Input.GetTouch(i).position;
                    Vector2 diff = pos - touches[i];
                    float mag = diff.magnitude;
                    // Debug.Log(diff + " " + mag);
                    if (Mathf.Abs(mag) < swipeYThreshold || diff.y < 0) swiped = false;
                }

                result = swiped;
            }
            else if (dir == "down")
            {
                bool swiped = true;
                for (int i = 0; i < Input.touchCount; i++)
                {
                    Vector2 pos = Input.GetTouch(i).position;
                    Vector2 diff = pos - touches[i];
                    float mag = diff.magnitude;
                    //Debug.Log(diff + " " + mag);
                    if (Mathf.Abs(mag) < swipeYThreshold || diff.y > 0) swiped = false;
                }

                result = swiped;
            }
            else if (dir == "left")
            {
                bool swiped = true;
                for (int i = 0; i < Input.touchCount; i++)
                {
                    Vector2 pos = Input.GetTouch(i).position;
                    Vector2 diff = pos - touches[i];
                    float mag = diff.magnitude;
                    // Debug.Log(diff + " " + mag);
                    if (Mathf.Abs(mag) < swipeXThreshold || diff.x > 0) swiped = false;
                }

                result = swiped;
            }
            else if (dir == "right")
            {
                bool swiped = true;
                for (int i = 0; i < Input.touchCount; i++)
                {
                    Vector2 pos = Input.GetTouch(i).position;
                    Vector2 diff = pos - touches[i];
                    float mag = diff.magnitude;
                    //Debug.Log(diff + " " + mag);
                    if (Mathf.Abs(mag) < swipeXThreshold || diff.x < 0) swiped = false;
                }

                result = swiped;
            }
        }
        return result;
    }

    void activatePointer()
    {
        pointer.GetComponent<pointerscript>().active = true;
    }

    void stopPointer()
    {
        pointer.GetComponent<pointerscript>().active = false;
    }
}
