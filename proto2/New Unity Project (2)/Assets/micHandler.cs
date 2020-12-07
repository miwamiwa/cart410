using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class micHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioSource audio = gameObject.GetComponent<AudioSource>();
        audio.clip = Microphone.Start(Microphone.devices[0], true, 10, 44100);
        audio.loop = true;

        while (!(Microphone.GetPosition(null) > 0)) { }
        audio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
