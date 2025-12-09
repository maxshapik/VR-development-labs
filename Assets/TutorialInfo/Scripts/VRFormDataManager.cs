using UnityEngine;
using TMPro;

public class VRFormDataManager : MonoBehaviour
{
    [Header("Поля форми")]
    public TMP_InputField nameField;
    public TMP_InputField ageField;
    public TMP_InputField descriptionField;

    private const string NameKey = "VR_FORM_NAME";
    private const string AgeKey = "VR_FORM_AGE";
    private const string DescKey = "VR_FORM_DESC";

    private void Start()
    {
        LoadData();
    }

    public void SaveData()
    {
        if (nameField != null)
            PlayerPrefs.SetString(NameKey, nameField.text);

        if (ageField != null)
            PlayerPrefs.SetString(AgeKey, ageField.text);

        if (descriptionField != null)
            PlayerPrefs.SetString(DescKey, descriptionField.text);

        PlayerPrefs.Save();
        Debug.Log("VR Form: Data saved");
    }

    public void LoadData()
    {
        if (nameField != null && PlayerPrefs.HasKey(NameKey))
            nameField.text = PlayerPrefs.GetString(NameKey);

        if (ageField != null && PlayerPrefs.HasKey(AgeKey))
            ageField.text = PlayerPrefs.GetString(AgeKey);

        if (descriptionField != null && PlayerPrefs.HasKey(DescKey))
            descriptionField.text = PlayerPrefs.GetString(DescKey);

        Debug.Log("VR Form: Data loaded");
    }

    public void ClearData()
    {
        if (nameField != null) nameField.text = "";
        if (ageField != null) ageField.text = "";
        if (descriptionField != null) descriptionField.text = "";

        Debug.Log("VR Form: Fields cleared");
    }

    public void CloseForm()
    {
        gameObject.SetActive(false);
        Debug.Log("VR Form: Closed");
    }
}

