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
            this.transform.GetChild(g).GetComponent<InventorySlotController>().SetItem(tmpInv.inventory[g]);
        }
        for (int z = player.GetComponent<Inventory>().inventory.Count; z < player.GetComponent<Inventory>().maxSize; z++)
        {
            this.transform.GetChild(z).GetComponent<InventorySlotController>().ResetItem();
        }
    }
    void populateBar()
    {
        System.Diagnostics.Debug.Write("tetst");
    }
}
