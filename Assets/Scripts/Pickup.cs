using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : Interactable
{
    public int itemId;
    private ItemDatabase _id;
    public void Start()
    {
        _id = ItemDatabase.Instance;
    }
    public override void interact()
    {
        PlayerController player = (PlayerController)FindObjectOfType(typeof(PlayerController));
        player.GetComponent<Inventory>().addItem(_id.getItemFromId(itemId));
        Destroy(this.gameObject);
    }

}
