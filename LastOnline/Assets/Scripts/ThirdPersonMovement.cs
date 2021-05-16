using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    CharacterController controller;
    FarPersonManager manager;
    DesktopManager msnManager;
    public Transform cam;
    public float speed;
    public float rotSpeed;
    public float jumpStre;
    float turnSmoothVel;
    public GameObject followPlayer;

    public bool hasTarget;
    
    public string currentAwnser;
    public ChatOption currentOption;

    private ChatBoxManager chatManager;
    private bool isgrounded=true;
    private float vSpeed = 0;
    private DoorsManager doorsManager;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<FarPersonManager>();
        msnManager = GameObject.FindGameObjectWithTag("DesktopManager").GetComponent<DesktopManager>();
        chatManager = GameObject.FindGameObjectWithTag("ChatManager").GetComponent<ChatBoxManager>();
        doorsManager = GameObject.FindGameObjectWithTag("DoorsManager").GetComponent<DoorsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(horizontal, 0f, vertical).normalized;
        if(Input.GetMouseButton(1) && Input.GetMouseButton(0))
        {
            dir.z = 1;
        }
        if (!manager.inChat)//the player is currently not typing in chat
        {


            //movement
            if (dir.magnitude >= 0.1f)
            {  

                float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVel, rotSpeed);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                vSpeed -= 9.8f * Time.deltaTime;


                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.SimpleMove(moveDir.normalized * speed * Time.deltaTime);

               
            }


           

            if (Input.GetKeyDown(KeyCode.B))
            {
                manager.OpenInventory();

            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                manager.OpenCharacter();

            }


        }
        else
        {
            if(currentAwnser!="")
            {
                if(doorsManager.HoldToType())
                {
                    if (!Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1) && Input.anyKey)
                    {
                        if(manager.gameObject.activeInHierarchy)//in the mmo
                        {
                            if (manager.CheckAwnserSize())
                            {
                                //chat is full
                                manager.AwnserClearOne();
                            }

                            manager.AddAwnser(currentAwnser[0]);
                            currentAwnser = currentAwnser.Remove(0, 1);
                        }

                        
                    }

                }
                else
                {
                    if (!Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1) && Input.anyKeyDown)
                    {
                        if (manager.CheckAwnserSize())
                        {
                            //chat is full
                            manager.AwnserClearOne();
                        }

                        manager.AddAwnser(currentAwnser[0]);
                        currentAwnser = currentAwnser.Remove(0, 1);
                    }
                }

                
            }
            else
            {
                if(Input.GetKeyDown(KeyCode.Return))
                {
                    if (currentOption.triggersEvents)
                    {
                        doorsManager.StartEvent(currentOption.nextEvent);
                    }

                    chatManager.SendAwnser(currentOption);
                    manager.CloseChat();
                   
                }

        
            }
        }


        //mouse click

        if(Input.GetMouseButtonDown(0))
        {
            Targetted();
        }

        //close opend tabs and remove targets
        if(Input.GetKeyDown(KeyCode.Escape))
        {

            if (manager.CheckOpenPanels() && !manager.inChat)
            {
                manager.OpenEscPanel();
            }
            else
            {
                manager.NoTarget();
                manager.CloseRPPanel();
                manager.CloseCharacter();
                manager.CloseInventory();
                if (manager.inChat)
                {
                    manager.CloseChat();
                    manager.ClearAwnser();
                }
            }

            




        }





    }

    void Targetted()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            //found a target
            if (hit.transform.gameObject.GetComponent<NPCController>())
            {
                //found a npc
                manager.UpdateTarget(hit.transform.gameObject.GetComponent<NPCController>().npcInfo);
                if (hit.transform.gameObject.GetComponent<NPCController>().npcInfo.rpName != null)//there is an rp profile
                {
                    manager.OpenRPPanel(hit.transform.gameObject.GetComponent<NPCController>().npcInfo);
                }
            }

            //found an interactable obj
            if (hit.transform.gameObject.GetComponent<InteractObj>())
            {
                hit.transform.gameObject.GetComponent<InteractObj>().OnInteract();
            }
        }
       
    }


 
}
