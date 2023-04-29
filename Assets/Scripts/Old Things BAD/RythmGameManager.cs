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
    public AudioClip backingSong;

    private AudioSource source;
    private bool alreadySpawning = false;

    private void Awake()
    {
        source = GameObject.Find("Main Camera").GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("startSpawn", 0, intervalSpawn);
        StartGame();
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
    public void StartSpawn()
    {
        StartCoroutine(Spawn());
    }
    public void StartGame()
    {
        source.PlayOneShot(backingSong);
        StartCoroutine(SpawnOnBeat());
    }

    IEnumerator SpawnOnBeat()
    {

        yield return new WaitForSecondsRealtime(.25f);
        SpawnAButton(Ja, Random.Range(0, 3));
        yield return new WaitForSecondsRealtime(.25f);
        SpawnAButton(Ja, Random.Range(0, 3));
        yield return new WaitForSecondsRealtime(.25f);
        SpawnAButton(Ja, Random.Range(0, 3));
        yield return new WaitForSecondsRealtime(.25f);
        SpawnAButton(Ja, Random.Range(0, 3));
        yield return new WaitForSecondsRealtime(.25f);
        SpawnAButton(Ja, Random.Range(0, 3));
        yield return new WaitForSecondsRealtime(.25f);
        SpawnAButton(Ja, Random.Range(0, 3));
        yield return new WaitForSecondsRealtime(.25f);
        SpawnAButton(Ja, Random.Range(0, 3));
        yield return new WaitForSecondsRealtime(.25f);
        SpawnAButton(Ja, Random.Range(0, 3));
        yield return new WaitForSecondsRealtime(.25f);
        SpawnAButton(Ja, Random.Range(0, 3));

    }
    //We could create whole course of button presses in here
    IEnumerator Spawn()
    {
        if (!alreadySpawning)
        {
            alreadySpawning = true;
            yield return new WaitForSecondsRealtime(Random.Range(spawnRangeMin, spawnRangeMax));
            SpawnAButton(Ja, Random.Range(0,3));
            yield return new WaitForSecondsRealtime(Random.Range(spawnRangeMin, spawnRangeMax));
            SpawnAButton(Je, Random.Range(0, 3));
            yield return new WaitForSecondsRealtime(Random.Range(spawnRangeMin, spawnRangeMax));
            SpawnAButton(Ja, Random.Range(0, 3));
            intervalSpawn -= .05f;
            alreadySpawning = false;
        }


    }

   

    public void SpawnAButton(AudioClip clip, int letter)
    {
        GameObject temp = Instantiate(buttonPrefab);
        temp.transform.SetParent(canvas.transform);
        temp.GetComponent<ButtonScript>().SetUp(letter, clip);
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
            source.PlayOneShot(buttonsInScene[0].sound);
            
            if (smallScore >= 3)
            {
                score++;
                updateScore();
                smallScore = 0;
            }

            removeObject(buttonsInScene[0].gameObject);
            
            
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
