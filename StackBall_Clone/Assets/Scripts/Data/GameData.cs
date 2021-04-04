﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class GameData
{
    public int _select;
    public int _level = 1;
    public bool _gameEnd = false;
    public int _bestScore = 0;
    public Color _ballColor;
    public Color _planeColor;
}
