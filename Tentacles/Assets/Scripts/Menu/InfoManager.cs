using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InfoManager : MonoBehaviour
{
    [SerializeField]
    private Button _menuButton;

    private void Start() {
        _menuButton.onClick.AddListener(LoadMenu);
    }

    private void LoadMenu() {
        SceneManager.LoadScene("MenuScene");
    }
}
