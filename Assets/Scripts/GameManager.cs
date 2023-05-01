using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UnityEvent OnNoteHit;
    [SerializeField] private UnityEvent<BUG_COLOR> OnSpawnBug;
    [SerializeField] private UnityEvent OnHurt;
    [SerializeField] private Animator bg_anim;
    public AudioSource source;

    public bool startSong = false;
    public NewButtonScript theBS;
    public int curScore = 0;
    public int pointPerScore = 10;
    public int currentMultiplier = 1;
    public int multiplierTracker = 0;
    public static GameManager instance;

    private int noteHitStreak = 0;
    private bool canAddStreak = true;

    [SerializeField] private TextMeshPro scoreText;
    [SerializeField] private TextMeshProUGUI multplierText;
    public Animator buttonText;
    public AudioClip miss;

    public HealthManager health;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
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
                buttonText.Play("Bye");
            }
        }
        else
        {

        }
    }

    public void NoteHit()
    {
        if (canAddStreak)
        {
            StartCoroutine(NoteHitCoroutine());
            noteHitStreak++;

            //Spawn a bug on the conveyer belt every three hits
            if (noteHitStreak % 4 == 0)
            {
                //Spawn in a bug on the conveyer belt
                if (noteHitStreak >= 10)
                {
                    //Spawn a red bug to show on fire if high enough combo
                    OnSpawnBug.Invoke(BUG_COLOR.RED);
                }
                else
                {
                    //Spawn blue bug
                    OnSpawnBug.Invoke(BUG_COLOR.BLUE);
                }
            }
            OnNoteHit.Invoke();
        }
        
        Debug.Log("HIT THE NOTE ON TIME");
        curScore += pointPerScore * currentMultiplier;
        
        multiplierTracker++;

        if(multiplierTracker > currentMultiplier)
        {
            currentMultiplier++;
            multiplierTracker = 0;
        }

        scoreText.text = curScore.ToString();
        multplierText.text = "x" + currentMultiplier;

        //health.TrackerUpdate();
    }
    private IEnumerator NoteHitCoroutine()
    {
        canAddStreak = false;
        yield return new WaitForSeconds(0.25f);
        canAddStreak = true;
    }
    public void NoteMissed()
    {
        OnHurt.Invoke();
        bg_anim.SetTrigger("Hurt");
        noteHitStreak = 0;
        Debug.Log("MISSED THE NOTE --> CONSEQUENCE");
        multiplierTracker = 0;
        currentMultiplier = 1;
        GetComponent<AudioSource>().PlayOneShot(miss);
        if (health.HealthDamage()) //if true game ends
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
