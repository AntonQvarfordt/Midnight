using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int SlotCount = 5;

    public List<KeyValuePair<string, Item>> InventorySlots = new List<KeyValuePair<string, Item>>();

    public Item LeftHandItem;
    public Item RightHandItem;

    public Transform LeftHandObjectPositioner;
    public Transform RightHandObjectPositioner;

    private void Start()
    {
        for (int i = 0; i < SlotCount; i++)
        {
            InventorySlots.Add(new KeyValuePair<string, Item>("Empty", null));
        }

        AddItem(GameManager.GetItemPrefab("Gun"));
    }

    private void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            var gunSlot = SlotIndexByItemName("Gun");

            if (gunSlot == -1)
            {
                Debug.LogWarning("Requested item not found");
                return;
            }

            EquipItem(gunSlot);
        }

        if (Input.GetKeyDown("q"))
        {
            UnequipItem();
        }
    }

    private void AddItem(Item item)
    {
        if (GetEmptySlot() == -1)
        {
            Debug.Log("No Empty Slots Found");
            return;
        }

        InventorySlots[GetEmptySlot()] = new KeyValuePair<string, Item>(item.Name, item);
    }

    private void EquipItem(int slot)
    {
        if (RightHandItem == null)
        {
            RightHandItem = InventorySlots[slot].Value;
            RightHandObjectPositioner.transform.parent.gameObject.SetActive(true);
            RightHandItem.Equipped = true;
            CreateEquippedGObject(InventorySlots[slot].Value, true);
            Debug.Log("*Inventory* Equipped Gun in Right Hand");
        }

        else if (LeftHandItem == null)
        {
            LeftHandItem = InventorySlots[slot].Value;
            LeftHandItem.Equipped = true;
            LeftHandObjectPositioner.transform.parent.gameObject.SetActive(true);
            CreateEquippedGObject(InventorySlots[slot].Value, false);
            Debug.Log("*Inventory* Equipped Gun in Left left");
        }
        else
        {
            Debug.Log("You have no hands free to equip an item to");
        }
    }

    private void UnequipItem()
    {
        if (LeftHandItem != null)
        {
            LeftHandObjectPositioner.transform.parent.gameObject.SetActive(false);
            LeftHandItem.Equipped = false;
            var lhItem = LeftHandObjectPositioner.transform.GetChild(0).gameObject;
            LeftHandItem = null;
            Destroy(lhItem);
            return;
        }
        if (RightHandItem != null)
        {
            RightHandObjectPositioner.transform.parent.gameObject.SetActive(false);
            RightHandItem.Equipped = false;
            var rhItem = RightHandObjectPositioner.transform.GetChild(0).gameObject;
            RightHandItem = null;
            Destroy(rhItem);
            return;
        }

        Debug.LogWarning("You have nothing to unequip");
        return;
    }

    private void CreateEquippedGObject(Item item, bool rightHand)
    {
        var itemObject = Instantiate(item.gameObject);

        if (rightHand)
            itemObject.transform.SetParent(RightHandObjectPositioner);
        else
            itemObject.transform.SetParent(LeftHandObjectPositioner);

        itemObject.transform.localPosition = Vector3.zero;
        itemObject.transform.localRotation = Quaternion.Euler(new Vector3(-90, 0, 0));

    }

    private int GetEmptySlot()
    {
        var iteration = 0;
        foreach (var slot in InventorySlots)
        {
            if (slot.Key == "Empty")
            {
                return iteration;
            }
            iteration++;
        }

        return -1;
    }

    private int SlotIndexByItemName(string itemName)
    {
        var iteration = 0;
        foreach (var slot in InventorySlots)
        {
            if (slot.Key == itemName)
            {
                return iteration;
            }

            iteration++;
        }

        return -1;
    }

    private void RemoveItem()
    {
    }

    private void ShowRightArm()
    {

    }

    private void HideRightArms()
    {

    }

    private void ShowLeftArm()
    {

    }

    private void HideLeftArm()
    {

    }
}
