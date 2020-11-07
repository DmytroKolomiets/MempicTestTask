using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoseHandler : MonoBehaviour
{
    [SerializeField] private TowerController towerController;
    [SerializeField] private Button restartButton;
    [SerializeField] private GameObject loseWindow;
    [SerializeField] private CameraMovement cameraMovement;

    private void Start()
    {
        restartButton.onClick.AddListener(Restart);
        towerController.SubscribeOnLose(Lose, AllowRestart);
    }
    private void Lose() 
    {
        loseWindow.gameObject.SetActive(true);        
    }
    private void AllowRestart()
    {
        restartButton.gameObject.SetActive(true);
        cameraMovement.ShowTower();
    }
    private void Restart() 
    {
        cameraMovement.ToStartPosition();
        towerController.Restart();
        restartButton.gameObject.SetActive(false);
        loseWindow.gameObject.SetActive(false);
    }
}
