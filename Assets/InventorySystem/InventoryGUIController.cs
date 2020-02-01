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
    private ItemDatabase _id;
    void Start()
    {

        //inv = player.GetComponent<Inventory>();
        //_id = ItemDatabase.Instance;
        //Item i = _id.items[3];  
        //Item k = _id.items[2];
        //i.stackSize = 5;
        //k.stackSize = 2;
        //inv.addItem(i);
        //inv.addItem(k);

        ////_id = ItemDatabase.Instance;
        ////inv = new Inventory();
        ////Debug.Log(_id);
        ////for (int g = 0; g < 3; g++)
        ////{
        ////    inv.addItem(_id.items[g]);
        ////}
        ////Debug.Log(inv.inventory.Count);
        ////inv.removeItem(1);
        ////for (int z = 0; z < inv.inventory.Count; z++)
        ////{
        ////    Debug.Log(inv.inventory[z]);
        ////}
        ////TODO: Take this out and implement when pc is in
        ////for (int j = 0; j < i.inventory.Count; j++)
        //initializeEmptySlots();
        //refresh();
        //inv.removeItem(1);
        //refresh();
        //inv.addItem(i);
        //inv.removeItem(3);
        //refresh();
        initializeEmptySlots();

    }
    public void initializeEmptySlots()
    {
        for (int x = 0; x < player.GetComponent<Inventory>().maxSize; x++)
        {
            Instantiate(inventorySlot, this.transform);
        }
    }
    public void refresh()
    {
        for (int g = 0; g < player.GetComponent<Inventory>().inventory.Count; g++)
        {
            Inventory tmpInv = player.GetComponent<Inventory>();
            this.transform.GetChild(g).gameObject.GetComponent<InventorySlotController>().item = tmpInv.inventory[g];
            this.transform.GetChild(g).gameObject.GetComponentInChildren<Image>().sprite = tmpInv.inventory[g].icon;
            this.transform.GetChild(g).gameObject.GetComponentInChildren<Text>().text = tmpInv.inventory[g].stackSize.ToString();
        }
        for (int z = player.GetComponent<Inventory>().inventory.Count; z < player.GetComponent<Inventory>().maxSize; z++)
        {
            this.transform.GetChild(z).gameObject.GetComponent<InventorySlotController>().item = null;
            this.transform.GetChild(z).gameObject.GetComponentInChildren<Image>().sprite = null;
            this.transform.GetChild(z).gameObject.GetComponentInChildren<Text>().text = null;
        }
    }
    void populateBar()
    {
        System.Diagnostics.Debug.Write("tetst");
    }
}
