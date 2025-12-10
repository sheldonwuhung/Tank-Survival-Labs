using UnityEngine;
using System.Collections;

public class ProjectileMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public float moveSpeed = 5f;
    public int damage;
    public string shooterTag;

    public Vector3 direction;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        StartCoroutine(Delay());
    }

    void FixedUpdate()
    {
        transform.Translate(Time.fixedDeltaTime * moveSpeed * direction);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        string otherTag = other.gameObject.tag;
        if ((shooterTag == "Player" && otherTag == "Player") || (shooterTag == "NPC" && otherTag == "NPC") || other.gameObject.CompareTag("Projectile")) return;

        var otherController = (NPCController)null;
        
        if (shooterTag == "Player" && otherTag == "NPC") otherController = other.gameObject.GetComponent<NPCController>();
        else if (shooterTag == "NPC" && otherTag == "Player") otherController = other.gameObject.GetComponent<PlayerController>();

        if (otherController) otherController.health -= damage;
        
        if (gameObject) Destroy(gameObject);
        
    }

    IEnumerator Delay(){
        for (int i = 0; i < 50; i++)
        {
            if (!gameObject) yield break;
            yield return new WaitForSeconds(0.1f);
        }
    
        if (gameObject) Destroy(gameObject);
    }

}