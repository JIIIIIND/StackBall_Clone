using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneManager : MonoBehaviour
{
    public int _planeCnt;
    [SerializeField]
    private GameObject _planes;
    [SerializeField]
    private List<PlainData> _planeModel;
    [SerializeField]
    private float _startPosition;
    [SerializeField]
    private float _rotateValue;
    private CreatePlane _create;
    [SerializeField]
    private Material[] materials;
    [SerializeField]
    private float _colorIncrease;

    // Start is called before the first frame update
    void Start()
    {
        UpdatePlane();
    }

    private void CleanPanel()
    {
        int childCount = _planes.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Destroy(_planes.transform.GetChild(i).gameObject);
        }
    }

    private int[] GetRandomNumber(int size, int cnt)
    {
        int[] blackArr = new int[size];
        ArrayList list = new ArrayList();

        for (int i = 0; i < cnt; i++)
            list.Add(i);
        for (int idx = 0; idx < size; idx++)
        {
            int listIdx = Random.Range(0, list.Count);
            blackArr[idx] = (int)list[listIdx];
            list.RemoveAt(listIdx);
        }
        return blackArr;
    }

    void SelectColor()
    {
        DataManager.Instance.gameData._planeColor = materials[Random.Range(0, materials.Length)].color;
        while (DataManager.Instance.gameData._planeColor ==
            DataManager.Instance.gameData._ballColor)
            DataManager.Instance.gameData._planeColor = materials[Random.Range(0, materials.Length)].color;
    }

    void ChangeColor()
    {
        Color color = DataManager.Instance.gameData._planeColor;
        DataManager.Instance.gameData._planeColor = new Color(
            color.r + _colorIncrease,
            color.g + _colorIncrease,
            color.b + _colorIncrease);
    }
    public void UpdatePlane()
    {
        float rotate = 0.0f;
        float y = _startPosition;
        if (DataManager.Instance.gameData._gameEnd)
            DataManager.Instance.gameData._select = Random.Range(0, _planeModel.Count);
        _planeCnt = 0;
        int panelSize = _planeModel[DataManager.Instance.gameData._select]._cnt;
        _create =
            _planeModel[DataManager.Instance.gameData._select]._model.
            GetComponent<CreatePlane>();
        CleanPanel();
        SelectColor();
        while (y >= 0.0f)
        {
            int rand = Random.Range(1, 10);
            int[] blackArr = GetRandomNumber(Random.Range(0, panelSize - 1), panelSize);
            for (int i = 0; i < rand; i++)
            {
                GameObject gameObject = Instantiate(_planeModel[DataManager.Instance.gameData._select]._model,
                    new Vector3(0, 0, 0), Quaternion.identity);
                gameObject.transform.parent = _planes.transform;
                _create.Create(panelSize, blackArr, y, gameObject);
                gameObject.transform.Rotate(Vector3.up, rotate);
                rotate -= _rotateValue * Time.deltaTime;
                y -= 0.4f;
                _planeCnt++;
                if (y <= 0.0f)
                    break;
                ChangeColor();
            }
        }
        DataManager.Instance.gameData._gameEnd = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
