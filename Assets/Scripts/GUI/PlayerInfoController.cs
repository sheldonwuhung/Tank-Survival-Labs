using TMPro;
using UnityEngine;

public class PlayerInfoController : MonoBehaviour
{
    public GameObject playerInfoCanvas;
    public GameObject weaponInfoPanelContainer;
    public GameObject weaponInfoPanel;

    public GameObject cannonPanel;
    public GameObject machineGunPanel;
    
    public GameObject cannonTextObject;
    public GameObject cannonNumObject;
    public GameObject machineGunTextObject;
    public GameObject machineGunNumObject;
    
    public TextMeshProUGUI cannonText;
    public TextMeshProUGUI cannonNum;
    public TextMeshProUGUI machineGunText;
    public TextMeshProUGUI machineGunNum;
  
    void Awake()
    {
        playerInfoCanvas = gameObject;
        weaponInfoPanelContainer = playerInfoCanvas.transform.Find("WeaponInfoContainer").gameObject;
        weaponInfoPanel = weaponInfoPanelContainer.transform.Find("WeaponInfo").gameObject;
        
        cannonPanel = weaponInfoPanel.transform.Find("Cannon").gameObject;
        machineGunPanel = weaponInfoPanel.transform.Find("MachineGun").gameObject;
        
        cannonTextObject = cannonPanel.transform.Find("Text").gameObject;
        cannonNumObject = cannonPanel.transform.Find("Number").gameObject;
        
        machineGunTextObject = machineGunPanel.transform.Find("Text").gameObject;
        machineGunNumObject = machineGunPanel.transform.Find("Number").gameObject;
        
        cannonText = cannonTextObject.GetComponent<TextMeshProUGUI>();
        cannonNum = cannonNumObject.GetComponent<TextMeshProUGUI>();
        machineGunText = machineGunTextObject.GetComponent<TextMeshProUGUI>();
        machineGunNum = machineGunNumObject.GetComponent<TextMeshProUGUI>();
        
        SwitchWeapon(1);
    }

    public void SwitchWeapon(int num)
    {
        if (num == 1)
        {
            cannonText.color = Color.yellow;
            cannonNum.color = Color.yellow;
            machineGunText.color = Color.white;
            machineGunNum.color = Color.white;
        }
        else if (num == 2)
        {
            cannonText.color = Color.white;
            cannonNum.color = Color.white;
            machineGunText.color = Color.yellow;
            machineGunNum.color = Color.yellow;
        }
    }
    
}
