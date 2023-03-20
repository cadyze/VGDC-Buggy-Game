using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RythmGameManager : MonoBehaviour
{
    public List<ButtonScript> buttonsInScene;
    public GameObject buttonPrefab;
    int score = 0;
    int smallScore = 0;
    public TMP_Text scoreText;
    public Canvas canvas;
    private float intervalSpawn = 4f;
    private float spawnRangeMin = .25f;
    private float spawnRangeMax = 1f;
    int soundTick = 0;

    public AudioClip Ja;
    public AudioClip Je;
    private AudioSource source;
    private bool alreadySpawning = false;

    private void Awake()
    {
        source = GameObject.Find("Main Camera").GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("startSpawn", 0, intervalSpawn);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            checkButton(0);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            checkButton(1);
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            checkButton(2);
        }

        

    }
    public void startSpawn()
    {
        StartCoroutine(spawn());
    }

    IEnumerator spawn()
    {
        if (!alreadySpawning)
        {
            alreadySpawning = true;
            yield return new WaitForSecondsRealtime(Random.Range(spawnRangeMin, spawnRangeMax));
            GameObject temp = Instantiate(buttonPrefab);
            temp.transform.SetParent(canvas.transform);
            yield return new WaitForSecondsRealtime(Random.Range(spawnRangeMin, spawnRangeMax));
            temp = Instantiate(buttonPrefab);
            temp.transform.SetParent(canvas.transform);
            yield return new WaitForSecondsRealtime(Random.Range(spawnRangeMin, spawnRangeMax));
            temp = Instantiate(buttonPrefab);
            temp.transform.SetParent(canvas.transform);
            //intervalSpawn -= .05f;
            alreadySpawning = false;
        }


    }

    public void addObject(GameObject button)
    {
        buttonsInScene.Add(button.GetComponent<ButtonScript>());
    }
    public void removeObject(GameObject button)
    {
        buttonsInScene.Remove(button.GetComponent<ButtonScript>());
        button.GetComponent<ButtonScript>().destroy();
        
        
    }
    public void checkButton(int i)
    {
        
        if(buttonsInScene.Count > 0 && buttonsInScene[0].isActive && i == buttonsInScene[0].letter)
        {
            
            smallScore++;
            if(soundTick== 0 || soundTick == 2)
            {
                source.PlayOneShot(Ja);
               
            }
            else
            {
                source.PlayOneShot(Je);
            }
            if (smallScore >= 3 && soundTick >=2)
            {
                score++;
                updateScore();
                smallScore = 0;
            }
            if (soundTick == 2)
            {
                smallScore = 0;
            }
            removeObject(buttonsInScene[0].gameObject);
            soundTick++;
            if (soundTick >= 3)
            {
                soundTick = 0;
            }
        }
        else
        {
            if (buttonsInScene.Count > 0)
            {
                removeObject(buttonsInScene[0].gameObject);
                minusPoints();
                soundTick++;
                if (soundTick >= 3)
                {
                    soundTick = 0;
                }
            }
        }
        
    }

    public void minusPoints()
    {
        smallScore = 0;
        score--;
        updateScore();
    }

    public void updateScore()
    {
        scoreText.text = "Score: " + score;
    }

}
