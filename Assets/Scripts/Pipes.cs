// Pipes.cs
using UnityEngine;

public class Pipes : MonoBehaviour
{
    private const float spacing = 2f; // Distance between pipes
    private const int totalPipes = 3;
    private Vector3 startPos;
    public float pipeVariance = .5f;

    private void Awake()
    {
        startPos = transform.localPosition;
        RandomizeY(); 
    }
    
    private void Update()
    {
        transform.Translate(Vector3.left * 2f * Time.deltaTime);
        if (transform.position.x < -10f)
        {
            transform.position = new Vector3(10f, Random.Range(-2f, 2f), 0f);
        }
    }

    public void InitialPosition()
    {
        transform.localPosition = startPos;
        RandomizeY();
    }

    private void RandomizeY()
    {
        float posY = Random.Range(-pipeVariance, pipeVariance);
        transform.Translate(Vector3.up * posY);
    }
}