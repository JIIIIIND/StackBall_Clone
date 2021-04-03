using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshDeformer : MonoBehaviour
{
    private Mesh _deformingMesh;
    private Vector3[] _originVertices, _displacedVertices;
    private Vector3[] _vertexVelocities;
    [SerializeField]
    private float _springForce = 20f;
    [SerializeField]
    private float _damping = 5f;
    void Start()
    {
        _deformingMesh = GetComponent<MeshFilter>().mesh;
        _originVertices = _deformingMesh.vertices;
        _displacedVertices = new Vector3[_originVertices.Length];
        for (int i = 0; i < _originVertices.Length; i++)
            _displacedVertices[i] = _originVertices[i];
        _vertexVelocities = new Vector3[_originVertices.Length];
    }

    public void AddDeformingForce(Vector3 point, float force)
    {
        Debug.Log("Deforming force to " + point + " " + force + "Length: " + _displacedVertices.Length);
        for (int i = 0; i < _displacedVertices.Length; i++)
        {
            AddForceToVertex(i, point, force);
        }
    }

    private void AddForceToVertex(int i, Vector3 point, float force)
    {
        Vector3 pointToVertex = _displacedVertices[i] - point;
        float attenuatedForce = force / (1f + pointToVertex.sqrMagnitude);
        float velocity = attenuatedForce * Time.deltaTime;
        _vertexVelocities[i] += pointToVertex.normalized * velocity;
    }
    private void UpdateVertex(int i)
    {
        Vector3 velocity = _vertexVelocities[i];
        Vector3 displacement = _displacedVertices[i] - _originVertices[i];
        velocity -= displacement * _springForce * Time.deltaTime;
        velocity *= 1f - _damping * Time.deltaTime;
        _vertexVelocities[i] = velocity;
        _displacedVertices[i] += velocity * Time.deltaTime;
    }
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < _displacedVertices.Length; i++)
        {
            UpdateVertex(i);
        }
        _deformingMesh.vertices = _displacedVertices;
        _deformingMesh.RecalculateNormals();
    }
}
