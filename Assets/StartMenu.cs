using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class StartMenu : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        var gamepad = Gamepad.current;

		if (gamepad != null)
		{
			if (gamepad.buttonSouth.wasPressedThisFrame)
			{
				StartGame();
			}
		}
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
