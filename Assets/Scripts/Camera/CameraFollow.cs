using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    private Vector3 offset;

    void Awake()
    {
        player = GameObject.Find("Player").transform;
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        if (!player) return;
        transform.position = player.position + offset;
        // transform.position = new Vector3(transform.position.x, transform.position.y, 0); 
    }
}