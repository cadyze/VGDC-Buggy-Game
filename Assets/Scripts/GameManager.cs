using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public AudioSource source;

    public bool startSong = false;
    public NewButtonScript theBS;
    public int curScore = 0;
    public int pointPerScore = 10;
    public int currentMultiplier = 1;
    public int multiplierTracker = 0;
    public static GameManager instance;

    public Text score;
    public Text multiplier;
    public TMP_Text buttonText;
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        score.text = "Score: 0";
    }

    // Update is called once per frame
    void Update()
    {
        if (!startSong)
        {
            if (Input.anyKey)
            {
                startSong = true;
                theBS.hasStarted = true;
                source.Play();
                buttonText.gameObject.SetActive(false);
            }
        }
        else
        {

        }
    }

    public void NoteHit()
    {
        Debug.Log("HIT THE NOTE ON TIME");
        curScore += pointPerScore * currentMultiplier;
        
        multiplierTracker++;

        if(multiplierTracker > currentMultiplier)
        {
            currentMultiplier++;
            multiplierTracker = 0;
        }

        score.text = "Score: " + curScore;
        multiplier.text = "Multiplier: " + currentMultiplier + "x";
    }

    public void NoteMissed()
    {

        Debug.Log("MISSED THE NOTE --> CONSEQUENCE");
        multiplierTracker = 0;
        currentMultiplier = 1;
    }
}
