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

        this.transform.Rotate(rotateAroundVector * (Time.deltaTime * rotationSpeed));
    }

    private void OnDrawGizmos()
    {
        var position = transform.position;
        Vector3 lineEnd = position + rotationDirection * rotationSpeed;

        Gizmos.DrawLine(position, lineEnd);
    }
}
