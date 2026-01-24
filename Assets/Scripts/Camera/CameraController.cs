using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform whatToFollow;
    public float topDownDistance = 50;
    public Vector3 topDownAngle = new Vector3(60, 0, 0);
    public float topDownInterpolation = 10;

    private void LateUpdate()
    {
        if (whatToFollow == null)
            return;

        Vector3 followPosition = whatToFollow.position;

        Vector3 targetPosition = transform.position;
        Quaternion targetRotation = transform.rotation;

        float deltaTime = Time.fixedDeltaTime;


        targetRotation = Quaternion.Euler(topDownAngle);


        Vector3 desiredPosition = followPosition + targetRotation * Vector3.back * topDownDistance;


        float t = Mathf.Clamp01(topDownInterpolation <= 0 ? 1 : topDownInterpolation * deltaTime);
        targetPosition = Vector3.Lerp(targetPosition, desiredPosition, t);

        transform.position = targetPosition;
        transform.rotation = targetRotation;
    }
}
