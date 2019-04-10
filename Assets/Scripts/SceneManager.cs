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

    public KeyCode InteractionKey = KeyCode.X;

    private bool _gameEnded;

    void Update()
    {
        if (Input.GetKeyDown(InteractionKey))
        {
            if (MainMenu.activeInHierarchy)
            {
                StartGame();
            }
            else if (_gameEnded)
            {
                Restart();
            }
        }
    }

    public void StartGame()
    {
        MainMenu.SetActive(false);
        GameUi.SetActive(true);

        Player.IsRunning = true;
        AiManager.IsRunning = true;
    }

    public void FailedHealth()
    {
        EndGame();
        FailHealth.SetActive(true);
    }

    public void FailedEnergy()
    {
        EndGame();
        FailEnergy.SetActive(true);
    }

    public void FailedSadness()
    {
        EndGame();
        FailSadness.SetActive(true);
    }

    private void EndGame()
    {
        Player.IsRunning = false;
        AiManager.IsRunning = false;
        _gameEnded = true;
    }

    public void CompleteMission()
    {
        EndGame();
        Win.SetActive(true);
    }

    public void Restart()
    {
        Application.LoadLevel(0);
    }
}
