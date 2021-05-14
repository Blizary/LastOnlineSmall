using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobUpAndDown : MonoBehaviour
{

    public AnimationCurve upAndDownCurve;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, upAndDownCurve.Evaluate((Time.time % upAndDownCurve.length)), transform.position.z);
    }
}
