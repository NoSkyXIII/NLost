using UnityEngine;
using Agava.YandexGames;

public class LeaderBoard : MonoBehaviour
{
    public static LeaderBoard Instance;

    [SerializeField] private int _showPlayers;
    [SerializeField] private LeaderBoardUI _leaderBoardUI;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void SetResult(int count)
    {
        int currentCount = 0;
        Leaderboard.GetPlayerEntry("PlaytestBoard", data =>
        {
            if (data != null)
            {
                currentCount = data.score;
            }
        });

        Leaderboard.SetScore("PlaytestBoard", count + currentCount);
    }

    private void OnEnable()
    {
        GetAllPlayerResults();
        GetPlayerResult();
    }

    private void OnDisable()
    {
        _leaderBoardUI.Reset();
    }

    private void GetAllPlayerResults()
    {
        Leaderboard.GetEntries("PlaytestBoard", (result) =>
        {
            foreach (var entry in result.entries)
            {
                string name = entry.player.publicName;
                if (string.IsNullOrEmpty(name))
                    name = "Anonymous";

                Debug.Log(name + " " + entry.score);

                _leaderBoardUI.AddPanel(entry.rank, name, entry.score);
            }
        });
    }

    private void GetPlayerResult()
    {
        Leaderboard.GetPlayerEntry("PlaytestBoard", (result) =>
        {
            if (result == null)
            {
                _leaderBoardUI.SetPlayerPanel(0, "Unknown", 0);
            }
            else
            {
                _leaderBoardUI.SetPlayerPanel(result.rank, result.player.publicName, result.score);
            }
        });
    }
}