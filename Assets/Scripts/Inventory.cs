using System.Collections.Generic;
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

    private readonly List<ItemObject> _itemObjects;

    private Goblin _krilkGoblin, _gnoxGoblin;
    private float _prevMove;

    private int _selected;
    public List<Item> Items;

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
                _selected += _itemObjects.Count - 1;
            //make sure selected is within the number of items;
            //turn on new outline
            _selected = _selected % _itemObjects.Count;
            _itemObjects[_selected].SetGlow(true);
        }

        if (Mathf.Approximately(move, 0)) _changed = true;

        _prevMove = move;

        if (Input.GetAxisRaw("SubmitPlayer1") > 0.5)
            EquipItem(Items[_selected], GlobalScript.PlayerController.Player1.GoblinType, _selected);
        else if (GlobalScript.PlayerController._gameMode == PlayerController.GameMode.MultiPlayer &&
                 Input.GetAxisRaw("SubmitPlayer2") > 0.5)
            EquipItem(Items[_selected], GlobalScript.PlayerController.Player2.GoblinType, _selected);
    }

    public void EquipItem(Item item, Goblin.GoblinType goblinType, int index)
    {
        if (goblinType != item.GoblinType && item.GoblinType != Goblin.GoblinType.Both) return;
        switch (goblinType)
        {
            case Goblin.GoblinType.Krilk:
                if (item is Weapon)
                {
                    if (_equipWeapKrilk != null) _equipWeapKrilk.SetEquip(false);
                    GlobalScript.Krilk.EquippedWeapon = (Weapon) item;
                    _equipWeapKrilk = _itemObjects[index];
                    _equipWeapKrilk.SetEquip(true);
                }
                else if (item is Equipment)
                {
                    if (_equipEquipKrilk != null) _equipEquipKrilk.SetEquip(false);
                    GlobalScript.Krilk.EquippedAccessory = (Equipment) item;
                    _equipEquipKrilk = _itemObjects[index];
                    _equipEquipKrilk.SetEquip(true);
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
                    if (_equipWeapGnox != null) _equipWeapGnox.SetEquip(false);
                    GlobalScript.Gnox.EquippedWeapon = (Weapon) item;
                    _equipWeapGnox = _itemObjects[index];
                    _equipWeapGnox.SetEquip(true);
                }
                else if (item is Equipment)
                {
                    if (_equipEquipGnox != null) _equipEquipGnox.SetEquip(false);
                    GlobalScript.Gnox.EquippedAccessory = (Equipment) item;
                    _equipEquipGnox = _itemObjects[index];
                    _equipEquipGnox.SetEquip(true);
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