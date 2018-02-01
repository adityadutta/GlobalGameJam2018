using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets._2D;

public class PlayerUIController : MonoBehaviour {

    [SerializeField]
    private RectTransform healthBarRect;

    [SerializeField]
    private RectTransform manaBarRect;

    [SerializeField]
    private Platformer2DUserControl playerControls;

    [SerializeField]
    private EnemyAI enemyMovement;

    public GameObject pauseMenu;
    public GameObject gameOver;

    private void Start()
    {
        pauseMenu.SetActive(false);
        gameOver.SetActive(false);

        playerControls = GetComponent<Platformer2DUserControl>();

        enemyMovement = GetComponent<EnemyAI>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(true);
            playerControls.enabled = false;
            enemyMovement.enabled = false;
        }
    }

    public void SetHealth(int _cur, int _max)
    {
        float _value = (float)_cur / _max;

        healthBarRect.localScale = new Vector3(_value, healthBarRect.localScale.y, healthBarRect.localScale.z);

    }

    public void SetMana(int _cur, int _max)
    {
        float _value = (float)_cur / _max;

        manaBarRect.localScale = new Vector3(_value, manaBarRect.localScale.y, manaBarRect.localScale.z);

    }

    public void OnResume()
    {
        pauseMenu.SetActive(false);
        playerControls.enabled = true;
        enemyMovement.enabled = true;
    }
}
