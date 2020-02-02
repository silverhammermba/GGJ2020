using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pickup : Interactable
{
    public int itemId;
    public bool isBox;
    public List<int> spawnables;
    private ItemDatabase _id;
    public string openchest_idle_name;
    public string unlooted_idle;
    public GameObject lootboxPref;
    public List<AudioClip> pickupSounds;
    private AudioSource audioSource;
    public void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        _id = ItemDatabase.Instance;
    }
    public override void interact()
    {
        if (isBox)
        {
            createItems();
            this.GetComponent<Animator>().Play(openchest_idle_name);
            //this.GetComponent<SpriteRenderer>().sprite = openchest;

            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            //Destroy(this.gameObject.GetComponent<Rigidbody2D>());
        }
        else
        {
            PlayerController player = (PlayerController)FindObjectOfType(typeof(PlayerController));
            if (player.GetComponent<Inventory>().addItem(_id.getItemFromId(itemId)))
            {
                audioSource.PlayOneShot(pickupSounds[Random.Range(0, pickupSounds.Count)], .47f);
                Debug.LogWarning("Played audio");
                Destroy(this.gameObject);
            }
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
