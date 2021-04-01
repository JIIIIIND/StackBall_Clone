﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchEvent : MonoBehaviour
{
    private Collider    _coll;
    private bool        _gameEnd;
    
    // Start is called before the first frame update
    void Start()
    {
        _coll = GetComponent<Collider>();
        _gameEnd = false;
    }

    private void CheckTouch()
    {
        if (Input.touchCount > 0)
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
        if (!_gameEnd)
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
            Destroy(other.gameObject);
        else
        {
            _gameEnd = true;
            _coll.isTrigger = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckTouch();
    }
}
