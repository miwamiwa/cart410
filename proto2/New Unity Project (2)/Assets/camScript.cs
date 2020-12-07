using UnityEngine;

public class camScript : MonoBehaviour
{
    static bool gyroBool;
    private Gyroscope gyro;
    //private Quaternion quatMult;
    private Quaternion quatMap;
    bool calibrated = false;
    private Vector3 initRot;
    GameObject camParent;
    float speed = 1f;

    public GameObject playerCube;
    public GameObject targetCube;
    public GameObject centerCube;
    public GameObject targetCenterCube;


    AudioSource audio;

    Quaternion targetAngle;
    // Awake() is called before start 
    private void Awake()
    {
        // create cam parent and grandparent and setup hierarchy
        Transform currentParent = transform.parent;
        camParent = new GameObject("camParent");
        camParent.transform.position = transform.position;
        GameObject camGrandparent = new GameObject("camGrandParent");
        camGrandparent.transform.position = transform.position;
        camParent.transform.parent = camGrandparent.transform;
        camGrandparent.transform.parent = currentParent;

        audio = GameObject.Find("audioSource").GetComponent<AudioSource>();

        newTarget();

        gyroBool = SystemInfo.supportsGyroscope;

        if (gyroBool)
        {
            gyro = Input.gyro;
            gyro.enabled = true;

            camParent.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            /*
            if(Screen.orientation == ScreenOrientation.LandscapeLeft)
            {
                quatMult = new Quaternion(0f, 0f, 0.7071f, -0.7071f);
            }
            else if (Screen.orientation == ScreenOrientation.LandscapeRight)
            {
                quatMult = new Quaternion(0f, 0f, -0.7071f, -0.7071f);
            }
            else if (Screen.orientation == ScreenOrientation.Portrait)
            {
                quatMult = new Quaternion(0f, 0f, 0f, 1f);
            }
            else if (Screen.orientation == ScreenOrientation.PortraitUpsideDown)
            {
                quatMult = new Quaternion(0f, 0f, 1f, 0f);
            }
            */
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }
        else
        {
            // Debug.Log("NO GYRO");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void newTarget()
    {
        targetAngle = Random.rotation;
        targetCenterCube.transform.eulerAngles = new Vector3(
                targetAngle.eulerAngles.x,
                targetAngle.eulerAngles.y,
                0f);

        transform.parent = camParent.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = Vector3.zero;

        if (gyroBool)
        {
            quatMap = gyro.attitude; // new Quaternion(gyro.attitude.w, gyro.attitude.x, gyro.attitude.y, gyro.attitude.z );
            Quaternion totalQuat = quatMap;// * quatMult;
            

            camParent.transform.localRotation = totalQuat;
            if (!calibrated)
            {
                if (Input.touchCount > 0)
                {
                    calibrated = true;
                    transform.localEulerAngles = -totalQuat.eulerAngles;
                }
            }

            Quaternion diff = Quaternion.identity;
            
            diff.eulerAngles = new Vector3(
                camParent.transform.eulerAngles.x - targetAngle.eulerAngles.x,
                0,//camParent.transform.eulerAngles.y - targetAngle.eulerAngles.y,
                0f );

            Debug.Log("DIFF: "+diff.eulerAngles.x+", "+diff.eulerAngles.y);
            centerCube.transform.eulerAngles = camParent.transform.eulerAngles;// camParent.transform.eulerAngles;
            
            
            Vector3 spokeToActual = playerCube.transform.position - centerCube.transform.position,
                    spokeToCorrect = targetCube.transform.position - centerCube.transform.position ;
            float angleFromCenter = Vector3.Angle(spokeToActual, spokeToCorrect);
            float newvolume = 1f - angleFromCenter / 180f; ;
            newvolume = Mathf.Pow(newvolume, 4f);
            audio.volume = newvolume;
            // NOTE: angle inputs don't need normalize. In degrees(!!)
            Debug.Log("ANGLE: "+angleFromCenter);
            //float D = 2 * Mathf.PI * radius * (angleFromCenter / 360);

            //  targetcube2.transform.eulerAngles = targetAngle.eulerAngles;
            //   Debug.Log("***********************");
            //   Debug.Log("Current Angle: x:" + camParent.transform.eulerAngles.x + ", y:" + camParent.transform.eulerAngles.y + ", z:" + camParent.transform.eulerAngles.z);
            //   Debug.Log("Target Angle: x:" + targetAngle.eulerAngles.x + ", y:" + targetAngle.eulerAngles.y + ", z:" + targetAngle.eulerAngles.z);
            //    Debug.Log("diff: x:" + diff.eulerAngles.x + ", y:" + diff.eulerAngles.y + ", z:" + diff.eulerAngles.z);

        }

        /*
        dir.x = 10f * Mathf.Floor( -Input.acceleration.y / 10f ) * speed;
        dir.y = 10f * Mathf.Floor( -Input.acceleration.z / 10f ) * speed;
        dir.z = 10f * Mathf.Floor( -Input.acceleration.x / 10f ) * speed;


        // clamp acceleration vector to unit sphere
        if (dir.sqrMagnitude > 1)
            dir.Normalize();

        // Make it move 10 meters per second instead of 10 meters per frame...
        dir *= Time.deltaTime;

        // Move object
        transform.Translate(dir );
        */
    }
}
