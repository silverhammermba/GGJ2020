using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[SerializeField]    
public class EnableDash : UnityEvent<bool> {
    public void enableDash(PlayerController p )
    {
        p.canDash = true;
    }
}
