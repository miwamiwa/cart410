using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class listPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StopSound(int index)
    {
        AudioSource[] sources = gameObject.GetComponents<AudioSource>();
        sources[index].Stop();
        sources[index].loop = false;
    }

    public void playFile(int index, bool looping)
    {
        AudioSource[] sources = gameObject.GetComponents<AudioSource>();
        for(int i=0; i<sources.Length; i++)
        {
            sources[i].Stop();
            sources[i].loop = false;
        }

        sources[index].Play();
        sources[index].loop = looping;
    }
}
