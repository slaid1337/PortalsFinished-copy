using UnityEngine;

public class Portal : MonoBehaviour
{
    public Portal Other;
    public Camera PortalView;
    private Vector3 _startPosition;

    private void Start()
    {
        Other.PortalView.targetTexture = new RenderTexture(Screen.width / 4, Screen.height / 4, 24);
        GetComponentInChildren<MeshRenderer>().sharedMaterial.mainTexture = Other.PortalView.targetTexture;
        _startPosition = transform.position;
    }
    
    private void Update()
    {
        // Position
        Vector3 lookerPosition = Other.transform.worldToLocalMatrix.MultiplyPoint3x4(Camera.main.transform.position);
        lookerPosition = new Vector3(-lookerPosition.x, lookerPosition.y, -lookerPosition.z);
        PortalView.transform.localPosition = lookerPosition;

        // Rotation
        Quaternion difference = transform.rotation * Quaternion.Inverse(Other.transform.rotation * Quaternion.Euler(0,180,0));
        PortalView.transform.rotation = difference * Camera.main.transform.rotation;

        // Clipping
        PortalView.nearClipPlane = lookerPosition.magnitude;
    }

    public void GoToStartPos()
    {
        transform.position = _startPosition;
    }
}