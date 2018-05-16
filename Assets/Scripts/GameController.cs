using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Player
{
    public Image panel;
    public Text text;
}

[System.Serializable]
public class PlayerColor
{
    public Color panelColor;
    public Color textColor;
}

public class GameController : MonoBehaviour
{

    public Text[] buttonList;
    private string _playerSide;
    public GameObject gameOverPanel;
    public Text gameOverText;
    private int _moveCount;
    public GameObject restartButton;

    public Player playerX;
    public Player playerO;
    public PlayerColor activePlayerColor;
    public PlayerColor inactivePlayerColor;

    private void SetGameControllerReferenceOnButtons()
    {
        foreach (var button in buttonList)
        {
            button.GetComponentInParent< GridSpace>().SetGameControllerReference(this);
        }
    }

    public void Awake()
    {
        SetPlayersColors(playerX, playerO);
        restartButton.SetActive(false);
        SetGameControllerReferenceOnButtons();
        _playerSide = "X";
        _moveCount = 0;
        gameOverPanel.SetActive(false);
        
    }

    public string GetPlayerSide()
    {
        return _playerSide;
    }

    public void RestartGame()
    {
        _playerSide = "X";
        SetPlayersColors(playerX, playerO);
        _moveCount = 0;
        gameOverPanel.SetActive(false);
        SetBoardInteractable(true);
        restartButton.SetActive(false);
            
    }

    private void SetPlayersColors(Player newPlayer, Player oldPlayer)
    {
        newPlayer.panel.color = activePlayerColor.panelColor;
        newPlayer.text.color = activePlayerColor.textColor;
        oldPlayer.panel.color = inactivePlayerColor.panelColor;
        oldPlayer.text.color = inactivePlayerColor.textColor;
    }

    private void SetBoardInteractable(bool toggle)
    {
        foreach (var button in buttonList)
        {
            button.GetComponentInParent<Button>().interactable = toggle;
            button.text = "";
        }
    }

    public void EndTurn()
    {
        _moveCount++;
        if ((buttonList[0].text == _playerSide && buttonList[1].text == _playerSide && buttonList[2].text == _playerSide) ||
            (buttonList[3].text == _playerSide && buttonList[4].text == _playerSide && buttonList[5].text == _playerSide) ||
            (buttonList[6].text == _playerSide && buttonList[7].text == _playerSide && buttonList[8].text == _playerSide) ||
            (buttonList[0].text == _playerSide && buttonList[3].text == _playerSide && buttonList[6].text == _playerSide) ||
            (buttonList[1].text == _playerSide && buttonList[4].text == _playerSide && buttonList[7].text == _playerSide) ||
            (buttonList[2].text == _playerSide && buttonList[5].text == _playerSide && buttonList[8].text == _playerSide) ||
            (buttonList[0].text == _playerSide && buttonList[4].text == _playerSide && buttonList[8].text == _playerSide) ||
            (buttonList[2].text == _playerSide && buttonList[4].text == _playerSide && buttonList[6].text == _playerSide))
        {
            GameOver(_playerSide);
        }
        
        else if (_moveCount > 8)
        {
           GameOver("draw");
        }
        else
            ChangeSides();
    }

    private void SetGameOverText(string value)
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = value;
    }

    private void GameOver(string winningPlayer)
    {
        SetBoardInteractable(false);
        SetGameOverText(winningPlayer == "draw" ? "It's a draw!" : string.Format("{0} Wins!", winningPlayer));
        restartButton.SetActive(true);
    }

    private void ChangeSides()
    {
        _playerSide = (_playerSide == "X") ? "O" : "X";
        if (_playerSide == "X")
        {
            SetPlayersColors(playerX, playerO);
        }
        else
        {
            SetPlayersColors(playerO, playerX);
        }
        
    }
}

