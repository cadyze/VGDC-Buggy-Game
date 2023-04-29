using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewButtonScript : MonoBehaviour
{
    public float songTempo;
    public bool hasStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        songTempo = songTempo / 60f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            
        }
        else
        {
            transform.position -= new Vector3(songTempo * Time.deltaTime, 0f, 0);
        }
    }
}
