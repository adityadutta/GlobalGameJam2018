using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [System.Serializable]
    public class EnemyStats
    {
        public bool isPurified = false;
        public int maxHealth = 100;

        private int _curHealth;
        public int curHealth
        {
            get { return _curHealth; }
            set { _curHealth = Mathf.Clamp(value, 0, maxHealth); }
        }

        public int damage = 40;
        public bool canPurify = false;
        public bool isFlying = false;
        public bool isHeavy = false;
        public bool isTombstone = false;
        public bool isBoss = false;

        public void Init()
        {
            curHealth = maxHealth;
        }
    }

    public EnemyStats enemyStats = new EnemyStats();

    public Transform deathParticles;
    //public float shakeAmount = 0.1f;
    //public float shakeLength = 0.1f;

    [Header("Optional:")]
    [SerializeField]
    private StatusIndicator statusIndicator;

    GameObject[] _enemies;
    public bool isBossActive = false;

    public GameObject gameOver;

    public string deathSoundName;

    private AudioManager audioManager;

    private void Start()
    {
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.Log("No audio manager found");
        }

        enemyStats.Init();

        if(statusIndicator != null)
        {
            statusIndicator.SetHealth(enemyStats.curHealth, enemyStats.maxHealth);
        }

        if(deathParticles == null)
        {
            Debug.Log("no particles");
        }

        GameObject[] _enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in _enemies)
        {
            Physics2D.IgnoreCollision(enemy.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

        gameOver.SetActive(false);

    }
    
    private void Update()
    {
        if (enemyStats.isPurified)
        {
            GetComponent<Animator>().SetBool("isPurified", true);
            Destroy(this.gameObject, 6f);
        }

        if (enemyStats.isBoss && enemyStats.curHealth <= 0)
        {
            gameOver.SetActive(true);
        }
    }

    public void DamageEnemy(int damage)
    {
        enemyStats.curHealth -= damage;
        if (enemyStats.curHealth <= 0 && !enemyStats.isBoss)
        {
            GameManager.KillEnemy(this);
            audioManager.PlaySound(deathSoundName);
        }

        if (statusIndicator != null)
        {
            statusIndicator.SetHealth(enemyStats.curHealth, enemyStats.maxHealth);
        }
    }

    private void OnCollisionEnter2D(Collision2D _colInfo)
    {
        Player _player = _colInfo.collider.GetComponent<Player>();
        if (_player != null)
        {
            if (enemyStats.isPurified)
            {
                Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>(), GetComponent<Collider2D>());
                Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("Weapon").GetComponent<Collider2D>(), GetComponent<Collider2D>());
            }

            if (enemyStats.isFlying)
            {
                _player.DamagePlayer(enemyStats.damage);
                DamageEnemy(99999);
            }
            else
            {
                _player.DamagePlayer(enemyStats.damage);
                Debug.Log("Player damaged for " + enemyStats.damage);
            }
        }
    }
}
