using System.Collections.Generic;
using System.Collections;
using System.Drawing; 
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    [SerializeField] private float grappleLength;
    [SerializeField] private LayerMask grappleLayer;
    [SerializeField] private LineRenderer rope;

    private Vector3 grapplePoint;
    private DistanceJoint2D joint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        joint = gameObject.GetComponent<DistanceJoint2D>();
        joint.enabled = false;
        rope.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(
                origin: Camera.main.ScreenToWorldPoint(Input.mousePosition),
                direction: Vector2.zero,
                distance: Mathf.Infinity,
                layerMask: grappleLayer);

            if (hit.collider != null)
            {
                Vector3 hitPoint3 = new Vector3(hit.point.x, hit.point.y, 0f);

                float maxDistSqr = grappleLength * grappleLength;
                float distSqr = (hitPoint3 - transform.position).sqrMagnitude;

                if (distSqr <= maxDistSqr)
                {
                    grapplePoint = hitPoint3;

                    joint.connectedAnchor = grapplePoint;
                    joint.enabled = true;

                    float actualDistance = Mathf.Sqrt(distSqr);
                    float ropeStartDistance = Mathf.Min(actualDistance, grappleLength);

                    joint.distance = ropeStartDistance;

                    rope.SetPosition(0, grapplePoint);
                    rope.SetPosition(1, transform.position);
                    rope.enabled = true;
                }
            }


        }
        if (Input.GetMouseButtonUp(0))
        {
            joint.enabled = false;
            rope.enabled = false;
        }

        if(rope.enabled == true)
        {
            rope.SetPosition(1, transform.position);
        }
    }
}
