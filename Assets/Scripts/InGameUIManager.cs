using UnityEngine;

public class InGameUIManager : MonoBehaviour
{
    [SerializeField] CanvasGroup _gameOverPanelCG;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void ShowInGameUI()
    {
        _canvasGroup.alpha = 1.0f;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
    }

    public void ShowGameOverPanel()
    {
        _gameOverPanelCG.alpha = 1;
        _gameOverPanelCG.interactable = true;
        _gameOverPanelCG.blocksRaycasts = true;
    }

    public void ReturnToMainMenu()
    {
        GameManager.Instance.ResetGame();
    }
}
