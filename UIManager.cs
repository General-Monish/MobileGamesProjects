using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject playButton;
    [SerializeField] GameObject tutorialButton;
    [SerializeField] GameObject quitButton;
    [SerializeField] GameObject tutorailPanel;
    [SerializeField] GameObject backButton;
    [SerializeField] GameObject mainMenuPanel;
    // Start is called before the first frame update
    void Start()
    {
        mainMenuPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButtonMethod()
    {
        SceneManager.LoadScene("Laser");
    }

    public void TutorialButtonMethod()
    {
        tutorailPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void CloseButtonMethod()
    {
        mainMenuPanel.SetActive(true);
        tutorailPanel?.SetActive(false);
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
    }
}
