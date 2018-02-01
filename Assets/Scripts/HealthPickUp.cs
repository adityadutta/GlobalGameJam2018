using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour {

    [SerializeField]
    private PlayerUIController playerStatusIndicator;

    private AudioManager audioManager;
    public string healthPotion;

    private void Start()
    {
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.Log("No audio manager found");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player _player = collision.collider.GetComponent<Player>();
        if (_player != null)
        {
            _player.stats.curHealth += 20;
            playerStatusIndicator.SetHealth(_player.stats.curHealth, _player.stats.maxHealth);
            audioManager.PlaySound(healthPotion);
            Destroy(this.gameObject);
        }
    }
}
