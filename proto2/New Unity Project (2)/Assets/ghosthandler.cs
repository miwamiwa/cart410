using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghosthandler : MonoBehaviour
{
    public Vector3 vel = new Vector3(0f, 0f, 0f);
    AudioSource sfx;
    // Start is called before the first frame update
    void Start()
    {
        sfx = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += vel;
        if (!sfx.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
