using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextRound : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Text _text;
    private string _form = "Level ";

    private void Start()
    {
        _text.text =
            _form + DataManager.Instance.gameData._level.ToString(); 
    }

    private void OnEnable()
    {
        DataManager.Instance.gameData._level =
            DataManager.Instance.gameData._level + 1;
        _text.text =
            _form + DataManager.Instance.gameData._level.ToString();
    }

    public void NextRoundStart(BallMovement _ballCtl)
    {
        //버튼 눌린 경우
        //공의 위치 초기화
        //
        _ballCtl.InitRound();
        this.gameObject.SetActive(false);
    }
}
