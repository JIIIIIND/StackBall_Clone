using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    static GameObject _container;
    static GameObject Container
    {
        get
        {
            return _container;
        }
    }
    static DataManager _instance;
    public static DataManager Instance
    {
        get
        {
            if (!_instance)
            {
                _container = new GameObject();
                _container.name = "DataManager";
                _instance = _container.AddComponent(typeof(DataManager)) as DataManager;
                DontDestroyOnLoad(_container);
            }
            return _instance;
        }
    }

    public string _dataFile = "config.json";
    public GameData _gameData;
    public GameData gameData
    {
        get
        {
            if (_gameData == null)
            {
                Debug.Log("GameData load");
                LoadData();
                SaveData();
            }
            return _gameData;
        }
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + _dataFile;

        Debug.Log("Load data");
        if (File.Exists(path))
        {
            Debug.Log("file exists");
            string jsonData = File.ReadAllText(path);
            _gameData =
                JsonUtility.FromJson<GameData>(jsonData);
        }
        else
        {
            _gameData = new GameData();
            _gameData._select = Random.Range(0, 9);
            _gameData._level = 1;
        }
    }
    public void SaveData()
    {
        Debug.Log("Save Data");
        string path = Application.persistentDataPath + _dataFile;
        bool tmp = _gameData._gameEnd;
        _gameData._gameEnd = false;
        string jsonData = JsonUtility.ToJson(gameData);
        _gameData._gameEnd = tmp;
        File.WriteAllText(path, jsonData);
    }
    private void Start()
    {
        Debug.Log("DataManager Start");
        LoadData();
        SaveData();
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }
}
