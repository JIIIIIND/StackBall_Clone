using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Text _score;
    [SerializeField]
    private UnityEngine.UI.Text _bestScore;
    [SerializeField]
    private LevelUI _levelUI;

    private void OnEnable()
    {
        int score = _levelUI._score;
        int bestScore = DataManager.Instance.gameData._bestScore;
        DataManager.Instance.gameData._bestScore = Mathf.Max(score, bestScore);
        bestScore = DataManager.Instance.gameData._bestScore;
        _bestScore.text =
            bestScore.ToString();
        _score.text = score.ToString();
    }
}
