using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform followTransform;
    private Vector3 previousPosition;
    public float smoothing = 1;
    public float rotationalSmoothing = 1;

    private void Start() {
        previousPosition = followTransform.position;
    }

    protected void FixedUpdate()
    {
        Vector3 currentVelocity = transform.position - previousPosition;
        transform.position = Vector3.SmoothDamp(transform.position, followTransform.position, ref currentVelocity, smoothing);
        transform.rotation = Quaternion.Slerp(transform.rotation, followTransform.rotation, rotationalSmoothing);
        previousPosition = transform.position;
    }
}
