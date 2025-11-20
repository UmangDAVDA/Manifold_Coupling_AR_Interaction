using UnityEngine;

public class setplacement : MonoBehaviour
{
     public MonoBehaviour placementScriptWithPrefab; // drag the placement script component here
    public GameObject myPrefab;

    void Start()
    {
        if (placementScriptWithPrefab == null || myPrefab == null) return;
        var field = placementScriptWithPrefab.GetType().GetField("placementPrefab");
        if (field != null) field.SetValue(placementScriptWithPrefab, myPrefab);
    }
}
