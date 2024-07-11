using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetherRenderer : MonoBehaviour
{
    public Transform nerd;
    public Transform jock;
    public float maxDistance = 3f;
    private LineRenderer lineRenderer;
    private DistanceJoint2D nerdJoint;
    private DistanceJoint2D jockJoint;

    
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer != null)
        {
            lineRenderer.positionCount = 2;
        }

        // Add DistanceJoint2D components if not already added
        nerdJoint = nerd.GetComponent<DistanceJoint2D>();
        if (nerdJoint == null)
        {
            nerdJoint = nerd.gameObject.AddComponent<DistanceJoint2D>();
        }

        jockJoint = jock.GetComponent<DistanceJoint2D>();
        if (jockJoint == null)
        {
            jockJoint = jock.gameObject.AddComponent<DistanceJoint2D>();
        }

        // Configure the joints
        nerdJoint.maxDistanceOnly = true;
        nerdJoint.distance = maxDistance;

        jockJoint.maxDistanceOnly = true;
        jockJoint.distance = maxDistance;

    }

    void Update()
    {
        if (nerd != null && jock != null)
        {
            if (lineRenderer != null)
            {
                // Update the line renderer to visually connect the characters
                Vector3 nerdMidPoint = new Vector3(nerd.position.x, nerd.position.y + 0.2f, nerd.position.z);
                Vector3 jockMidPoint = new Vector3(jock.position.x, jock.position.y + 0.2f, jock.position.z);

                lineRenderer.SetPosition(0, nerdMidPoint);
                lineRenderer.SetPosition(1, jockMidPoint);
            }
        }
        else 
        {
            Debug.LogError("Nerd or Jock transform is not assigned!");
        }
    }

}
