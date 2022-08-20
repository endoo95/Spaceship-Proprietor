using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform myTarget;
    [SerializeField] private Vector3 offset;

    [SerializeField] private float followSpeed = 0.2f; //Best for from 0 to 1

    private void Start()
    {
        myTarget = GameObject.FindGameObjectWithTag("Player").transform.Find("Ship").transform;
    }

    void FixedUpdate()
    {
        if (myTarget != null)
        {
            offset.z = transform.position.z;
            Vector3 targetPosition = myTarget.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, followSpeed);
            transform.position = smoothedPosition;
        }
    }
}