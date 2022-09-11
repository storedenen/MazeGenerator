using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField]
    public float rotationSpeed = 1f;

    [SerializeField]
    public Vector3 rotationDirection = Vector3.left;

    void Update()
    {
        Vector3 rotateAroundVector = Quaternion.AngleAxis(90, Vector3.forward) * rotationDirection;

        this.transform.Rotate(rotateAroundVector * Time.deltaTime * rotationSpeed);
    }

    private void OnDrawGizmos()
    {
        Vector3 lineEnd = this.transform.position + rotationDirection * rotationSpeed;

        Gizmos.DrawLine(this.transform.position, lineEnd);
    }
}
