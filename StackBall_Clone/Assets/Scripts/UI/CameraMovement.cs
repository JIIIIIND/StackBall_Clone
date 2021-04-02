using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject _sphere;
    [SerializeField]
    private float _height;
    private float _y;

    // Start is called before the first frame update
    void Start()
    {
        _y = _sphere.transform.position.y;
    }

    public void UpdatePosition()
    {
        _y = _sphere.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        _y = Mathf.Min(_y, _sphere.transform.position.y);
        this.transform.position = new Vector3(
            transform.position.x,
            _height + _y,
            transform.position.z);
    }
}
