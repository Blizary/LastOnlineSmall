using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public NPC npcInfo;
    private FarPersonManager manager;
    public GameObject chatBubble;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<FarPersonManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseOver()
    {
        manager.ShowToolTip(npcInfo);
    }


    void OnMouseExit()
    {
        manager.CloseToolTip();
    }

    public void SetChatBubble(string _newText)
    {
        chatBubble.GetComponent<ChatBubbleController>().SetChat(_newText);
        chatBubble.GetComponent<ChatBubbleController>().StartTimer();
    }
}
