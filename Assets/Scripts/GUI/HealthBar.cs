using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Transform target;
    public GameObject border;
    public Slider slider;
    public Vector3 offset = new Vector3(0, -10, 0);

    PlayerController playerController;
    NPCController npcController;

    void Awake()
    {
        border = transform.Find("Border").gameObject;
        slider = border.GetComponent<Slider>();
    }

    public void SetController(Transform t)
    {
        target = t;
        if (target.gameObject.CompareTag("Player")) playerController = target.GetComponent<PlayerController>();
        else if (target.gameObject.CompareTag("NPC")) npcController = target.GetComponent<NPCController>();
    }
    void Update()
    {
        if (!target) return;

        if (playerController) slider.value = (float)playerController.health / playerController.maxHealth;
        else if (npcController) slider.value = (float)npcController.health / npcController.maxHealth;

        transform.position = target.position + offset;
    }
}
