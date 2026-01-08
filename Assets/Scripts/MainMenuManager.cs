using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup _mainMenuButtonsCG;
    [SerializeField] private CanvasGroup _quitConfirmationCG;
    private CanvasGroup _mainMenuCG;

    private void Awake()
    {
        _mainMenuCG = GetComponent<CanvasGroup>();
        OpenMainMenu();
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }

    public void OpenQuitConfirmation()
    {
        CanvasGroupSetState(_mainMenuButtonsCG, false);
        CanvasGroupSetState(_quitConfirmationCG, true);
    }

    public void CloseQuitConfirmation()
    {
        CanvasGroupSetState(_quitConfirmationCG, false);
        CanvasGroupSetState(_mainMenuButtonsCG, true);
    }

    public void OpenMainMenu()
    {
        CanvasGroupSetState(_mainMenuCG, true);
    }

    public void CloseMainMenu()
    {
        CanvasGroupSetState(_mainMenuCG, false);
    }

    private void CanvasGroupSetState(CanvasGroup canvasGroup, bool state)
    {
        canvasGroup.alpha = state ? 1.0f : 0.0f;
        canvasGroup.interactable = state;
        canvasGroup.blocksRaycasts = state;
    }

    public void Play()
    {
        CloseMainMenu();
        GameManager.Instance.StartGame();
    }
}
