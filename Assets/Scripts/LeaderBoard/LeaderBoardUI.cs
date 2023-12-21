using UnityEngine;

public class LeaderBoardUI : MonoBehaviour
{
    [SerializeField] private LeaderBoardPanelUI _prefabPanel;
    [SerializeField] private LeaderBoardPanelUI _playerPanel;
    [SerializeField] private LeaderBoard _leaderBoard;
    [SerializeField] private RectTransform _contentPanel;

    public void AddPanel(int rank, string name, int score)
    {
        LeaderBoardPanelUI panelUI = Instantiate(_prefabPanel, _contentPanel.position, Quaternion.identity, _contentPanel);
        panelUI.Set(rank, name, score);
    }

    public void SetPlayerPanel(int rank, string name, int score)
    {
        _playerPanel.Set(rank, name, score);
    }

    public void Reset()
    {
        foreach (Transform child in _contentPanel)
        {
            Destroy(child.gameObject);
        }
    }
}