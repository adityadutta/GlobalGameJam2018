using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour {

    [SerializeField]
    private RectTransform healthBarRect;

    [SerializeField]
    private RectTransform manaBarRect;

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
}
