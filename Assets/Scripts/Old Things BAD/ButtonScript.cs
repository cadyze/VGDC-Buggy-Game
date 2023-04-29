using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    float speed = 100f;
    float despawnXPos = 194f;
    public int letter;       // 0: A , 1: S, 2: d
    public bool isActive = false;
    private Vector3 ASpawn = new Vector3(483, 423, 0);
    private Vector3 SSpawn = new Vector3(482, 408, 0);
    private Vector3 DSpawn = new Vector3(482, 392, 0);

    public AudioClip sound;
    //Okay so this is backwards but I already made this class and i dont want
    //to change it to a scriptable object so im just gonna write
    //an SetUp method

    //MUST CALL THIS METHOD WHEN CREATING A NEW BUTTON
    public void SetUp(int letter, AudioClip sound) {

        this.letter = letter;
        this.sound = sound;
    }




    void Start()
    {
        FindObjectOfType<RythmGameManager>().addObject(gameObject);
        //letter = Random.Range(0, 3);
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
        if(transform.position.x < despawnXPos)
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
