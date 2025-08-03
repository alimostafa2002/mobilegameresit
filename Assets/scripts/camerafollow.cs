using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerafollow : MonoBehaviour
{
    [SerializeField] private Transform target;            // The object the camera follows
    [SerializeField] private float smoothing = 5f;        // How smoothly the camera follows
    [SerializeField] private Transform minBoundary;       // Bottom-left limit
    [SerializeField] private Transform maxBoundary;       // Top-right limit

    private Vector3 offset;  // The initial distance between camera and target

    void Start()
    {
        if (target != null)
        {
            offset = transform.position - target.position;
        }
    }

    void LateUpdate()
    {
        if (target == null) return; // ✅ Stop update if target is destroyed

        var targetCamPos = target.position + offset;

        targetCamPos.x = Mathf.Clamp(targetCamPos.x, minBoundary.position.x, maxBoundary.position.x);
        targetCamPos.y = Mathf.Clamp(targetCamPos.y, minBoundary.position.y, maxBoundary.position.y);

        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }

    // ✅ Optional: public method to clear the target from outside
    public void ClearTarget()
    {
        target = null;
    }
}
