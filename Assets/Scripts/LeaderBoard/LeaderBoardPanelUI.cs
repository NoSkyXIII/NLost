using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LeaderBoardPanelUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _rankText;
    [SerializeField] private Image _scoreBackground;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _name;

    public void Set(int rank, string name, int score)
    {
        _rankText.text = rank.ToString();
        _name.text = name;
        _scoreText.text = score.ToString();
        SetColor(rank);
    }

    private void SetColor(int rank)
    {
        string hexColor;

        if (rank == 1)
        {
            hexColor = "#ffd700";
        }
        else if (rank == 2)
        {
            hexColor = "#c0c0c0";
        }
        else if (rank == 3)
        {
            hexColor = "#B87333";
        }
        else
        {
            hexColor = "#ffffff";
        }

        _scoreBackground.color = HexToColor(hexColor);
    }

    private Color HexToColor(string hex)
    {
        Color color = new Color();
        ColorUtility.TryParseHtmlString(hex, out color);
        return color;
    }
}