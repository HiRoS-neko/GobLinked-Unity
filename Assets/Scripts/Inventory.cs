using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private int _columns, _rows, _spacing;

    private GameObject[,] _itemGrid;

    private Goblin _krilkGoblin, _gnoxGoblin;
    public List<Item> Items;

    private void Awake()
    {
        if (Items == null) Items = new List<Item>();

        //
        //Spawn Item Gird? 
        //
        _itemGrid = new GameObject[_rows, _columns];
    }


    public void EquipItem(Item item)
    {
        var goblinType = item.GoblinType;
        EquipItem(item, goblinType);
    }

    public void EquipItem(Item item, Goblin.GoblinType goblinType)
    {
        switch (goblinType)
        {
            case Goblin.GoblinType.Krilk:
                if (item.GetType() == typeof(Weapon))
                    _krilkGoblin.EquippedWeapon = (Weapon) item;
                else if (item.GetType() == typeof(Accessory))
                    _krilkGoblin.EquippedAccessory = (Accessory) item;
                else if (item.GetType() == typeof(Consumable)) _krilkGoblin.UseConsumable((Consumable) item);

                break;
            case Goblin.GoblinType.Gnox:
                if (item.GetType() == typeof(Weapon))
                    _gnoxGoblin.EquippedWeapon = (Weapon) item;
                else if (item.GetType() == typeof(Accessory))
                    _gnoxGoblin.EquippedAccessory = (Accessory) item;
                else if (item.GetType() == typeof(Consumable)) _gnoxGoblin.UseConsumable((Consumable) item);

                break;
            case Goblin.GoblinType.Both:
                break;
        }
    }

    public void AddItem(Item item)
    {
        Items.Add(item);
    }

    public void ShowInventory()
    {
        gameObject.SetActive(true);
    }

    private void ItemGrid(int row, int column, bool enable)
    {
        _itemGrid[row, column].SetActive(enable);
    }

    public void ArrangeItems()
    {
        for (var i = 0; i < _rows; i++)
        for (var j = 0; j < _columns; j++)
            if (i * _columns + j < Items.Count)
            {
                Items[i * _columns + j].transform.position =
                    Vector3.up * _spacing * i + Vector3.right * _spacing * j +
                    gameObject.transform.position.z * Vector3.forward;
                ItemGrid(i, j, true);
            }
            else
            {
                ItemGrid(i, j, false);
            }
    }
}