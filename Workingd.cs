using UnityEngine;

public class Workingd : MonoBehaviour
{
    [Header("Parent Objects")]
    public GameObject parent1;  // moving parent
    public GameObject parent2;  // target parent (parent3 stays attached to this)
    public GameObject parent3;  // rotating part

    [Header("Animation Settings")]
    public float moveSpeed = 2f;       // speed of separation
    public float separationAmount = 0.5f;
    public float rotateSpeed = 180f;   // degrees per second
    public float rotateAngle = 360f;   // total rotation

    [Header("Particle Effects")]
    public GameObject particled;   // central particle
    public GameObject particlec1;  // attached to parent1
    public GameObject particlec2;  // attached to parent2

    private bool rotationDone = false;
    private bool separated = false;
    private float rotated = 0f;

    private Vector3 separationDirection;
    private float separationMoved = 0f;

    void Start()
    {
        // Disable particles initially
        if (particled) particled.SetActive(false);
        if (particlec1) particlec1.SetActive(false);
        if (particlec2) particlec2.SetActive(false);

        // ✅ Get direction for separation (line from parent1 to parent2)
        if (parent1 && parent2)
            separationDirection = (parent2.transform.position - parent1.transform.position).normalized;

        // ✅ Attach parent3 to parent2 (keep current world transform)
        if (parent3 && parent2)
        {
            parent3.transform.SetParent(parent2.transform, true);
        }

        // ✅ Attach particles to correct parents (keep their current position)
        AttachParticleToParent(particlec1, parent2);
        AttachParticleToParent(particlec2, parent1);
    }

    void Update()
    {
        // --- Step 1: Rotate parent3 vertically while attached to parent2 ---
        if (!rotationDone)
        {
            float rotationStep = rotateSpeed * Time.deltaTime;
            if (rotated + rotationStep > rotateAngle)
                rotationStep = rotateAngle - rotated;

            // Vertical rotation around local Z axis
            parent3.transform.Rotate(0f, 0f, -rotationStep, Space.Self);
            rotated += rotationStep;

            if (rotated >= rotateAngle)
            {
                rotationDone = true;
                Debug.Log("Rotation completed — starting separation.");

                // Activate particles
                if (particled) particled.SetActive(true);
                if (particlec1) particlec1.SetActive(true);
                if (particlec2) particlec2.SetActive(true);
            }
        }

        // --- Step 2: Separate parent1 and parent2 after rotation ---
        else if (!separated)
        {
            float moveStep = moveSpeed * Time.deltaTime;

            if (separationMoved < separationAmount)
            {
                parent1.transform.position -= separationDirection * moveStep;
                parent2.transform.position += separationDirection * moveStep;
                separationMoved += moveStep;
            }
            else
            {
                separated = true;
                Debug.Log("Separation complete!");
            }
        }
    }

    // ✅ Utility: Re-parent while keeping world position/rotation
    private void AttachParticleToParent(GameObject particle, GameObject parent)
    {
        if (!particle || !parent) return;
        Vector3 worldPos = particle.transform.position;
        Quaternion worldRot = particle.transform.rotation;
        particle.transform.SetParent(parent.transform, true);
        particle.transform.SetPositionAndRotation(worldPos, worldRot);
    }
}
