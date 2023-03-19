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
    private float intervalSpawn = 6f;
    private float spawnRangeMin = .25f;
    private float spawnRangeMax = 2f;

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
        
        yield return new WaitForSecondsRealtime(Random.Range(spawnRangeMin, spawnRangeMax));
        GameObject temp = Instantiate(buttonPrefab);
        temp.transform.SetParent(canvas.transform);
        yield return new WaitForSecondsRealtime(Random.Range(spawnRangeMin, spawnRangeMax));
        temp = Instantiate(buttonPrefab);
        temp.transform.SetParent(canvas.transform);
        yield return new WaitForSecondsRealtime(Random.Range(spawnRangeMin, spawnRangeMax));
        temp = Instantiate(buttonPrefab);
        temp.transform.SetParent(canvas.transform);
        intervalSpawn -= .05f;
        smallScore = 0; //this is a bandaid and will fail if stuff spawns  too quick


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
            removeObject(buttonsInScene[0].gameObject);
            smallScore++;
            if (smallScore >= 3)
            {
                score++;
                updateScore();
                smallScore = 0;
            }
        }
        else
        {
            if (buttonsInScene.Count > 0)
            {
                removeObject(buttonsInScene[0].gameObject);
                minusPoints();
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
