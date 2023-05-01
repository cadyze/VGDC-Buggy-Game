using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScamperController : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void OnHurt()
    {
        anim.SetTrigger("Hurt");
    }
}
