using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private UIManager _ui;
    private PlayerController _player;
    public int numOfRepairedItems = 0;
    public static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(GameManager).Name;
                    instance = obj.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        numOfRepairedItems = 0;
        _ui = UIManager.Instance;
        _player = GameObject.FindObjectsOfType<PlayerController>()[0];        
    }

    public void TeleportPlayerToLocation(Transform loc)
    {
        StartCoroutine(teleportPlayer(loc));
    }

    private IEnumerator teleportPlayer(Transform loc) 
    {
        this._ui.FadeToBlack();
        yield return new WaitForSeconds(2.0f);
        this._player.transform.position = loc.position;
        this._ui.FadeFromBlack();
    }
    public void winCondition()
    {
        if (numOfRepairedItems >= 4)
        {
            Debug.LogError("you win");
        }
    }
    // Update is called once per frame
    void Update()
    {
       
    }
}
