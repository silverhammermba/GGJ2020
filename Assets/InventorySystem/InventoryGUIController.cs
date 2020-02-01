using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryGUIController : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> inventorySlots;
    public GameObject inventoryBar;
    public GameObject inventorySlot;
    void Start()
    {
        Inventory i = new Inventory();
        //for (int j = 0; j < i.inventory.Count; j++)
        for (int j = 0; j < 5; j++)
        {

            Instantiate(inventorySlot, this.transform);
            

        }
    }
}
