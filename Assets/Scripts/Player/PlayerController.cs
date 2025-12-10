using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class PlayerController : NPCController
{
    public General generalScript;
    public GameStatusController gameStatusController;
    public PlayerInfoController playerInfoController;
    public PlayerMovement playerMovement;
    
    public Transform firePoint2;
    [SerializeField] public GameObject firePrefab2;
    
    public AudioSource fire1AudioSource;
    public AudioSource fire2AudioSource;

    public int damage2 = 5;
    public float fireRate2 = .1f;

    public Transform currentFirePoint;
    public GameObject currentFirePrefab;
    public AudioSource currentFireAudioSource;
    public int currentDamage;
    public float currentFireRate;

    private float clickCooldown = .1f;
    private bool isClicking = false;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        tr = GetComponent<Transform>();
        playerMovement = GetComponent<PlayerMovement>();
        gameStatusController = GameObject.Find("GameStatusContainer").transform.GetChild(0).GetComponent<GameStatusController>();
        playerInfoController = GameObject.Find("PlayerInfo").GetComponent<PlayerInfoController>();
        generalScript = GameObject.Find("Managers").GetComponent<General>();
        
        topGunTR = tr.Find("TopGun").transform;
        firePoint1 = topGunTR.transform.Find("FirePoint1").transform;
        firePoint2 = topGunTR.transform.Find("FirePoint2").transform;
        
        AudioSource[] audioSources = GetComponents<AudioSource>();
        fire1AudioSource = audioSources[0];
        fire2AudioSource = audioSources[1];
        explosionAudioSource = audioSources[2];
        
        currentFirePoint = firePoint1;
        currentFirePrefab = firePrefab1;
        currentFireAudioSource = fire1AudioSource;
        currentFireRate = fireRate1;
        currentDamage = damage1;
        shooterTag = gameObject.tag;
        
        playerTank = gameObject;
        
        GameObject Managers = GameObject.Find("Managers");
        GameObject healthBarReference = Managers.transform.Find("HealthBar").gameObject;
        healthBar = Instantiate(healthBarReference, playerTank.transform.position, Quaternion.identity);
        healthBar.GetComponent<HealthBar>().SetController(tr);
        healthBar.name = gameObject.name + " HealthBar";
        gameManager = Managers.GetComponent<GameManager>();
    }

    void Update()
    {
        playerMovement.Movement(tr, topGunTR);
    }

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        playerMovement.MoveTo(rb);
        if (health <= 0 && !dying)
        {
            Death();
        }
    }
    
    public new void Death()
    {
        dying = true;
        Destroy(healthBar);
        explosionAudioSource.Play();
        
        topGunTR.gameObject.GetComponent<SpriteRenderer>().DOFade(0, explosionAudioSource.clip.length);
        sr.DOFade(0, explosionAudioSource.clip.length);
        
        gameStatusController.gameObject.SetActive(true);
        gameStatusController.LoseLevel();
        gameManager.RestartLevelWithDelay(4f);
    }
    
    public void OnFire(InputAction.CallbackContext context)
    {
        if (isClicking) return;
        isClicking = true;
        FireItem(Camera.main.ScreenToWorldPoint(Input.mousePosition), currentFirePoint, currentFirePrefab, currentFireAudioSource, currentFireRate, currentDamage, shooterTag);
        StartCoroutine(ResetClickCooldown());
    }

    public void OnSwitchWeapon(InputAction.CallbackContext context)
    {
        InputControl triggeredControl = context.control;
        int number = int.Parse(triggeredControl.name);

        if (number == 1)
        {
            currentFirePoint = firePoint1;
            currentFirePrefab = firePrefab1;
            currentFireAudioSource = fire1AudioSource;
            currentFireRate = fireRate1;
            currentDamage = damage1;
        }
        else if (number == 2)
        {
            currentFirePoint = firePoint2;
            currentFirePrefab = firePrefab2;
            currentFireAudioSource = fire2AudioSource;
            currentFireRate = fireRate2;
            currentDamage = damage2;
        }

        playerInfoController.SwitchWeapon(number);
    }

    IEnumerator ResetClickCooldown()
    {
        yield return new WaitForSeconds(clickCooldown);
        isClicking = false;
    }

}
