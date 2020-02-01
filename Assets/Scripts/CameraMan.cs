using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMan : MonoBehaviour
{
    public Transform target;
    public float marginPercentage;
    public float easeRate;

    Camera myCamera;

    void Awake()
    {
        myCamera = GetComponent<Camera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPos = myCamera.WorldToScreenPoint(target.position);

        float marginX = marginPercentage * myCamera.pixelWidth / 2;
        float marginY = marginPercentage * myCamera.pixelHeight / 2;

        float deltaX = 0;
        float deltaY = 0;
        if (screenPos.x < marginX)
        {
            deltaX = screenPos.x - marginX;
        }
        if (screenPos.x > myCamera.pixelWidth - marginX)
        {
            deltaX = screenPos.x - (myCamera.pixelWidth - marginX);
        }
        if (screenPos.y < marginY)
        {
            deltaY = screenPos.y - marginY;
        }
        if (screenPos.y > myCamera.pixelHeight - marginY)
        {
            deltaY = screenPos.y - (myCamera.pixelHeight - marginY);
        }

        myCamera.transform.position += new Vector3(deltaX, deltaY, 0) * easeRate * Time.deltaTime;
    }
}
