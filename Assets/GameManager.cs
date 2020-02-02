using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private UIManager _ui;
    private PlayerController _player;
    // Start is called before the first frame update
    void Start()
    {
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
