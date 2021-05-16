using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayerChatBubble : MonoBehaviour
{
    public Camera lookAtCam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = lookAtCam.transform.position - transform.position;

        v.x = 0;
        v.z = 0;

        transform.LookAt(lookAtCam.transform.position - v);
        //transform.Rotate(0, 180, 0);
    }
}
