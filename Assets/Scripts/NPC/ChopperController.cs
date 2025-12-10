using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChopperController : NPCController
{
    public Transform rotors;
    void Awake()
    {
        enemyContainer = GameObject.Find("EnemyContainer");
        
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        tr = GetComponent<Transform>();
        npcMovement = GetComponent<NPCMovement>();
        
        rotors = tr.Find("Rotors").transform;

        if (tr.Find("TopGun"))
        {
            topGunTR = tr.Find("TopGun").transform;
            firePoint1 = GameObject.Find("TopGun").transform.Find("FirePoint1").transform;
        }
        else
        {
            firePoint1 = transform.Find("FirePoint1").transform;
        }

        playerTank = GameObject.Find("Player");
        
        AudioSource[] audioSources = GetComponents<AudioSource>();
        fire1AudioSource = audioSources[0];
        explosionAudioSource = audioSources[1];
        shooterTag = gameObject.tag;
        
        GameObject Managers = GameObject.Find("Managers");
        GameObject healthBarReference = Managers.transform.Find("HealthBar").gameObject;
        healthBar = Instantiate(healthBarReference, playerTank.transform.position, Quaternion.identity);
        healthBar.GetComponent<HealthBar>().SetController(tr);
        healthBar.name = gameObject.name + " HealthBar";
        gameManager = Managers.GetComponent<GameManager>();
        gameStatusController = GameObject.Find("GameStatusContainer").transform.GetChild(0).GetComponent<GameStatusController>();
    }

    void Start()
    {
        if (gameObject.CompareTag("Player")) return;
        StartCoroutine(FireLoop());
    }
    void Update()
    {
        if (!playerTank || dying) return;
        Vector3 playerPos = playerTank.transform.position;
        RotorAnimation(.1f);
        npcMovement.Movement(tr, playerPos);
    }

    void FixedUpdate()
    {
        if (!playerTank || dying) return;
        npcMovement.MoveTo(rb);
        
        if (health <= 0 && !dying) Death();
    }
    
    void RotorAnimation(float time)
    {
        rotors.Rotate(Vector3.forward * (Time.deltaTime * 520f));
    }
}
