using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class compassHandler : MonoBehaviour
{
    Compass compass;
    // Start is called before the first frame update
    void Start()
    {
        compass = Input.compass;
        compass.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        float read = compass.magneticHeading;
        transform.rotation.SetEulerAngles(new Vector3(0f, read, 0f));
        Debug.Log(read);
        Debug.Log(Input.gyro.attitude);
    }
}
