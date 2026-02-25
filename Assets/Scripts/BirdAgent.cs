using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class BirdAgent : Agent
{
    public Rigidbody2D birdRb;
    public float flapForce = 5f;
    public Transform pipeSpawner;
    public PipeSpawner spawnerScript;

    public override void Initialize()
    {
        birdRb = GetComponent<Rigidbody2D>();
        if (birdRb == null)
        {
            birdRb = gameObject.AddComponent<Rigidbody2D>();
            birdRb.gravityScale = 1f;
        }
    }

    public override void OnEpisodeBegin()
    {
        // Reset bird position and velocity
        if (birdRb != null)
        {
            birdRb.linearVelocity = Vector2.zero;
        }
        transform.position = new Vector3(-2f, 0f, 0f);

        // Reset pipes
        if (spawnerScript != null)
        {
            spawnerScript.ResetPipes();
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Bird's vertical position and velocity
        sensor.AddObservation(transform.position.y);
        sensor.AddObservation(birdRb != null ? birdRb.linearVelocity.y : 0f);

        // Position of the next pipe
        if (spawnerScript != null)
        {
            Transform nextPipe = spawnerScript.GetClosestPipe(transform.position.x);
            if (nextPipe != null)
            {
                sensor.AddObservation(nextPipe.position.x - transform.position.x);
                sensor.AddObservation(nextPipe.position.y - transform.position.y);
            }
            else
            {
                sensor.AddObservation(0f);
                sensor.AddObservation(0f);
            }
        }
        else
        {
            sensor.AddObservation(0f);
            sensor.AddObservation(0f);
        }
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        int flap = actions.DiscreteActions[0];
        if (flap == 1)
        {
            if (birdRb != null)
            {
                Debug.Log("Jump!");
                birdRb.linearVelocity = Vector2.up * flapForce;
            }
        }

        // Reward for staying alive
        AddReward(0.1f);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActions = actionsOut.DiscreteActions;
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Space!");
            discreteActions[0] = 1;
        }
        else
        {
            discreteActions[0] = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pipe") || collision.CompareTag("Ground"))
        {
            SetReward(-1f);
            EndEpisode();
        }
    }
}