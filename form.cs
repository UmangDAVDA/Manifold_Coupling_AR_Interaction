using UnityEngine;
using TMPro; // If using TextMeshPro

public class form : MonoBehaviour
{
    [Header("UI References")]
    public TMP_InputField nameInput;
    public TMP_InputField mobileInput;
    public GameObject formPanel;
    public TMP_Text outputText;

    public void OnSubmit()
    {
        string name = nameInput.text;
        string mobile = mobileInput.text;

        // Validation check
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(mobile))
        {
            outputText.text = "⚠️ Please fill in all fields.";
            return;
        }

        // Print or display the values
        outputText.text = $"Logged in !\nName: {name}\nMobile: {mobile}";

        // Hide the form after submission
        formPanel.SetActive(false);

        Debug.Log($"Form Submitted — Name: {name}, Mobile: {mobile}");
    }
}
