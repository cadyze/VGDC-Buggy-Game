using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Events;

public class ConveyerManager : MonoBehaviour
{
    [SerializeField] private UnityEvent<BUG_COLOR> OnBugFall;
    [SerializeField] private GameObject conveyerBug_blue;
    [SerializeField] private GameObject conveyerBug_red;
    [SerializeField] private Vector2 bugSpawnPosition;
    [SerializeField] private HealthManager healthManager;
    private List<ConveyerBug> conveyers = new List<ConveyerBug>();
    private bool canMoveAgain = true;

    //Move the conveyer belt with every correct note hit
    public void OnNoteHit()
    {
        if (conveyers.Count == 0 || !canMoveAgain) return;
        StartCoroutine(MoveCoroutine());
        foreach(ConveyerBug bug in FindObjectsOfType<ConveyerBug>())
        {
            //Returns false if dead and falls in hole
            if(!bug.OnMove())
            {
                //conveyers.Remove(bug);
                StartCoroutine(BugFall(bug));
            }
        }
    }

    private IEnumerator BugFall(ConveyerBug bug)
    {
        yield return new WaitForSeconds(0.25f);
        OnBugFall.Invoke(bug.color);
        healthManager.IncreaseHealth();
        Destroy(bug.gameObject);
    }
    //Stops random spam
    private IEnumerator MoveCoroutine()
    {
        canMoveAgain = false;
        yield return new WaitForSeconds(0.25f);
        canMoveAgain = true;
    }

    //Invoke spawn bug when told to
    public void SpawnBug(BUG_COLOR color)
    {
        GameObject bug;

        if (color == BUG_COLOR.BLUE) {  bug = Instantiate(conveyerBug_blue, bugSpawnPosition, Quaternion.identity); }
        else { bug = Instantiate(conveyerBug_red, bugSpawnPosition, Quaternion.identity); }
        
        conveyers.Add(bug.GetComponent<ConveyerBug>());
    }
}
