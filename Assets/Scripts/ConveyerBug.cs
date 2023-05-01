using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum BUG_COLOR
{
    RED,
    BLUE
}

public class ConveyerBug : MonoBehaviour
{
    public BUG_COLOR color;
    private Animator anim;
    int turnsTillFall = 6;
    //Move when the bug is on the conveyer belt, fall when necessary
    public bool OnMove()
    {
        turnsTillFall--;
        if(turnsTillFall == 0)
        {
            //Fall into hole and turn into health
            anim.Play("Fall");
            return false;
        }
        anim.Play("Move");
        return true;
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
}
