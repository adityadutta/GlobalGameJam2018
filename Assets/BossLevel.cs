using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLevel : MonoBehaviour {

    public GameObject boss;

    private void Start()
    {
        boss.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        boss.SetActive(true);
    }
}
