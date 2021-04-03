using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshDeformerInput : MonoBehaviour
{
    [SerializeField]
    private float _force = 10f;
    [SerializeField]
    private float _forceOffset = 0.1f;
    void Start()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        MeshDeformer deformer = collision.collider.GetComponent<MeshDeformer>();
        if (deformer)
        {
            ContactPoint[] contacts = new ContactPoint[10];
            int numContacts = collision.GetContacts(contacts);
            for (int i = 0; i < numContacts; i++)
            {
                Debug.Log(i + " deforming " + contacts[i].point);
                Vector3 point = contacts[i].point;
                point += contacts[i].normal * _forceOffset;
                deformer.AddDeformingForce(point, _force);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
