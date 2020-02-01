using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryGUIController : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> inventorySlots;
    public ItemDatabase inst;
    public Inventory inv;
    public GameObject player;
    public GameObject inventoryBar;
    public GameObject inventorySlot;

    void Start()
    {
        Inventory inv = new Inventory();
        for (int g = 0; g < 5; g++)
        {
            inv.addItem(inst.items[g]);
        }

        //TODO: Take this out and implement when pc is in
        inv = new Inventory();
        //for (int j = 0; j < i.inventory.Count; j++)
        for (int j = 0; j < 5; j++)
        {
            inventorySlot.GetComponent<InventorySlotController>().item = inv.inventory[j];
            inventorySlot.gameObject.GetComponentInChildren<Image>().sprite = inv.inventory[j].icon;
            inventorySlot.gameObject.GetComponentInChildren<Text>().text = inventorySlot.GetComponent<InventorySlotController>().stackSize.ToString();
            Instantiate(inventorySlot, this.transform);
        }

    }
    void populateBar()
    {
        System.Diagnostics.Debug.Write("tetst");
    }
}
