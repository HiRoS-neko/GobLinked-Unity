using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private bool _changed;
    private int _columns, _rows, _spacing;

    [SerializeField] private GameObject _content;
    private ItemObject _equipEquipGnox;
    private ItemObject _equipEquipKrilk;
    private ItemObject _equipWeapGnox;
    private ItemObject _equipWeapKrilk;


    [SerializeField] private ItemObject _itemObject;

    private List<ItemObject> _itemObjects = new List<ItemObject>();

    private Goblin _krilkGoblin, _gnoxGoblin;
    private float _prevMove;

    private int _selected;
    public List<Item> Items = new List<Item>();
    private bool _toggle;

    private void Awake()
    {
        GlobalScript.SceneChanged += GlobalScriptOnSceneChanged;
    }

    private void GlobalScriptOnSceneChanged(string prevScene)
    {
        _selected = 0;
        //reinit inventory?
        _itemObjects = _content.GetComponentsInChildren<ItemObject>().ToList();
        Items = new List<Item>();
        foreach (var item in _itemObjects)
        {
            Items.Add(item.Item);
            if (item.Equip)
            {
                var i = item.Item;
                if (i is Weapon)
                {
                    var weapon = (Weapon) i;
                    if (i.GoblinType == Goblin.GoblinType.Gnox)
                    {
                        GlobalScript.Gnox.EquippedWeapon = weapon;
                    }
                    else
                    {
                        GlobalScript.Krilk.EquippedWeapon = weapon;
                    }
                }
                else if (i is Equipment)
                {
                    var equipment = (Equipment) i;
                    if (i.GoblinType == Goblin.GoblinType.Gnox)
                    {
                        GlobalScript.Gnox.EquippedAccessory = equipment;
                    }
                    else
                    {
                        GlobalScript.Krilk.EquippedAccessory = equipment;
                    }
                }
            }

            item.Glow = false;
        }
    }

    private void Update()
    {
        var move = Input.GetAxisRaw("Vertical");
        if (Mathf.Abs(move) > 0.5f && _changed && _itemObjects.Count > 0)
        {
            _changed = false;
            //turn off old outline
            _selected = _selected % _itemObjects.Count;
            _itemObjects[_selected].Glow = false;
            if (move < -0.5)
                _selected += 1;
            else if (move > 0.5)
                _selected += _itemObjects.Count - 1;
            //make sure selected is within the number of items;
            //turn on new outline
            _selected = _selected % _itemObjects.Count;
            _itemObjects[_selected].Glow = true;
        }

        if (Mathf.Abs(move) > 0.5f) _changed = true;

        if (Mathf.Abs(Input.GetAxisRaw("SubmitPlayer1")) > 0.5 && _toggle)
        {
            EquipItem(Items[_selected], GlobalScript.PlayerController.Player1.GoblinType, _selected);
            _toggle = false;
        }
        else if (GlobalScript.PlayerController._gameMode == PlayerController.GameMode.MultiPlayer &&
                 Mathf.Abs(Input.GetAxisRaw("SubmitPlayer2")) > 0.5 && _toggle)
        {
            EquipItem(Items[_selected], GlobalScript.PlayerController.Player2.GoblinType, _selected);
            _toggle = false;
        }
        else if (Mathf.Abs(Input.GetAxisRaw("SubmitPlayer1")) < 0.5 &&
                 Mathf.Abs(Input.GetAxisRaw("SubmitPlayer2")) < 0.5)
        {
            _toggle = true;
        }
    }

    public void EquipItem(Item item, Goblin.GoblinType goblinType, int index)
    {
        if (goblinType != item.GoblinType && item.GoblinType != Goblin.GoblinType.Both) return;
        switch (goblinType)
        {
            case Goblin.GoblinType.Krilk:
                if (item is Weapon)
                {
                    if (_equipWeapKrilk != null) _equipWeapKrilk.Equip = false;
                    GlobalScript.Krilk.EquippedWeapon = null;
                    GlobalScript.Krilk.EquippedWeapon = (Weapon) item;
                    _equipWeapKrilk = _itemObjects[index];
                    _equipWeapKrilk.Equip = true;
                }
                else if (item is Equipment)
                {
                    if (_equipEquipKrilk != null) _equipEquipKrilk.Equip = false;
                    GlobalScript.Krilk.EquippedAccessory = null;
                    GlobalScript.Krilk.EquippedAccessory = (Equipment) item;
                    _equipEquipKrilk = _itemObjects[index];
                    _equipEquipKrilk.Equip = true;
                }
                else if (item is Consumable)
                {
                    GlobalScript.Krilk.UseConsumable((Consumable) item);
                    Items.RemoveAt(index);
                    Destroy(_itemObjects[index].gameObject);
                    _itemObjects.RemoveAt(index);
                }

                break;
            case Goblin.GoblinType.Gnox:
                if (item is Weapon)
                {
                    if (_equipWeapGnox != null) _equipWeapGnox.Equip = false;
                    GlobalScript.Gnox.EquippedWeapon = null;
                    GlobalScript.Gnox.EquippedWeapon = (Weapon) item;
                    _equipWeapGnox = _itemObjects[index];
                    _equipWeapGnox.Equip = true;
                }
                else if (item is Equipment)
                {
                    if (_equipEquipGnox != null) _equipEquipGnox.Equip = false;
                    GlobalScript.Gnox.EquippedAccessory = null;
                    GlobalScript.Gnox.EquippedAccessory = (Equipment) item;
                    _equipEquipGnox = _itemObjects[index];
                    _equipEquipGnox.Equip = true;
                }
                else if (item is Consumable)
                {
                    GlobalScript.Gnox.UseConsumable((Consumable) item);
                    Items.RemoveAt(index);
                    Destroy(_itemObjects[index].gameObject);
                    _itemObjects.RemoveAt(index);
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