using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObj : MonoBehaviour
{
    public string objName;
    [TextArea]
    public string messageOnclick;



    private ChatBoxManager chatManager;
    private FarPersonManager manager;


    // Start is called before the first frame update
    void Start()
    {
        chatManager = GameObject.FindGameObjectWithTag("ChatManager").GetComponent<ChatBoxManager>();
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<FarPersonManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnInteract()
    {
        if(objName == "Brazier")
        {
            //tell manager knife was found
            transform.parent.GetComponent<InteractablesManager>().DeactiveInteractablez();
            chatManager.KnifeFound();
        }
        else
        {
            //tell manager about message
            manager.InteractedWithItem(messageOnclick);
        }

        //turn off
        gameObject.SetActive(false);
    }
}
