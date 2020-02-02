using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotController : MonoBehaviour
{
    // Start is called before the first frame update
    public Item item;
    public int stackSize;

    public Sprite emptyIcon;

    public Text stackSizeText;
    public Image icon;

    public void SetItem(Item item){
        this.item = item;
        this.stackSize = item.stackSize;
        this.stackSizeText.text = this.stackSize.ToString();
        this.icon.sprite = item.icon;
    }
    public void ResetItem() 
    {
        this.item = null;
        this.stackSize = 0;
        this.stackSizeText.text = "";
        this.icon.sprite = emptyIcon;
    }
}

