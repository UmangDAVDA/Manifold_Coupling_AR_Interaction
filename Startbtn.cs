using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Startbtn : MonoBehaviour
{
    public GameObject workingObject;   // GameObject with Working script
    public GameObject workingdObject;  // GameObject with Workingd script
    public Button button;              // Assign the UI Button here

    private bool useFirst = true;
    private bool canClick = true;      // Track if button is allowed to be clicked

    void Start()
    {
        // Disable both scripts at the start
        if (workingObject != null) workingObject.SetActive(false);
        if (workingdObject != null) workingdObject.SetActive(false);

        if (button != null)
            button.onClick.AddListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        if (!canClick) return; // Ignore clicks during cooldown

        useFirst = !useFirst;

        if (workingObject != null) workingObject.SetActive(useFirst);
        if (workingdObject != null) workingdObject.SetActive(!useFirst);

        Debug.Log("Button clicked! Now using: " + (useFirst ? "Working GameObject" : "Workingd GameObject"));

        // Start 20-second cooldown
        StartCoroutine(ButtonCooldown());
    }

    private IEnumerator ButtonCooldown()
    {
        canClick = false;
        if (button != null)
            button.interactable = false; // visually disable button

        yield return new WaitForSeconds(10f); // wait 20 seconds

        canClick = true;
        if (button != null)
            button.interactable = true; // re-enable button
    }
}
