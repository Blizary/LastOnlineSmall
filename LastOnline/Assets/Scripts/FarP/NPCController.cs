using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NPCController : MonoBehaviour
{
    public NPC npcInfo;
    private FarPersonManager manager;
    public GameObject chatBubble;
    private List<GameObject> movement = new List<GameObject>();
    private float speed = 5;

    public List<GameObject> rpMovement;

    private GameObject parent;
   

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<FarPersonManager>();
        parent = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(movement.Count!=0)
        {
            if(movement.Count == 1)
            {
                Vector3 dir = movement[0].transform.position;
                dir.y = transform.position.y;
                transform.LookAt(dir);
                movement.RemoveAt(0);

            }
            else
            {
                float step = speed * Time.deltaTime; // calculate distance to move
                Vector3 dir = movement[0].transform.position;
                dir.y = transform.position.y;
                parent.transform.position = Vector3.MoveTowards(parent.transform.position, dir, step);
                parent.transform.LookAt(dir);

                // Check if the position of the cube and sphere are approximately equal.
                if (Vector3.Distance(parent.transform.position, dir) < 0.1f)
                {
                    // Swap the position of the cylinder.
                    movement.RemoveAt(0);
                }
            }
          
        }
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

    public void RPMove()
    {
        for(int i =0;i<rpMovement.Count;i++)
        {
            movement.Add(rpMovement[i]);
        }
    }
}
