using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    [NonSerialized] private GameObject menuPanel;
    [NonSerialized] private GameObject playButton;
    [NonSerialized] private GameObject instructionsButton;
    [NonSerialized] private GameObject settingsButton;
    [NonSerialized] private GameObject highScoresButton;
 
    [NonSerialized] private GameObject instructionsPanel;
    [NonSerialized] private GameObject instructionsReturnButton;
    
    [NonSerialized] private GameObject settingsPanel;
    [NonSerialized] private GameObject settingsReturnButton;
    
    [NonSerialized] private GameObject highScorePanel;
    [NonSerialized] private GameObject highScoreReturnButton;
    
    [NonSerialized] private GameObject[] interactButtons;
    [NonSerialized] private GameObject[] returnButtons;
    
    private bool paused = false;
    void Awake()
    {
        menuPanel = gameObject.transform.Find("Menu").gameObject;
        playButton = menuPanel.transform.Find("PlayButton").gameObject;
        instructionsButton = menuPanel.transform.Find("InstructionsButton").gameObject;
        settingsButton = menuPanel.transform.Find("SettingsButton").gameObject;
        highScoresButton = menuPanel.transform.Find("HighScoresButton").gameObject;
        
        instructionsPanel = gameObject.transform.Find("Instructions").gameObject;
        instructionsReturnButton = instructionsPanel.transform.Find("ReturnButton").gameObject;
        
        settingsPanel = gameObject.transform.Find("Settings").gameObject;
        settingsReturnButton = settingsPanel.transform.Find("ReturnButton").gameObject;
        
        highScorePanel = gameObject.transform.Find("HighScores").gameObject;
        highScoreReturnButton = highScorePanel.transform.Find("ReturnButton").gameObject;
        
        interactButtons = new GameObject[] { playButton, instructionsButton, settingsButton, highScoresButton, instructionsReturnButton, settingsReturnButton, highScoreReturnButton };
        
        foreach (GameObject interactButton in interactButtons)
        {
            PanelButton pb = interactButton.AddComponent<PanelButton>();  
            pb.SetUpButton(menuPanel, instructionsPanel, settingsPanel, highScorePanel,interactButton);
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0 && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            paused = !paused;
            menuPanel.SetActive(paused);
            instructionsPanel.SetActive(false);
            settingsPanel.SetActive(false);
            
            Time.timeScale = paused ? 0 : 1;
        }
    }
    
}
