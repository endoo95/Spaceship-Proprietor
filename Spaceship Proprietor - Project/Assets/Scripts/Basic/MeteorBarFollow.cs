using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorBarFollow : MonoBehaviour
{
    public Transform myTarget;
    public Vector3 offset;

    [SerializeField] private float followSpeed = 1f;

    private void FixedUpdate()
    {
        if(myTarget != null)
        {
            Vector3 targetPosition = myTarget.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, followSpeed);
            transform.position = smoothedPosition;
        }
    }
}
