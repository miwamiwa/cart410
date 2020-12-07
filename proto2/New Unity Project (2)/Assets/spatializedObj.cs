using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spatializedObj : MonoBehaviour
{
    public Transform listener;
    AudioSource clip;
    // Start is called before the first frame update
    void Start()
    {
        clip = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
