using UnityEngine;
using System.Linq;
using Agava.YandexGames;
using Newtonsoft.Json;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance;

    [SerializeField] private float saveInterval = 15f;
    private float timer = 0f;
    private IDataHandler[] _dataHandlers;
    private GameData _gameData;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        
        _dataHandlers = FindObjectsOfType<MonoBehaviour>().OfType<IDataHandler>().ToArray();
    }

    private void Start()
    {
        LoadAll();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= saveInterval)
        {
            SaveAll();
            timer = 0f;
        }
    }

    private void NewGame()
    {
        _gameData = new GameData();

        foreach (var dataHandler in _dataHandlers)
        {
            dataHandler.Load(_gameData, true);
        }
    }

    public void SaveAll()
    {
        _gameData = new GameData();

        foreach (var dataHandler in _dataHandlers)
        {
            dataHandler.Save(_gameData);
        }

        string json = JsonConvert.SerializeObject(_gameData);

        PlayerPrefs.SetString("Save", json);

        if (PlayerAccount.IsAuthorized)
            PlayerAccount.SetCloudSaveData(json);
    }

    public void LoadAll()
    {
        if (PlayerAccount.IsAuthorized)
        {
            PlayerAccount.GetCloudSaveData((data) => Load(data));
        }

        else
        {
            string data = PlayerPrefs.GetString("Save");
            Load(data);
        }
    }

    private void Load(string data)
    {
        _gameData = new GameData();

        if (string.IsNullOrEmpty(data))
        {
            NewGame();
            return;
        }

        _gameData = JsonConvert.DeserializeObject<GameData>(data);

        foreach (var dataHandler in _dataHandlers)
        {
            dataHandler.Load(_gameData);
        }
    }

    private void OnApplicationQuit()
    {
        SaveAll();
    }
}