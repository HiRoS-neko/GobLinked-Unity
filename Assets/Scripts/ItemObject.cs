using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemObject : MonoBehaviour
{
    [SerializeField] private GameObject _eqippyThing;
    [SerializeField] private GameObject _glowyThing;
    [SerializeField] private TextMeshProUGUI _infoBox;
    [SerializeField] private Image _sprite;

    public Item Item;

    public void Init()
    {
        _sprite.sprite = Item.GetComponent<SpriteRenderer>().sprite;
        _infoBox.text = Item.Description;
        transform.localScale = Vector3.one;
    }

    public bool Glow
    {
        get { return _glowyThing.activeSelf; }
        set { _glowyThing.SetActive(value); }
    }

    public bool Equip
    {
        get { return _eqippyThing.activeSelf; }
        set { _eqippyThing.SetActive(value); }
    }
}