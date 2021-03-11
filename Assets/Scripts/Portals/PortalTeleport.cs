using UnityEngine;

public class PortalTeleport : MonoBehaviour
{
    public PortalTeleport Other;

    public bool teleported = false;

    private void OnTriggerStay(Collider other)
    {
        float zPos = transform.worldToLocalMatrix.MultiplyPoint3x4(other.transform.position).z;
        Other.teleported = true;
        if (zPos < 0) Teleport(other.transform);
    }

    private void Teleport(Transform obj)
    {
        if(!teleported)
        {
            // Position
            Vector3 localPos = transform.worldToLocalMatrix.MultiplyPoint3x4(obj.position);
            localPos = new Vector3(-localPos.x, localPos.y, -localPos.z);
            obj.position = Other.transform.localToWorldMatrix.MultiplyPoint3x4(localPos);

            // Rotation
            Quaternion difference = Other.transform.rotation * Quaternion.Inverse(transform.rotation * Quaternion.Euler(0, 180, 0));
            obj.rotation = difference * obj.rotation;
        }
    }
    private void OnTriggerExit(Collider other) {
        teleported = false;
    }

}