using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextRound : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Text _completeLevel;
    private string _form = "Level ";
    [SerializeField]
    private GameObject _nextLevel;
    [SerializeField]
    private GameObject _gameOver;
    private bool _gameEnd;
    [SerializeField]
    private LevelUI _levelUI;

    private void Start()
    {
        _completeLevel.text =
            _form + DataManager.Instance.gameData._level.ToString(); 
    }

    private void OnEnable()
    {
        if (DataManager.Instance.gameData._gameEnd)
        {
            _gameEnd = true;
            DataManager.Instance.gameData._level =
            DataManager.Instance.gameData._level + 1;
            _completeLevel.text =
                _form + DataManager.Instance.gameData._level.ToString();
            _nextLevel.SetActive(true);
        }
        else
        {
            _gameEnd = false;
            DataManager.Instance.gameData._gameEnd = true;
            _gameOver.SetActive(true);
        }
    }

    public void NextRoundStart(BallMovement _ballCtl)
    {
        //버튼 눌린 경우
        //공의 위치 초기화
        //
        _ballCtl.InitRound();
        DataManager.Instance.gameData._gameEnd = _gameEnd;
        if (_gameEnd == false)
        {
            _levelUI._score = 0;
            _gameOver.SetActive(false);
        }
        else
            _nextLevel.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
