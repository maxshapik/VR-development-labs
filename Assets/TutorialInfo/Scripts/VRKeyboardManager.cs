using UnityEngine;
using TMPro;

public class VRKeyboardManager : MonoBehaviour
{
    public static VRKeyboardManager Instance;

    [Header("Поточне активне поле вводу")]
    public TMP_InputField currentField;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetTargetField(TMP_InputField field)
    {
        currentField = field;

        if (currentField != null)
        {
            currentField.Select();
            currentField.ActivateInputField();
        }
    }

    public void AppendChar(string c)
    {
        if (currentField == null) return;

        currentField.text += c;
        currentField.caretPosition = currentField.text.Length;
    }

    public void AddSpace()
    {
        AppendChar(" ");
    }

    public void Backspace()
    {
        if (currentField == null) return;
        if (string.IsNullOrEmpty(currentField.text)) return;

        currentField.text = currentField.text.Substring(0, currentField.text.Length - 1);
        currentField.caretPosition = currentField.text.Length;
    }

    public void ClearField()
    {
        if (currentField == null) return;

        currentField.text = "";
        currentField.caretPosition = 0;
    }
}

