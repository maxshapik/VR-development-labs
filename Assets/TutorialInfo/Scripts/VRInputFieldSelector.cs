using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class VRInputFieldSelector : MonoBehaviour, IPointerClickHandler
{
    public TMP_InputField field;

    private void Reset()
    {
        if (field == null)
            field = GetComponent<TMP_InputField>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (field == null)
            field = GetComponent<TMP_InputField>();

        if (VRKeyboardManager.Instance != null)
        {
            VRKeyboardManager.Instance.SetTargetField(field);
        }
    }
}
