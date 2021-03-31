using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _planeList;
    [SerializeField]
    private List<GameObject> _planeModel;
    [SerializeField]
    private float rotateValue;
    private int select;

    // Start is called before the first frame update
    void Start()
    {
        select = Random.Range(0, 9);
        Debug.Log(select);
        UpdatePlane(select);
    }

    void UpdatePlane(int model)
    {
        // Y 값 18.6 부터 0.4씩 떨어트리면서 0.0까지 생성
        float rotate = 0.0f;
        for (float y = 18.6f; y >= 0.0f; y -= 0.4f)
        {
            GameObject gameObject = Instantiate(_planeModel[model], new Vector3(0, y, 0), Quaternion.identity);
            gameObject.transform.Rotate(Vector3.up, rotate);
            rotate -= rotateValue * Time.deltaTime;
            gameObject.transform.parent = _planeList.transform;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
