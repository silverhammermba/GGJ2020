using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairGuiController : MonoBehaviour
{
    public Camera camera;
    public GameObject panel;
    // Start is called before the first frame update
    //TODO: remove test class
    private void OnDrawGizmosSelected()
    {
       Vector3 p = camera.ViewportToWorldPoint(new Vector3(1,1,camera.nearClipPlane));
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(p, 0.1f);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
