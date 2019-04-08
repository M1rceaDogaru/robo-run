using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public AiManager AiManager;
    public Player Player;

    public GameObject MainMenu;
    public GameObject Win;
    public GameObject FailSadness;
    public GameObject FailEnergy;
    public GameObject FailHealth;
    public GameObject GameUi;

    public void StartGame()
    {
        MainMenu.SetActive(false);
        GameUi.SetActive(true);

        Player.IsRunning = true;
        AiManager.IsRunning = true;
    }

    public void Failed()
    {
        Player.IsRunning = false;
        AiManager.IsRunning = false;

        FailHealth.SetActive(true);
    }

    public void CompleteMission()
    {
        Player.IsRunning = false;
        AiManager.IsRunning = false;
        Win.SetActive(true);
    }

    public void Restart()
    {
        Application.LoadLevel(0);
    }
}
