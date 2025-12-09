using UnityEngine;

public class VRObjectInfoPanel : MonoBehaviour
{
    [SerializeField] private GameObject infoCanvas;

    private void Start()
    {
        if (infoCanvas != null)
            infoCanvas.SetActive(false);
    }

    public void Toggle()
    {
        if (infoCanvas == null)
        {
            Debug.LogWarning("InfoCanvas is not assigned on " + gameObject.name);
            return;
        }

        bool newState = !infoCanvas.activeSelf;
        infoCanvas.SetActive(newState);
        Debug.Log("Info panel for " + gameObject.name + " active = " + newState);
    }
}
