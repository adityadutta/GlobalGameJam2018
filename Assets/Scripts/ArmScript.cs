using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmScript : MonoBehaviour {

    Animator anim;
    public Animator zombieAnim;

    public Sprite purifiedHuman;

    [SerializeField]
    private PlayerUIController playerStatusIndicator;

    Player _player;

    private void Start()
    {
        _player = GetComponentInParent<Player>();

        anim = GetComponentInParent<Animator>();
        playerStatusIndicator.SetMana(_player.stats.curMana, _player.stats.maxMana);
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
        if (_colInfo.tag == "Enemy" )
        {
            Enemy _enemy = _colInfo.GetComponent<Enemy>();
            if (Input.GetKey(KeyCode.Z))
            {
                if (_enemy != null)
                {
                    if (_enemy.enemyStats.canPurify == true && _player.stats.curMana > 10)
                    {
                        _enemy.enemyStats.damage = 0;
                        _enemy.GetComponentInChildren<SpriteRenderer>().color = Color.white;
                        Physics2D.IgnoreCollision(_enemy.GetComponent<Collider2D>(), _player.GetComponent<Collider2D>());
                        Physics2D.IgnoreCollision(_enemy.GetComponent<Collider2D>(), GetComponent<Collider2D>());
                        _enemy.GetComponentInChildren<SpriteRenderer>().sprite = purifiedHuman;
                        zombieAnim.SetBool("isPurified", true);
                        _player.stats.curMana -= 10;
                        playerStatusIndicator.SetMana(_player.stats.curMana, _player.stats.maxMana);
                    }
                }
            }
            else
            {
                if (_enemy.enemyStats.damage != 0)
                    _enemy.DamageEnemy(_player.stats.damage);
            }
        }
    }
}
