using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.Timeline;

public class Inventory : MonoBehaviour
{
    private int _columns, _rows, _spacing;

    private Goblin _krilkGoblin, _gnoxGoblin;
    public List<Item> Items;

    private List<ItemObject> _itemObjects;

    private int _selected;
    private float _prevMove;


    [SerializeField] private ItemObject _itemObject;

    [SerializeField] private GameObject _content;
    private bool _changed;

    public Inventory()
    {
        if (Items == null) Items = new List<Item>();
        _itemObjects = new List<ItemObject>();
    }

    private void Update()
    {
        var move = Input.GetAxisRaw("Vertical");
        if (!Mathf.Approximately(move, _prevMove) && _changed && _itemObjects.Count > 0)
        {
            //turn off old outline
            _selected = _selected % _itemObjects.Count;
            _itemObjects[_selected].SetGlow(false);

            if (move < -0.5)
                _selected += 1;
            else if (move > 0.5)
                _selected += (_itemObjects.Count - 1);

            //make sure selected is within the number of items;


            //turn on new outline
            _selected = _selected % _itemObjects.Count;
            _itemObjects[_selected].SetGlow(true);
        }

        if (Mathf.Approximately(move, 0))
        {
            _changed = true;
        }

        _prevMove = move;

        if (Input.GetAxis("SubmitPlayer1") > 0.5)
        {
            EquipItem(Items[_selected], GlobalScript.PlayerController.Player1.GoblinType);
        }
        else if (GlobalScript.PlayerController._gameMode == PlayerController.GameMode.MultiPlayer &&
                 Input.GetAxis("SubmitPlayer2") > 0.5)
        {
            EquipItem(Items[_selected], GlobalScript.PlayerController.Player2.GoblinType);
        }
    }

    public void EquipItem(Item item, Goblin.GoblinType goblinType)
    {
        if (goblinType != item.GoblinType && item.GoblinType != Goblin.GoblinType.Both) return;
        switch (goblinType)
        {
            case Goblin.GoblinType.Krilk:
                if (item.GetType() == typeof(Weapon))
                    _krilkGoblin.EquippedWeapon = (Weapon) item;
                else if (item.GetType() == typeof(Accessory))
                    _krilkGoblin.EquippedAccessory = (Accessory) item;
                else if (item.GetType() == typeof(Consumable))
                {
                    _krilkGoblin.UseConsumable((Consumable) item);
                    Items.Remove(item);
                }

                break;
            case Goblin.GoblinType.Gnox:
                if (item.GetType() == typeof(Weapon))
                    _gnoxGoblin.EquippedWeapon = (Weapon) item;
                else if (item.GetType() == typeof(Accessory))
                    _gnoxGoblin.EquippedAccessory = (Accessory) item;
                else if (item.GetType() == typeof(Consumable))
                {
                    _gnoxGoblin.UseConsumable((Consumable) item);
                    Items.Remove(item);
                }

                break;
            case Goblin.GoblinType.Both:
                break;
        }
    }

    public void AddItem(Item item)
    {
        Items.Add(item);
        var newItem = Instantiate(_itemObject, Vector3.zero, Quaternion.identity);
        newItem.Item = item;
        newItem.Init();

        newItem.transform.SetParent(_content.transform);

        _itemObjects.Add(newItem);
    }

    public void ShowInventory(bool show)
    {
        gameObject.SetActive(show);
    }
}