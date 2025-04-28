using Oculus.Interaction.DebugTree;
using UnityEngine;

public class RayGun : MonoBehaviour
{
    public OVRInput.RawButton shootingButton;
    public LineRenderer linePrefab;
    public Transform shootingPoint;
    public float maxLineDistance = 5;
    public float lineShowTimer = 0.1f;

    void Update()
    {
        if (OVRInput.GetDown(shootingButton))
        {
            //Debug.Log("Shoot");
            LineRenderer line = Instantiate(linePrefab);
            line.positionCount = 2;
            line.SetPosition(0, shootingPoint.position);
            Vector3 endPoint = shootingPoint.position + shootingPoint.forward * maxLineDistance;
            line.SetPosition(1, endPoint);
            Destroy(line.gameObject, lineShowTimer);
        }
    }
}
