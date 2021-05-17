using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class DroneController : Agent
{
    [SerializeField] GameObject[] propellers;
    [SerializeField] Rigidbody rb;

    Environment environment;
    GameObject target;

    float startingDistance;
    float closestDistance;

    public void InitialiseEnvironment(Environment _environment, GameObject _target)
    {
        environment = _environment;
        target = _target;
    }

    private void InitialiseDistance()
    {
        Vector3 vectorToTarget = target.transform.localPosition - transform.localPosition;
        startingDistance = vectorToTarget.magnitude;
        closestDistance = startingDistance;
    }

    public override void OnEpisodeBegin()
    {
        target = environment.transform.GetChild(0).gameObject;
        environment.ResetEnvironment();
        InitialiseDistance();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        Vector3 vectorToTarget = target.transform.localPosition - transform.localPosition;
        float currentDistance = vectorToTarget.magnitude;
        if(currentDistance < closestDistance)
        {
            closestDistance = currentDistance;
        }

        sensor.AddObservation(vectorToTarget);
        sensor.AddObservation(rb.velocity);
        sensor.AddObservation(transform.eulerAngles);
        sensor.AddObservation(rb.angularVelocity);
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        for(int i = 0; i < vectorAction.Length; i++)
        {
            propellers[i].transform.Rotate(0, 45 * vectorAction[i], 0);
            rb.AddForceAtPosition(transform.up * vectorAction[i], propellers[i].transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Boundary"))
        {
            AddReward(startingDistance - closestDistance);
            EndEpisode();
        }
        if (other.gameObject.CompareTag("Target"))
        {
            AddReward(startingDistance);
            environment.RandomiseTargetPosition();
            InitialiseDistance();
        }
    }
}
