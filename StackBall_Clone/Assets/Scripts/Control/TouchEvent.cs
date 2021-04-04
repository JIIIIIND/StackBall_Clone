using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Gauge { READY, START, FULL }

public class TouchEvent : MonoBehaviour
{
    private Collider    _coll;
    [SerializeField]
    private PlaneManager _manager;
    [SerializeField]
    private GameObject _nextRound;
    [SerializeField]
    private LevelUI _levelUI;
    [SerializeField]
    private GameObject _gaugeObj;
    [SerializeField]
    private UnityEngine.UI.Image _gaugeSprite;
    private int _gaugeReadyCnt = 0;
    private Gauge _gauge;
    private Coroutine _gaugeRoutine;
    [SerializeField]
    private GameObject _gaugeParticle;
    [SerializeField]
    private GameObject _trail;
    [SerializeField]
    private AudioSource[] _sounds;

    void Start()
    {
        _coll = GetComponent<Collider>();
        DataManager.Instance.gameData._gameEnd = false;
        _gauge = Gauge.READY;
        _gaugeRoutine = null;
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
                if (_gauge == Gauge.FULL)
                {
                    _gaugeParticle.SetActive(true);
                    _trail.SetActive(false);
                }
            }
            else if (touch.phase == TouchPhase.Canceled ||
                touch.phase == TouchPhase.Ended)
            {
                _coll.isTrigger = false;
                _trail.SetActive(true);
                _gaugeParticle.SetActive(false);
            }
        }
        if (!DataManager.Instance.gameData._gameEnd)
        {
            if (Input.GetMouseButton(0))
            {
                _coll.isTrigger = true;
                if (_gauge == Gauge.FULL)
                {
                    _gaugeParticle.SetActive(true);
                    _trail.SetActive(false);
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                _coll.isTrigger = false;
                _trail.SetActive(true);
                _gaugeParticle.SetActive(false);
            }
        }
    }

    private IEnumerator DecreaseGauge()
    {
        while (_gaugeSprite.fillAmount > 0)
        {
            _gaugeSprite.fillAmount -= 1f * Time.deltaTime;
            yield return null;
        }
        _gauge = Gauge.READY;
        _gaugeObj.SetActive(false);
        _gaugeReadyCnt = 0;
        _gaugeParticle.SetActive(false);
        _trail.SetActive(true);
        _gaugeRoutine = null;
    }

    private void GaugeControl()
    {
        if (_gauge == Gauge.READY)
        {
            _gaugeReadyCnt++;
            if (_gaugeReadyCnt >= 10)
            {
                _gauge = Gauge.START;
                _gaugeReadyCnt = 0;
                _gaugeObj.SetActive(true);
            }
        }
        else if (_gauge == Gauge.START)
        {
            _gaugeSprite.fillAmount += 0.1f;
            if (_gaugeRoutine == null)
                _gaugeRoutine = StartCoroutine(DecreaseGauge());
            if (_gaugeSprite.fillAmount >= 1)
            {
                _sounds[3].Play();
                _gauge = Gauge.FULL;
            }
        }
        else
        {
            _levelUI._score++;
        }
    }

    public void GameClear()
    {
        DataManager.Instance.gameData._gameEnd = true;
        _gaugeObj.SetActive(false);
        _nextRound.SetActive(true);
        _coll.isTrigger = false;
        DataManager.Instance.SaveData();
        _sounds[2].Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((_gauge == Gauge.FULL && other.gameObject.tag == "Black")
                || other.gameObject.tag == "Plane")
        {
            GameObject parent = other.transform.parent.gameObject;
            parent.GetComponent<PlaneDestroy>().DestroyPlane(other.gameObject);
            _manager._planeCnt--;
            GaugeControl();
            _levelUI._score++;
            _sounds[0].Play();
        }
        else if (other.gameObject.tag == "Black")
        {
            _gaugeObj.SetActive(false);
            _nextRound.SetActive(true);
            _coll.isTrigger = false;
            DataManager.Instance.SaveData();
            _sounds[1].Play();
        }
        else
            GameClear();
    }

    // Update is called once per frame
    void Update()
    {
        CheckTouch();
    }
}
