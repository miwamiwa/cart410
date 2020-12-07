using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCubeScript : MonoBehaviour
{
    public bool collided = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("yo!");
        if (collision.gameObject.name == "targetCube")
        {
            collided = true;
        }
    }
}
