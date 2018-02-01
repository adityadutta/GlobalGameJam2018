using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPickUp : MonoBehaviour {

    [SerializeField]
    private PlayerUIController playerStatusIndicator;

    private AudioManager audioManager;
    public string manaPotion;

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
            _player.stats.curMana += 20;
            playerStatusIndicator.SetMana(_player.stats.curMana, _player.stats.maxMana);
            audioManager.PlaySound(manaPotion);
            Destroy(this.gameObject);
        }
    }
}
