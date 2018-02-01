using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmScript : MonoBehaviour
{

    Animator anim;
    public Animator zombieAnim;

    public Sprite purifiedHuman;

    [SerializeField]
    private PlayerUIController playerStatusIndicator;

    Player _player;

    private AudioManager audioManager;
    public string hitSound;

    private void Start()
    {
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.Log("No audio manager found");
        }

        _player = GetComponentInParent<Player>();

        anim = GetComponentInParent<Animator>();
        // playerStatusIndicator.SetMana(_player.stats.curMana, _player.stats.maxMana);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("swordAttack");
        }
    }

    private void OnTriggerEnter2D(Collider2D _colInfo)
    {
        Enemy _enemy = _colInfo.GetComponent<Enemy>();
        if (_enemy != null)
        {
            if (Input.GetKey(KeyCode.Z))
            {

                if (_enemy.enemyStats.canPurify == true && _player.stats.curMana > 10)
                {
                    _enemy.enemyStats.damage = 0;
                    _enemy.GetComponentInChildren<SpriteRenderer>().sprite = purifiedHuman;
                    zombieAnim.SetBool("isPurified", true);
                    _enemy.enemyStats.isPurified = true;
                    _enemy.GetComponent<EnemyAI>().enabled = false;
                    _player.stats.curMana -= 10;
                    playerStatusIndicator.SetMana(_player.stats.curMana, _player.stats.maxMana);
                }

            }
            else if (_enemy.enemyStats.isTombstone)
            {
                _enemy.DamageEnemy(_player.stats.damage);
            }
            else
            {
                if (_enemy.enemyStats.damage != 0)
                    _enemy.DamageEnemy(_player.stats.damage);
            }
            audioManager.PlaySound(hitSound);
        }

    }
}
