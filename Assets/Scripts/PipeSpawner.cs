using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public GameObject pipePrefab;
    public float spawnInterval = 2f;
    public float pipeSpeed = 2f;
    public float verticalRange = 2f;
    public int maxPipes = 5;

    private List<GameObject> activePipes = new List<GameObject>();
    private float timer;

    void Start()
    {
        timer = spawnInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SpawnPipe();
            timer = spawnInterval;
        }

        MovePipes();
        CleanupPipes();
    }

    void SpawnPipe()
    {
        float yOffset = Random.Range(-verticalRange, verticalRange);
        Vector3 spawnPosition = new Vector3(transform.position.x, yOffset, 0f);
        GameObject pipe = Instantiate(pipePrefab, spawnPosition, Quaternion.identity);
        activePipes.Add(pipe);
    }

    void MovePipes()
    {
        foreach (GameObject pipe in activePipes)
        {
            if (pipe != null)
            {
                pipe.transform.position += Vector3.left * pipeSpeed * Time.deltaTime;
            }
        }
    }

    void CleanupPipes()
    {
        for (int i = activePipes.Count - 1; i >= 0; i--)
        {
            if (activePipes[i].transform.position.x < -10f)
            {
                Destroy(activePipes[i]);
                activePipes.RemoveAt(i);
            }
        }
    }

    public void ResetPipes()
    {
        foreach (GameObject pipe in activePipes)
        {
            Destroy(pipe);
        }
        activePipes.Clear();
        timer = spawnInterval;
    }

    public Transform GetClosestPipe(float birdX)
    {
        Transform closest = null;
        float closestDistance = float.MaxValue;
        
        foreach (GameObject pipe in activePipes)
        {
            if (pipe.transform.position.x > birdX)
            {
                float distance = pipe.transform.position.x - birdX;
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closest = pipe.transform;
                }
            }
        }
        return closest;
    }
}