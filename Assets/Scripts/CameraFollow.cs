using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public Vector3 cameraOffset;
    // Start is called before the first frame update
    void Start()
    {
        cameraOffset = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = target.position + cameraOffset;
        transform.position = newPosition;
    }
}
