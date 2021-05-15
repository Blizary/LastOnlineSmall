using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablesManager : MonoBehaviour
{
    public List<GameObject> interactables;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ActivateInteractablez()
    {
        foreach(GameObject inte in interactables)
        {
            inte.SetActive(true);
        }
    }
}
