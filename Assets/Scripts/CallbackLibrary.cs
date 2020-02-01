using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class CallbackLibrary : MonoBehaviour
{
    public static CallbackLibrary instance;
    public PlayerController pc;
    public float daytime;
    public static CallbackLibrary Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CallbackLibrary>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(CallbackLibrary).Name;
                    instance = obj.AddComponent<CallbackLibrary>();
                }
            }
            return instance;
        }
    }
    //// Start is called before the first frame update
    //public delegate void rewardFunction();
    //public delegate void enableDashDelegate();
    //public rewardFunction rf;
    //public enableDashDelegate edd;
    //public void rewardFunc()
    //{
    //    this.rf = rewardFunc;
    //}
    //public void enableDash()
    //{
    //    PlayerController player = (PlayerController)FindObjectOfType(typeof(PlayerController));
    //    player.canDash = true;
    //}
    public void enableDash()
    {
        Debug.Log("Dash enabled!");
        pc.canDash = true;
    }
}
