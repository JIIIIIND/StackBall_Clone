using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchEvent : MonoBehaviour
{
    private Collider    _coll;
    [SerializeField]
    private PlaneManager _manager;
    [SerializeField]
    private GameObject _nextRound;
    [SerializeField]
    private LevelUI _levelUI;
    // Start is called before the first frame update
    void Start()
    {
        _coll = GetComponent<Collider>();
        DataManager.Instance.gameData._gameEnd = false;
    }

    private void CheckTouch()
    {
        if (Input.touchCount > 0 && !DataManager.Instance.gameData._gameEnd)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved ||
                touch.phase == TouchPhase.Stationary)
            {
                _coll.isTrigger = true;
            }
            else if (touch.phase == TouchPhase.Canceled ||
                touch.phase == TouchPhase.Ended)
            {
                _coll.isTrigger = false;
            }
        }
        if (!DataManager.Instance.gameData._gameEnd)
        {
            if (Input.GetMouseButton(0))
            {
                _coll.isTrigger = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                _coll.isTrigger = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Plane")
        {
            GameObject parent = other.transform.parent.gameObject;
            Destroy(parent);
            _manager._planeCnt--;
            _levelUI._score++;
        }
        else
        {
            DataManager.Instance.gameData._gameEnd = true;
            _coll.isTrigger = false;
            DataManager.Instance.SaveData();
            _nextRound.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckTouch();
    }
}
