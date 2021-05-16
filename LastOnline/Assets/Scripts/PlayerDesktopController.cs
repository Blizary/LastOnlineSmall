using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDesktopController : MonoBehaviour
{
    CharacterController controller;
    DesktopManager manager;

    public string currentAwnser;
    public ChatOption currentOption;

    public GameObject desktop;

    private MsnManager chatManager;
    private DoorsManager doorsManager;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        manager = GameObject.FindGameObjectWithTag("DesktopManager").GetComponent<DesktopManager>();

        chatManager = GameObject.FindGameObjectWithTag("MsnManager").GetComponent<MsnManager>();
        doorsManager = GameObject.FindGameObjectWithTag("DoorsManager").GetComponent<DoorsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentAwnser != "")
        {
            if (doorsManager.HoldToType())
            {
                if (!Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1) && Input.anyKey)
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
            if (Input.GetKeyDown(KeyCode.Return))
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











}

    



