using System;
using System.Collections;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCController : MonoBehaviour
{
    public GameManager gameManager;
    public DataManager dataManager;
    public GameStatusController gameStatusController;
    public GameObject enemyContainer;
    
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public Transform tr;
    public NPCMovement npcMovement;
    
    public Transform topGunTR;
    
    [SerializeField] public int health = 100;
    [SerializeField] public int maxHealth = 100;
    [SerializeField] public int damage1 = 25;
    [SerializeField] public int score = 50;
    public bool fireCooldown = false;
    [SerializeField] public float fireRate1 = 5f;
    
    public AudioSource fire1AudioSource;
    public AudioSource explosionAudioSource;

    public Transform firePoint1;
    [SerializeField] public GameObject firePrefab1;
    [SerializeField] public GameObject repairBoxPrefab;

    public string shooterTag;

    public GameObject playerTank;
    public float spotDistance = 150f;
    
    public GameObject healthBar;

    public bool dying = false;

    void Awake()
    {
        enemyContainer = GameObject.Find("EnemyContainer");
        
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        tr = GetComponent<Transform>();
        npcMovement = GetComponent<NPCMovement>();

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
        dataManager = GameObject.Find("Title").GetComponent<DataManager>();
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
        npcMovement.Movement(tr, playerPos);
    }

    void FixedUpdate()
    {
        if (!playerTank || dying) return;
        npcMovement.MoveTo(rb);
        
        if (health <= 0 && !dying) Death();
    }
    
    public void FireItem(Vector3 target, Transform point, GameObject prefab, AudioSource sound, float fireRate, int damage, string shooterTag)
    {
        if (fireCooldown) return;
        fireCooldown = true;

        StartCoroutine(FireItemIENum(target, point, prefab, sound, fireRate, damage, shooterTag));
    }

    public void Death()
    {
        dying = true;
        Destroy(healthBar);
        Instantiate(repairBoxPrefab, tr.position, Quaternion.identity);
        explosionAudioSource.Play();
        
        if (shooterTag == "NPC" && gameManager) gameManager.AddScore(score);
        
        sr.DOFade(0, .1f);
        if (topGunTR) topGunTR.gameObject.GetComponent<SpriteRenderer>().DOFade(0, .1f);
        
        if (enemyContainer.transform.childCount <= 1)
        {
            gameStatusController.gameObject.SetActive(true);

            if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1)
            {
                gameStatusController.Won();
                dataManager.CheckAndSetHighScore(gameManager.score);
            }
            else gameStatusController.NextLevel();
            
            gameManager.LoadNextLevelWithDelay(4f);
            Destroy(gameObject);
        }
        else {Destroy(gameObject, .1f);}
    }

    public IEnumerator FireLoop()
    {
        while (!dying)
        {
            if (Vector3.Distance(playerTank.transform.position, tr.position) < spotDistance)
            {
                Vector3 randomOffset = new Vector3(UnityEngine.Random.Range(-20f, 20f), UnityEngine.Random.Range(-20f, 20f), 0);
                Vector3 playerPos = playerTank.transform.position + randomOffset;
                FireItem(playerPos, firePoint1, firePrefab1, fire1AudioSource, fireRate1, damage1, shooterTag);
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
    
    public IEnumerator FireItemIENum(Vector3 target, Transform point, GameObject prefab, AudioSource sound, float fireRate, int damage, string shooterTag)
    {
        sound.Play();
        GameObject itemClone = Instantiate(prefab, point.transform.position, Quaternion.identity);
        itemClone.transform.GetChild(0).transform.DORotate(new Vector3(0,0,point.rotation.eulerAngles.z), 0f);

        if (itemClone)
        {
            ProjectileMovement itemScript = itemClone.GetComponent<ProjectileMovement>();
            if (itemScript)
            {
                itemScript.damage = damage;
                itemScript.shooterTag = shooterTag;
                itemScript.direction = (target - point.position) + itemClone.transform.forward * 100f;
            }
        }

        yield return new WaitForSeconds(fireRate);
        fireCooldown = false;
    }
}