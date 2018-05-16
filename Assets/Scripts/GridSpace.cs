using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSpace : MonoBehaviour {

    public Button button;
    public Text buttonText; 
    
    private GameController _gameController;

    public void SetGameControllerReference(GameController controller)
    {
        _gameController = controller;
    }

    public void SetSpace()
    {
        buttonText.text = _gameController.GetPlayerSide();
        button.interactable = false;
        _gameController.EndTurn();
    }
}
