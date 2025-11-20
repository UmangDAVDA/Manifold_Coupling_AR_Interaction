using UnityEngine;

public class Working : MonoBehaviour
{
    public GameObject parent1;      // moving parent
    public GameObject parent2;      // target parent
    public GameObject parent3;      // object to rotate
    public float moveSpeed = 5f;       // speed to move children
    public float joinDistance = 0.5f; // distance to stop before overlapping
    public float rotateSpeed = 180f;   // degrees per second
    public float rotateAngle = 360f;   // total rotation after joining

    public GameObject particled;

    private bool joined = false;
    private float rotated = 0f;        // track how much parent3 has rotated

    void Start()
    {
        // Disable particle initially
        if (particled != null) particled.SetActive(false);

        // ✅ Attach particle to parent2 and set position exactly at parent2
        if (particled != null && parent2 != null)
        {
            particled.transform.SetParent(parent2.transform);
            particled.transform.localPosition = Vector3.zero;
            particled.transform.localRotation = Quaternion.identity;
        }
    }

    void Update()
    {
        // --- Step 1: Move children until "touching distance" reached ---
        if (!joined)
        {
            int count = Mathf.Min(parent1.transform.childCount, parent2.transform.childCount);
            bool allJoined = true;

            for (int i = 0; i < count; i++)
            {
                Transform child1 = parent1.transform.GetChild(i);
                Transform child2 = parent2.transform.GetChild(i);

                float distance = Vector3.Distance(child1.position, child2.position);

                // Only move if distance is greater than joinDistance
                if (distance > joinDistance)
                {
                    Vector3 direction = (child2.position - child1.position).normalized;
                    child1.position += direction * moveSpeed * Time.deltaTime;
                    allJoined = false;
                }
            }

            if (allJoined)
            {
                joined = true;
                Debug.Log("All children reached stop distance! Starting rotation...");
            }
        }

        // --- Step 2: Rotate parent3 once after joining ---
        else if (rotated < rotateAngle)
        {
            float rotationStep = rotateSpeed * Time.deltaTime;
            if (rotated + rotationStep > rotateAngle)
                rotationStep = rotateAngle - rotated;

            parent3.transform.Rotate(0f, 0f, +rotationStep); // clockwise
            rotated += rotationStep;

            if (rotated >= rotateAngle)
            {
                Debug.Log("Rotation completed!");

                // ✅ Place particle exactly at parent2’s position before showing
                if (particled != null && parent2 != null)
                {
                    particled.transform.position = parent2.transform.position;
                    particled.SetActive(true);
                }
            }
        }
    }
}
