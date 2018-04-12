﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemObject : MonoBehaviour
{
    [SerializeField] private Image _sprite;
    [SerializeField] private GameObject _glowyThing;
    [SerializeField] private GameObject _eqippyThing;
    [SerializeField] private TextMeshProUGUI _infoBox;

    public Item Item;

    public void Init()
    {
        _sprite.sprite = Item.GetComponent<SpriteRenderer>().sprite;
        _infoBox.text = Item.Description;
        transform.localScale = Vector3.one;
    }

    public void SetGlow(bool glow)
    {
        _glowyThing.SetActive(glow);
    }

    public void SetEquip(bool b)
    {
        _eqippyThing.SetActive(b);
    }
}