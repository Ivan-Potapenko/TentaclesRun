using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private Button _playButton;

    [SerializeField]
    private Button _exitButton;

    [SerializeField]
    private Button _infoButton;

  /*  private void Start() {
        _playButton.onClick.AddListener(LoadGameplayScene);
        _exitButton.onClick.AddListener(Exit);
        _infoButton.onClick.AddListener(LoadInfoScene);
    }*/



    private void Exit() {
        Application.Quit();
    }
}
