using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class HealthManager : MonoBehaviour
{
    //I'm thing the way the whole thing works is that you have a certain amount of misses before you lose a bug, like maybe 2 misses and the bug goes bye bye
    //you can have a max of 10 nugs, and maybe the big bugs give you better health? but that's a later thing. 
    // Like every 10 hits gets you a bug

    [SerializeField] private UnityEvent OnHealthLoss;
    public int health = 5;
    public int healthTracker;
    public int damageTracker;
    [SerializeField] private TextMeshPro healthTxt;

    // Start is called before the first frame update
    void Start()
    {
        healthTracker = 0;
        damageTracker = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // will increment the health tracker by 1
    public void TrackerUpdate()
    {
        healthTracker++;
        UpdateHealthUI();
    }

    public bool ShouldGiveHealth()
    {
        if (healthTracker >= 10 && health < 10)
        {
            healthTracker = 0;
            return true;
        }
        return false;
    }

    public void IncreaseHealth()
    {
        health++;
        UpdateHealthUI();
    }

    //if health is less than 1 returns true, else returns false
    public bool HealthDamage()
    {
        UpdateHealthUI();
        damageTracker++;
        if (damageTracker >= 2)
        {
            damageTracker = 0;
            health--;
            OnHealthLoss.Invoke();
            if (health < 0) return true;
        }
        UpdateHealthUI();
        return false;
    }

    private void UpdateHealthUI()
    {
        healthTxt.text = health.ToString();
    }
}
