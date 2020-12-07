using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostspawner : MonoBehaviour
{
    public List<AudioClip> ghosts;

    public GameObject ghostObj;
    int counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // counter++;
        //if (counter % 200 == 0) triggerGhost();
    }

    public void triggerGhost()
    {
        GameObject ghost = Instantiate(ghostObj, transform);
        Vector3 rand = new Vector3(Random.Range(10f, 20f), 0f, Random.Range(10f, 20f));
        if (Random.Range(0f, 1f) < 0.5) rand.x = -rand.x;
        if (Random.Range(0f, 1f) < 0.5) rand.z = -rand.z;
        ghost.transform.localPosition = rand;
        ghost.GetComponent<ghosthandler>().vel = -rand / 100f;
        AudioSource source = ghost.GetComponent<AudioSource>();
        source.clip = ghosts[Mathf.FloorToInt(Random.Range(0, ghosts.Count))];
        source.Play();
    }
}
