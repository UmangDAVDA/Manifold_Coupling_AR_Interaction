using UnityEngine;

public class arfocus : MonoBehaviour
{
    public Camera targetCamera;        // camera to follow (leave null to use Camera.main)
    public Vector3 offset = new Vector3(0f, 0f, 1f); // local offset from camera (z-forward)
    public bool matchRotation = true;  // rotate to match camera rotation
    public float positionSmooth = 10f; // 0 = immediate, >0 = smoothing
    public float rotationSmooth = 0f;

    Transform camT;

    void Start()
    {
        if (targetCamera == null) targetCamera = Camera.main;
        camT = targetCamera ? targetCamera.transform : null;
    }

    void LateUpdate()
    {
        if (camT == null) return;

        // Desired world position: camera transform + camera's rotation * offset
        Vector3 desiredPos = camT.TransformPoint(offset);
        if (positionSmooth > 0f)
            transform.position = Vector3.Lerp(transform.position, desiredPos, Time.deltaTime * positionSmooth);
        else
            transform.position = desiredPos;

        if (matchRotation)
        {
            Quaternion desiredRot = camT.rotation;
            if (rotationSmooth > 0f)
                transform.rotation = Quaternion.Slerp(transform.rotation, desiredRot, Time.deltaTime * rotationSmooth);
            else
                transform.rotation = desiredRot;
        }
    }
}
