using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class micScript : MonoBehaviour
{
    // Start is called before the first frame update
    float[] sampleData;
    float loudness = 0f;

    public GameObject marker;
    public GameObject text1;
    public GameObject text2;
    void Start()
    {
        Debug.Log(Microphone.devices.Length);
        foreach (var device in Microphone.devices)
        {
            Debug.Log("Name: " + device);
        }

        AudioSource audio = gameObject.GetComponent<AudioSource>();
        audio.clip = Microphone.Start(Microphone.devices[0], true, 10, 44100);
        audio.loop = true;

        text1.GetComponent<UnityEngine.UI.Text>().text = "";
        text2.GetComponent<UnityEngine.UI.Text>().text = "";

        while (!(Microphone.GetPosition(null) > 0)) { }
        audio.Play();

        text1.GetComponent<UnityEngine.UI.Text>().text = Microphone.devices[0];
    }

    void Update()
    {
        float[] spectrum = new float[256];

        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

        loudness = 0f;
        AudioListener.GetOutputData(sampleData, 0);
        foreach( float sample in sampleData)
        {
            loudness += Mathf.Abs(sample);
        }

        loudness /= sampleData.Length;


        marker.transform.localScale = new Vector3(1f, 10f * loudness, 1f);
        text2.GetComponent<UnityEngine.UI.Text>().text = ""+loudness;
    }
}
