using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gameWonPanel;
    [SerializeField] GameObject gameLosePanel;
    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject resumeButton;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject jumpButton;
    [SerializeField] GameObject joystick;

    AudioSource audioSource;
    public static GameManager instance;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        pauseButton.SetActive(true);
        jumpButton.SetActive(true);
        joystick.SetActive(true);

        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MainMenuMenthod()
    {
        SceneManager.LoadScene("MM");
    }

    public void PauseButtonMenthod()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
        resumeButton.SetActive(true);
        pauseButton.SetActive(false);
        jumpButton.SetActive(false);
        joystick.SetActive(false);
        audioSource.Stop();
    }

    public void ResumeButton()
    {
        pauseButton.SetActive(true);
        jumpButton.SetActive(true);
        joystick.SetActive(true);
        pausePanel.SetActive(false);
        audioSource.Play();
        Time.timeScale = 1.0f;
    }

    public void GameWonMethod()
    {
        gameWonPanel.SetActive(true);
        pauseButton.SetActive(false);
        jumpButton.SetActive(false);
        joystick.SetActive(false);
    }

    public void GameLoseMethod()
    {
        gameLosePanel.SetActive(true);
        pauseButton.SetActive(false);
        jumpButton.SetActive(false);
        joystick.SetActive(false);
    }

    public void playAgain()
    {
        SceneManager.LoadScene("Laser");
    }

    public void JumpButton()
    {
        PlayerController.instance.Jump();
    }
}
