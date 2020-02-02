using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pickup : Interactable
{
    public int itemId;
    public bool isBox;
    public List<int> spawnables;
    private ItemDatabase _id;
    public Sprite lootableOne;
    public Sprite lootableTwo;
    public GameObject lootboxPref;
    public void Start()
    {
        _id = ItemDatabase.Instance;
    }
    public override void interact()
    {
        if (isBox)
        {
            createItems();
        }
        PlayerController player = (PlayerController)FindObjectOfType(typeof(PlayerController));
        if (player.GetComponent<Inventory>().addItem(_id.getItemFromId(itemId)))
        {
            Destroy(this.gameObject);
        }

    }
    public void createItems()
    {
        foreach(int id in spawnables)
        {
            Debug.LogWarning("Created loot");
            lootboxPref.gameObject.GetComponent<Pickup>().itemId = id;
            GameObject loot = (GameObject) Instantiate(lootboxPref.gameObject, this.transform.position, Quaternion.identity);
            loot.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.up * 1.5f;
        }
    }

}
