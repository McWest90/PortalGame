using System;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Portal Other;
    public Camera PortalView;

    // Start is called before the first frame update
    void Start()
    {
        Other.PortalView.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        // if the render is on the main object
        // GetComponent<MeshRenderer>().sharedMaterial.mainTexture = Other.PortalView.targetTexture;
        
        // if the render is on the empty object
        GetComponentInChildren<MeshRenderer>().sharedMaterial.mainTexture = Other.PortalView.targetTexture;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookerPosition = Other.transform.worldToLocalMatrix.MultiplyPoint3x4(Camera.main.transform.position);
        lookerPosition = new Vector3(-lookerPosition.x, lookerPosition.y, -lookerPosition.z);
        PortalView.transform.localPosition = lookerPosition;

        Quaternion difference = transform.rotation * Quaternion.Inverse(Other.transform.rotation * Quaternion.Euler(0,-180,0));
        PortalView.transform.rotation = difference * Camera.main.transform.rotation;

        PortalView.nearClipPlane = lookerPosition.magnitude;
    }
}
