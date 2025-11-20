using UnityEngine;

public class areplace : MonoBehaviour
{
    [Tooltip("Exact name of the sample GameObject to replace (case-sensitive).")]
    public string sampleName = "Icon";

    [Tooltip("Your replacement prefab (drag from Project window).")]
    public GameObject replacementPrefab;

    [Tooltip("Keep the new object's name the same as the sampleName (helps other code find it).")]
    public bool keepName = true;

    void Start()
    {
        if (replacementPrefab == null)
        {
            Debug.LogWarning("QuickReplaceAtStart: No replacementPrefab assigned.");
            return;
        }

        var sample = GameObject.Find(sampleName);
        if (sample == null)
        {
            Debug.LogWarning($"QuickReplaceAtStart: GameObject named '{sampleName}' not found in scene.");
            return;
        }

        Replace(sample);
    }

    void Replace(GameObject sample)
    {
        var parent = sample.transform.parent;
        var worldPos = sample.transform.position;
        var worldRot = sample.transform.rotation;
        var localScale = sample.transform.localScale;

        GameObject newObj = Instantiate(replacementPrefab, worldPos, worldRot, parent);
        newObj.transform.localScale = localScale;

        if (keepName) newObj.name = sample.name;

        Destroy(sample);
        Debug.Log($"QuickReplaceAtStart: Replaced '{sampleName}' with prefab '{replacementPrefab.name}'.");
    }
}
