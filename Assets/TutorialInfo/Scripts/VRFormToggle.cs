using UnityEngine;
using UnityEngine.XR;

public class VRFormToggleByController : MonoBehaviour
{
    [SerializeField] private GameObject formCanvas;
    [SerializeField] private XRNode controllerNode = XRNode.RightHand;

    private InputDevice device;
    private bool prevPrimaryPressed = false;

    private void Start()
    {
        device = InputDevices.GetDeviceAtXRNode(controllerNode);

        if (formCanvas != null)
            formCanvas.SetActive(false);
    }

    private void Update()
    {
        if (!device.isValid)
        {
            device = InputDevices.GetDeviceAtXRNode(controllerNode);
        }

        bool secondaryPressed = false;

        if (device.TryGetFeatureValue(CommonUsages.secondaryButton, out secondaryPressed))
        {
            if (secondaryPressed && !prevPrimaryPressed)
            {
                Debug.Log("Secondary button pressed on " + controllerNode);
                ToggleForm();
            }

            prevPrimaryPressed = secondaryPressed;
        }
    }

    private void ToggleForm()
    {
        if (formCanvas == null)
        {
            Debug.LogWarning("FormCanvas is not assigned in VRFormToggleByController!");
            return;
        }

        bool newState = !formCanvas.activeSelf;
        formCanvas.SetActive(newState);
        Debug.Log("Form Canvas active = " + newState);

        if (newState)
        {
            PositionInFrontOfHead();
        }
    }

    private void PositionInFrontOfHead()
    {
        if (formCanvas == null || Camera.main == null) return;

        Transform head = Camera.main.transform;

        Vector3 forwardFlat = new Vector3(head.forward.x, 0, head.forward.z).normalized;
        if (forwardFlat.sqrMagnitude < 0.001f)
            forwardFlat = head.forward;

        Vector3 targetPos = head.position + forwardFlat * 1.5f + Vector3.down * 0.1f;
        formCanvas.transform.position = targetPos;

        formCanvas.transform.LookAt(head);
        formCanvas.transform.Rotate(0, 180f, 0f);
    }
}
