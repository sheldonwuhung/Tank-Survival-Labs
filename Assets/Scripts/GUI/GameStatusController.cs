using TMPro;
using UnityEngine;

public class GameStatusController : MonoBehaviour
{
    public GameObject gameStatusPanel;
    public TextMeshProUGUI topText;
    public TextMeshProUGUI bottomText;

    void Awake()
    {
        gameStatusPanel = gameObject;
        topText = gameObject.transform.Find("TopText").GetChild(0).GetComponent<TextMeshProUGUI>();
        bottomText = gameObject.transform.Find("BottomText").GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    public void Active(bool statement)
    {
        gameStatusPanel.SetActive(statement);
    }
    
    public void LoseLevel()
    {
        topText.text = "You Lost!";
        bottomText.text = "Restarting Wave...";
    }
    
    public void NextLevel()
    {
        topText.text = "You Survived!";
        bottomText.text = "Starting Next Wave...";
    }
    
    public void Won()
    {
        topText.text = "You Won!";
        bottomText.text = "Restarting Game...";
    }
}
