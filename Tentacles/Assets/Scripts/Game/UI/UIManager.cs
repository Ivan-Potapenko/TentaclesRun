using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Events;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private EventListener _playerDeadEventListner;

    [SerializeField]
    private LooseGroup _looseGroup;

    private void OnEnable() {
        _playerDeadEventListner.OnEventHappened += EnableLose;
    }

    private void OnDisable() {
        _playerDeadEventListner.OnEventHappened -= EnableLose;
    }

    public static void LoadGameplayScene() {
        SceneManager.LoadScene("GameplayScene");
    }

    public static void LoadInfoScene() {
        SceneManager.LoadScene("InfoScene");
    }

    public static void LoadMenuScene() {
        SceneManager.LoadScene("MenuScene");
    }

    private void EnableLose() {
        _looseGroup.SetActive(true);
    }
}
