using System.Collections.Generic;
using System.Net;
using UnityEditor.IMGUI.Controls;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    public List<Item> Items;

    private int _columns, _rows, _spacing;


    private GameObject[,] _itemGrid;

    private void Awake()
    {
        if (Items == null)
        {
            Items = new List<Item>();
        }
        
        //
        //Spawn Item Gird? 
        //
        _itemGrid = new GameObject[_rows, _columns];
    }


    public void AddItem(Item item)
    {
        Items.Add(item);
    }

    public void ShowInventory()
    {
        this.gameObject.SetActive(true);
    }

    private void ItemGrid(int row, int column, bool enable)
    {
        _itemGrid[row, column].SetActive(enable);
    }

    public void ArrangeItems()
    {
        for (int i = 0; i < _rows; i++)
        {
            for (int j = 0; j < _columns; j++)
            {
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
    }
}