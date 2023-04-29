using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewButtonObject : MonoBehaviour
{
    public bool canBePressed;
    public KeyCode keyToPress;
    bool canDestory = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToPress)){
            if (canBePressed)
            {
          
                gameObject.SetActive(false);
                // ******** UPDATE SCORE HERE *********
                GameManager.instance.NoteHit();
            }
            else if (canDestory)
            {
                gameObject.SetActive(false);
                GameManager.instance.NoteMissed();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Activator")
        {
            canBePressed = true;
        }
        else if(collision.tag == "Destroyer")
        {
            canDestory = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canBePressed = false;
        //******* MINUS POINTS ********

        if (collision.tag == "Activator" && gameObject.activeSelf)
        {
            GameManager.instance.NoteMissed();
            gameObject.SetActive(false);
        }
    }

}
