
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtObject : MonoBehaviour
{
    [SerializeField] private Camera objectToLookAt;
    [SerializeField] private Quaternion rotationVector;

    void Update()
    {
        transform.rotation = new Quaternion(
            objectToLookAt.transform.rotation.x,
            objectToLookAt.transform.rotation.y,
            objectToLookAt.transform.rotation.z,
            objectToLookAt.transform.rotation.w
        );
        transform.Rotate(0, -180f, 0);
    }
}