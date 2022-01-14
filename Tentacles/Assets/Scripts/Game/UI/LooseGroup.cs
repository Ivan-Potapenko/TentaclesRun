using UnityEngine;
using UnityEngine.UI;

public class LooseGroup : MonoBehaviour
{
    [SerializeField]
    private Button _menuButton;

    [SerializeField]
    private Button _restartButton;

    private void Awake() {
        _menuButton.onClick.AddListener(UIManager.LoadMenuScene);
        _restartButton.onClick.AddListener(UIManager.LoadGameplayScene);
    }

    public void SetActive(bool enable) {
        gameObject.SetActive(enable);
    }

}
