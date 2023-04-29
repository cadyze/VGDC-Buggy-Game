using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewButtonScript : MonoBehaviour
{
    public float songTempo;
    public bool hasStarted = false;
    public GameObject[] beatGroups;
    int beatGroupIndex = 1;
    public float timeCheck = 0;
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
            timeCheck += Time.deltaTime;

            //minimizes lag
            if (timeCheck > 6.85f && beatGroups.Length > beatGroupIndex)
            {
                beatGroups[beatGroupIndex].SetActive(true);
                GameObject temp = Instantiate<GameObject>(beatGroups[beatGroupIndex]);
                temp.transform.SetParent(GameObject.Find("NoteHolder").transform);
                temp.transform.position = new Vector3(11, 0, 0);
                beatGroupIndex++;
                timeCheck = 0;
            }
        }

        
    }
}
