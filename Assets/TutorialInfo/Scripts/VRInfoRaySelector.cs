using UnityEngine;
using UnityEngine.XR;

public class VRInfoRaySelector : MonoBehaviour
{
    [Header("Звідки стріляємо променем")]
    [SerializeField] private Transform rayOrigin;
    [SerializeField] private float rayDistance = 5f;
    [SerializeField] private LayerMask infoLayerMask;

    [Header("Яку руку та кнопку слухаємо")]
    [SerializeField] private XRNode controllerNode = XRNode.RightHand;

    private InputDevice device;
    private bool prevPrimaryPressed = false;

    private void Start()
    {
        device = InputDevices.GetDeviceAtXRNode(controllerNode);
    }

    private void Update()
    {
        if (!device.isValid)
            device = InputDevices.GetDeviceAtXRNode(controllerNode);

        bool primaryPressed = false;

        if (device.TryGetFeatureValue(CommonUsages.primaryButton, out primaryPressed))
        {
            if (primaryPressed && !prevPrimaryPressed)
            {
                TryToggleInfo();
            }

            prevPrimaryPressed = primaryPressed;
        }
    }

    private void TryToggleInfo()
    {
        if (rayOrigin == null)
        {
            Debug.LogWarning("Ray origin is not assigned!");
            return;
        }

        Ray ray = new Ray(rayOrigin.position, rayOrigin.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance, infoLayerMask))
        {
            var infoPanel = hit.collider.GetComponent<VRObjectInfoPanel>();
            if (infoPanel != null)
            {
                infoPanel.Toggle();
            }
            else
            {
                Debug.Log("Hit object " + hit.collider.name + " but it has no VRObjectInfoPanel");
            }
        }
        else
        {
            Debug.Log("No InfoObject hit");
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (rayOrigin == null) return;
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(rayOrigin.position, rayOrigin.forward * rayDistance);
    }
}

