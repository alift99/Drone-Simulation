using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    [SerializeField]
    GameObject drone;
    [SerializeField]
    GameObject target;
    [SerializeField]
    GameObject ground;
    [SerializeField]
    Material[] materials;

    private void Start()
    {
        drone.GetComponent<DroneController>().InitialiseEnvironment(this, target);
    }

    public void ResetEnvironment()
    {
        ResetDrone();
        RandomiseTargetPosition();
    }

    public void RandomiseTargetPosition()
    {
        Vector3 targetPosition = new Vector3(Random.Range(-10.0f, 10.0f), 15f, Random.Range(-10.0f, 10.0f));
        target.transform.localPosition = targetPosition;
    }

    public void ResetDrone()
    {
        drone.transform.localPosition = new Vector3(0f, 10f, 0f);
        drone.transform.eulerAngles = Vector3.zero;
        drone.GetComponent<Rigidbody>().velocity = Vector3.zero;
        drone.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }
}
