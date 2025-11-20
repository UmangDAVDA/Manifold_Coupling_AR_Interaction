// using UnityEngine;

// public class fixcamerapos : MonoBehaviour
// {
//     public float distance = 0.5f;

//     void Update()
//     {
//         if (Camera.main != null)
//         {
//             transform.position = Camera.main.transform.position + Camera.main.transform.forward * distance;
//             transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
//         }
//     }
// }
using UnityEngine;

public class fixcamerapos : MonoBehaviour
{
    public Camera arCamera;     // AR Camera reference
    public float distance = 1f; // distance in front of camera

    void LateUpdate()
    {
        if (arCamera == null) return;

        // Keep position always in front of camera
        transform.position = arCamera.transform.position + arCamera.transform.forward * distance;

        // Optional: make object always face same direction as camera
        transform.rotation = arCamera.transform.rotation;
    }
}
