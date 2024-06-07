using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    Transform[] spawnPoints;
    [SerializeField]
    float speed;
    float waitTime = 0.5f;
    int currentPointIndex;
    bool once;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != spawnPoints[currentPointIndex].position)
        {
            // Move towards the current spawn point
            transform.position = Vector3.MoveTowards(transform.position, spawnPoints[currentPointIndex].position, speed * Time.deltaTime);
        }
        else
        {
            // Start waiting at the current point
            if (!once)
            {
                once = true;
                StartCoroutine(Wait());
            }
        }
    }

    IEnumerator Wait()
    {
        // Wait for the specified time
        yield return new WaitForSeconds(waitTime);

        // Move to the next spawn point
        if (currentPointIndex + 1 < spawnPoints.Length)
        {
            currentPointIndex++;
        }
        else
        {
            currentPointIndex = 0;
        }

        // Reset the once flag to allow moving again
        once = false;
    }
}
