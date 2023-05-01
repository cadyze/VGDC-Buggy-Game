using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugSpawner : MonoBehaviour
{

    private Queue<GameObject> lifeQueue;
    [SerializeField] private GameObject bluePrefab;
    [SerializeField] private GameObject redPrefab;
    [SerializeField] private Vector3 bugSpawnPoint;
    public void SpawnLifeBug(BUG_COLOR color)
    {
        if(color == BUG_COLOR.BLUE)
        {
            lifeQueue.Enqueue(Instantiate(bluePrefab, bugSpawnPoint, Quaternion.identity));
        }
        else
        {

            lifeQueue.Enqueue(Instantiate(redPrefab, bugSpawnPoint, Quaternion.identity));
        }
    }

    public void HealthLoss()
    {
        if(lifeQueue.Count != 0)
        Destroy(lifeQueue.Dequeue());
    }

    private void Start()
    {
        StartCoroutine(SpawnInitialLives());   
    }

    private IEnumerator SpawnInitialLives()
    {
        lifeQueue = new Queue<GameObject>();
        for(int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.25f);
            lifeQueue.Enqueue(Instantiate(bluePrefab, bugSpawnPoint, Quaternion.identity));
        }
    }

}
