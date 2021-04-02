﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUI : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Text _level;
    [SerializeField]
    private UnityEngine.UI.Text _nextLevel;
    [SerializeField]
    private PlaneManager _manager;
    private int _maxCnt;
    [SerializeField]
    private UnityEngine.UI.Slider _slider;
    
    // Start is called before the first frame update
    void Start()
    {
        UpdateLevel();
        _maxCnt = 0;
    }

    public void UpdateLevel()
    {
        int level = DataManager.Instance.gameData._level;
        _level.text = level.ToString();
        _nextLevel.text = (level + 1).ToString();
        DataManager.Instance.SaveData();
    }

    // Update is called once per frame
    void Update()
    {
        if (_maxCnt == 0)
            _maxCnt = _manager._planeCnt;
        else
        {
            float ratio = 1 - ((float)_manager._planeCnt / (float)_maxCnt);
            _slider.value = ratio;
        }
    }
}
