
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Launcher))]
[RequireComponent(typeof(LineRenderer))]
public class DrawProjection : MonoBehaviour
{
    Launcher launcher;
    LineRenderer lineRenderer;
    [SerializeField] private GameObject target_OBJ;

    [SerializeField] private int numPoints = 50;
    [SerializeField] private float timeBerweenPoints = 0.1f;
    [SerializeField] private LayerMask collidableLayers;

    private void Start()
    {
        target_OBJ = Instantiate(target_OBJ, transform.position, Quaternion.identity);
        launcher = GetComponent<Launcher>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        //lineRenderer.positionCount = numPoints;
        List<Vector3> points = new List<Vector3>();
        Vector3 startingPos = launcher.ball_Origin.position;
        Vector3 startingVel = launcher.ball_Origin.up * launcher.BlastPower;

        for (float i = 0; i < numPoints; i += timeBerweenPoints)
        {
            Vector3 newPoint = startingPos + i * startingVel;
            newPoint.y = startingPos.y + startingVel.y * i + Physics.gravity.y / 2f * i * i;
            points.Add(newPoint);

            Vector3 rayStart = points[points.Count > 1 ? points.Count - 2 : points.Count - 1];
            Vector3 rayTarget = points[points.Count - 1];
            Vector3 direction = rayStart - rayTarget;

            //Debug.DrawRay(points[points.Count - 1], direction, Color.blue, 1);

            RaycastHit hit;
            if(Physics.Linecast(rayStart, rayTarget, out hit, collidableLayers))
            {
                //Debug.Log("hit " + hit.collider.name + points.Count);
                lineRenderer.positionCount = points.Count;
                target_OBJ.transform.position = hit.point;
                break;
            }
            /*
            if (Physics.OverlapSphere(newPoint, 2, collidableLayers).Length > 0)
            {
                lineRenderer.positionCount = points.Count;
                break;
            }*/
        }

        lineRenderer.SetPositions(points.ToArray());
    }

    private void OnDestroy()
    {
        Destroy(target_OBJ);
    }
}
