using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    float speed = 50f;
    public int letter;
    public bool isActive = false;
    private Vector3 ASpawn = new Vector3(483, 430, 0);
    private Vector3 SSpawn = new Vector3(482, 415, 0);
    private Vector3 DSpawn = new Vector3(482, 402, 0);
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<RythmGameManager>().addObject(gameObject);
        letter = Random.Range(0, 3);
        if(letter == 0)
        {
            transform.position = ASpawn;
 
        }
        else if(letter == 1)
        {
            transform.position = SSpawn;
        }
        else
        {
            transform.position = DSpawn;
        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        if(transform.position.x < 175)
        {
            FindObjectOfType<RythmGameManager>().minusPoints();
            Destroy(gameObject);
        }
    }

    public void destroy()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collision Box"))
        {
            isActive = true;
        }

        Debug.Log("Collision");
    }


    private void OnDestroy()
    {
        FindObjectOfType<RythmGameManager>().removeObject(gameObject);
    }
}
