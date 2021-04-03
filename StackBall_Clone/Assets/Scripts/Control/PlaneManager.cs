using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneManager : MonoBehaviour
{
    public int _planeCnt;
    [SerializeField]
    private GameObject _planeList;
    [SerializeField]
    private List<GameObject> _planeModel;
    [SerializeField]
    private float _startPosition;
    [SerializeField]
    private float _rotateValue;

    // Start is called before the first frame update
    void Start()
    {
        UpdatePlane();
    }

    public void UpdatePlane()
    {
        float rotate = 0.0f;
        if (DataManager.Instance.gameData._gameEnd)
            DataManager.Instance.gameData._select = Random.Range(0, _planeModel.Count);
        _planeCnt = 0;
        for (float y = _startPosition; y >= 0.0f; y -= 0.4f)
        {
            GameObject gameObject =
                Instantiate(_planeModel[DataManager.Instance.gameData._select],
                new Vector3(0, y, 0), Quaternion.identity);
            gameObject.transform.Rotate(Vector3.up, rotate);
            rotate -= _rotateValue * Time.deltaTime;
            gameObject.transform.parent = _planeList.transform;
            _planeCnt++;
        }
        DataManager.Instance.gameData._gameEnd = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
