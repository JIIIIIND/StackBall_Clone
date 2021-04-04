using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePlane : MonoBehaviour
{
    [SerializeField]
    private GameObject _baseTile;
    [SerializeField]
    private float _angle;
    public List<GameObject> _planeList;

    public void Create(int cnt, int[] blackArr, float posY, GameObject parent)
    {
        int blackIdx = 0;
        float angle = 0;
        GameObject tile;
        CreatePlane parentPlane = parent.GetComponent<CreatePlane>();

        for (int i = 0; i < cnt; i++)
        {
            tile = Instantiate(_baseTile,
                    new Vector3(0, posY, 0), Quaternion.Euler(90, angle, 0));
            tile.GetComponent<Renderer>().material.color =
                DataManager.Instance.gameData._planeColor;
            if (blackIdx < blackArr.Length && i == blackArr[blackIdx])
            {
                // black Tile로 생성
                tile.tag = "Black";
                tile.GetComponent<Renderer>().material.color = Color.black;
                blackIdx++;
            }
            tile.transform.parent = parent.transform;
            angle += _angle;
            parentPlane._planeList.Add(tile);
        }
    }
}
