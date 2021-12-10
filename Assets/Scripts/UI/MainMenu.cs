using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject Menu;
    [SerializeField] GameObject CreditsMenu;
    [SerializeField] GameObject InstructionsMenu;
    [SerializeField] GameObject Title;

    private void Start()
    {
        Menu.SetActive(true);
        CreditsMenu.SetActive(false);
        InstructionsMenu.SetActive(false);
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void GoToCredits()
    {
        CreditsMenu.SetActive(true);
        Menu.SetActive(false);
        InstructionsMenu.SetActive(false);
    }
    
    public void GoToInstructions()
    {
        Menu.SetActive(false);
        CreditsMenu.SetActive(false);
        InstructionsMenu.SetActive(true);
        Title.SetActive(false);
    }

    public void ReturnToMenu()
    {
        Menu.SetActive(true);
        CreditsMenu.SetActive(false);
        InstructionsMenu.SetActive(false);
        Title.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
