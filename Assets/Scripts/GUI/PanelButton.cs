using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelButton : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject instructionsPanel;
    public GameObject settingsPanel;
    public GameObject highScorePanel;
    
    public GameObject panel;
    public GameObject button;
    
    public GameManager gameManager;
    
    public void SetUpButton(GameObject mp, GameObject ip, GameObject sp, GameObject hsp, GameObject ib)
    {
        menuPanel = mp;
        instructionsPanel = ip;
        settingsPanel = sp;
        highScorePanel = hsp;
        button = ib;
        
        panel = button.transform.parent.gameObject;
        
        Button btn = button.GetComponent<Button>();
        btn.onClick.AddListener(Interaction);
        
        gameManager = GameObject.Find("Managers").GetComponent<GameManager>();
    }
    
    void Interaction()
    {
        if (button.name == "PlayButton")
        {
            if (SceneManager.GetActiveScene().buildIndex == 0) gameManager.LoadNextLevel();
            if (Time.timeScale == 0) Time.timeScale = 1;
            menuPanel.SetActive(false);
        }
        else if (button.name == "ReturnButton") ReturnToMenu();
        else if (button.name == "InstructionsButton") OpenInstructions();
        else if (button.name == "SettingsButton") OpenSettings();
        else if (button.name == "HighScoresButton") OpenHighScore();
    }
    
    void OpenInstructions()
    {
        instructionsPanel.SetActive(true);
        menuPanel.SetActive(false);
    }
    
    void OpenSettings()
    {
        settingsPanel.SetActive(true);
        menuPanel.SetActive(false);
    }
    
    void OpenHighScore()
    {
        highScorePanel.SetActive(true);
        menuPanel.SetActive(false);
    }
    
    void ReturnToMenu()
    {
        menuPanel.SetActive(true);
        panel.SetActive(false);
    }
}
