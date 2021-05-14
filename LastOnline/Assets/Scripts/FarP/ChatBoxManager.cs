﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatBoxManager : MonoBehaviour
{
    public GameObject tabsContainer;
    public GameObject tabsPrefab;
    public GameObject generalTextContainer;//original text obj in the world used to render all text
    public GameObject textPrefab;
    public GameObject scrollUpTextButton;
    public GameObject scrollDownTextButton;
    public GameObject UITrash;
    public GameObject optionsPanel;

    public int maxNumOfMessages;//maximun amount of shown messages at 1 time
    public float inactivityChatTimer;// amount of time after witch the chat resets to the latest messages
    private float innerInactivityChatTimer;

    public List<Tab> tabs;


    private int currentChat;
    private bool scrolling;
    private int currentChatIndx;
    private FarPersonManager manager;
    private ThirdPersonMovement playerController;


    private float innerTimer;
    private bool messageFinnish;

    // Start is called before the first frame update
    void Start()
    {
        tabs = new List<Tab>();

        currentChat = 0;
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<FarPersonManager>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonMovement>();
        scrolling = false;
        messageFinnish = false;
        ReadChatLists();
        StartChat();
    }

    // Update is called once per frame
    void Update()
    {
        ShowScrollButtons();
        InactiveChatTimer();

    }

    /// <summary>
    /// reads a new message in the chat
    /// </summary>
    /// <param name="_nextMessage"></param>
    void ReadNextMessage(int _message, ChatText _newText, Tab _currentTab)
    {
        if(_newText!=null)
        {
            //if its in say chat and has a speaker trigger the chat ballons
            if (!string.IsNullOrEmpty(_newText.speaker))
            {
                string textbubble = "";
                for (int i = 0; i < _newText.chatText.Count; i++)//for each line in this message
                {
                    textbubble += _newText.chatText[i] + "\n";
                    Debug.Log(textbubble);
                }
                if (GameObject.Find(_newText.speaker))
                {
                    GameObject speakerObj = GameObject.Find(_newText.speaker);
                    speakerObj.transform.GetChild(0).GetComponent<NPCController>().SetChatBubble(textbubble);
                }

            }
        }
        
      


        if (_currentTab.tabChat.Count > _message)//check if there are any messages left in this conversation
        {
            if (_newText == null)
            {
                _newText = _currentTab.tabChat[_message];
            }


            for (int i = 0; i < _newText.chatText.Count; i++)//for each line in this message
            {
                if (i == 0)//check if it is the 1st message so the name is added
                {
                    _currentTab.displayedText.Add("[" + _newText.characterName + "] " + _newText.chatText[i]);
                }
                else//dont add name
                {
                    _currentTab.displayedText.Add(_newText.chatText[i]);
                }


                //check if the scroll up is active
                if (!scrolling && currentChat == _currentTab.tabNum)
                {
                    GameObject newText = Instantiate(textPrefab, generalTextContainer.transform);//create a new line        
                    if (i == 0)//check if it is the 1st message so the name is added
                    {
                        newText.GetComponent<TextMeshProUGUI>().text = "[" + _newText.characterName + "] " + _newText.chatText[i];
                    }
                    else//dont add name
                    {
                        newText.GetComponent<TextMeshProUGUI>().text = _newText.chatText[i];
                    }
                    _currentTab.currentText += 1;
                    //chatNumb[0] += 1;//current chat possion

                    CheckOverflow(0);//check if overflow
                }
                else
                {
                    if(_currentTab.type!=ChatType.chatPublic && _currentTab.type != ChatType.chatSay)
                    {
                        _currentTab.tabObj.GetComponent<TabController>().SomethingNew();
                    }
                    

                }


            }

            _currentTab.lastMessage = _newText;

            //check if it requires an awnser
            if (_newText.options.Count != 0)
            {
                if (currentChat == _currentTab.tabNum)//check if this is the active chat
                {
                    //set options
                    for (int i = 0; i < _newText.options.Count; i++)
                    {
                        optionsPanel.transform.GetChild(i).GetComponent<ChoiceController>().SetValues(tabs[currentChat].lastMessage.options[i]);
                    }
                }

                if (!scrolling && currentChat == _currentTab.tabNum)//show the options to pick if it is the active chat
                {
                    manager.ChatStartButton();
                }
                //wait for anwser

            }
            else if(_newText.nextNpcText !=null)
            {

                StartCoroutine(WaitForNextSentence(_newText.nextNpcText.timer, _currentTab, _currentTab.tabChat.Count - 1, _newText.nextNpcText)); 
            }
            else
            {
                //start timer for next sentence
                StartCoroutine(WaitForNextSentence(_currentTab.tabChat[_message].timer, _currentTab, _message + 1, null));
            }

        }

    }

    /// <summary>
    /// Reads and coppies all the data available in the manager so we can manipulate them in this script
    /// without damaging the scriptable objs
    /// </summary>
    void ReadChatLists()
    {
        for (int i = 0; i < manager.chatbox.Count; i++)
        {
            Tab newTab = new Tab();
            //get the name
            if (manager.chatbox[i].chatType == ChatType.chatPublic)
            {
                newTab.tabName = "General";
                GameObject newtabDisplay = Instantiate(tabsPrefab, tabsContainer.transform);//create a new tab    
                newTab.tabObj = newtabDisplay;
                newTab.tabObj.GetComponent<TabController>().Selected();
            }
            else if(manager.chatbox[i].chatType == ChatType.chatSay)
            {
                newTab.tabName = "Say";
            }
            else
            {
                newTab.tabName = manager.chatbox[i].conversation[0].characterName;
            }

            //get the conversation
            newTab.tabChat = manager.chatbox[i].conversation;

            //set type
            newTab.type = manager.chatbox[i].chatType;

            //start at 0 chat
            newTab.currentText = 0;
            //inicialize lists
            newTab.displayedText = new List<string>();
            newTab.tabNum = i;

            tabs.Add(newTab);


        }
    }


    void StartChat()
    {
        for (int i = 0; i < tabs.Count; i++)
        {
            StartCoroutine(WaitForNextSentence(tabs[i].tabChat[0].timer, tabs[i], 0, null));
        }
    }

    IEnumerator WaitForNextSentence(float _timer, Tab _currentTab, int _message, ChatText _newText)
    {
        //wait for timer
        yield return new WaitForSeconds(_timer);
        //check if it is a new tab
        if (_currentTab.type != ChatType.chatPublic)
        {
            if (_message == 0)
            {
                GameObject newtabDisplay = Instantiate(tabsPrefab, tabsContainer.transform);//create a new tab  
                newtabDisplay.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _currentTab.tabName;
                newtabDisplay.GetComponent<TabController>().tabnum = _currentTab.tabNum;
                _currentTab.tabObj = newtabDisplay;
            }
        }

        //read next message
        ReadNextMessage(_message, _newText, _currentTab);
    }

    void CheckOverflow(int _chat)
    {

        if (generalTextContainer.transform.childCount > maxNumOfMessages)
        {
            GameObject trashtext = generalTextContainer.transform.GetChild(0).gameObject;
            trashtext.transform.SetParent(UITrash.transform);
            Destroy(trashtext);

        }
    }

    void ShowScrollButtons()
    {
        if (tabs[currentChat].currentText > maxNumOfMessages)
        {
            scrollUpTextButton.SetActive(true);
        }
        else
        {
            scrollUpTextButton.SetActive(false);
        }


        if (tabs[currentChat].currentText < tabs[currentChat].displayedText.Count - 1)
        {
            scrollDownTextButton.SetActive(true);
        }
        else
        {
            scrollDownTextButton.SetActive(false);
        }
    }


    void ActivatedChat()
    {
        innerInactivityChatTimer = inactivityChatTimer;//triggers the timer
        scrolling = true;
    }

    void ResetInactivityChatTimer()
    {
        innerInactivityChatTimer = 0;
        scrolling = false;
    }

    void InactiveChatTimer()
    {
        if (scrolling)
        {
            if (innerInactivityChatTimer > 0)
            {
                innerInactivityChatTimer -= Time.deltaTime;//ticking time
            }
            else// reset the chat
            {

                //clear all messages
                foreach (Transform text in generalTextContainer.transform)
                {
                    GameObject.Destroy(text.gameObject);

                }

                //add lastest messages
                for (int i = 0; i < maxNumOfMessages; i++)
                {
                    GameObject newText = Instantiate(textPrefab, generalTextContainer.transform);
                    newText.GetComponent<TextMeshProUGUI>().text = tabs[currentChat].displayedText[tabs[currentChat].displayedText.Count - (maxNumOfMessages - i)];
                    tabs[currentChat].currentText = tabs[currentChat].displayedText.Count - 1;
                }

                scrolling = false;
            }
        }

    }



    public void ScrollUpText()
    {

        ActivatedChat();
        //remove i
        GameObject trashtext = generalTextContainer.transform.GetChild(generalTextContainer.transform.childCount - 1).gameObject;
        trashtext.transform.SetParent(UITrash.transform);
        Destroy(trashtext);

        tabs[currentChat].currentText -= 1;
        //add i-6
        GameObject newText = Instantiate(textPrefab, generalTextContainer.transform);
        newText.GetComponent<TextMeshProUGUI>().text = tabs[currentChat].displayedText[tabs[currentChat].currentText - maxNumOfMessages];
        newText.transform.SetAsFirstSibling();
        Debug.Log("print stuff");


    }

    public void ScrollDownText()
    {

        //add i+1
        GameObject newText = Instantiate(textPrefab, generalTextContainer.transform);
        newText.GetComponent<TextMeshProUGUI>().text = tabs[currentChat].displayedText[tabs[currentChat].currentText + 1];

        //remove top message
        GameObject trashtext = generalTextContainer.transform.GetChild(0).gameObject;
        trashtext.transform.SetParent(UITrash.transform);
        Destroy(trashtext);

        tabs[currentChat].currentText += 1;

        //check if the player is on the last available message
        if (tabs[currentChat].currentText == tabs[currentChat].displayedText.Count - 1)
        {
            scrolling = false;
            ResetInactivityChatTimer();
        }
    }


    public void TabsButton(int _tabNum)
    {
        tabs[currentChat].tabObj.GetComponent<TabController>().NotSelected();
        currentChat = _tabNum;

        //clear all messages
        foreach (Transform text in generalTextContainer.transform)
        {
            GameObject.Destroy(text.gameObject);
        }

        //add lastest messages
        for (int i = 0; i < maxNumOfMessages; i++)
        {
            if(tabs[currentChat].displayedText.Count - (maxNumOfMessages - i)>=0)
            {
                GameObject newText = Instantiate(textPrefab, generalTextContainer.transform);
                newText.GetComponent<TextMeshProUGUI>().text = tabs[currentChat].displayedText[tabs[currentChat].displayedText.Count - (maxNumOfMessages - i)];
                tabs[currentChat].currentText = tabs[currentChat].displayedText.Count - 1;
            }
            
        }

        manager.CloseChat();

        //update the choices
        for (int i = 0; i < tabs[currentChat].lastMessage.options.Count; i++)
        {
            optionsPanel.transform.GetChild(i).GetComponent<ChoiceController>().SetValues(tabs[currentChat].lastMessage.options[i]);
        }
        
        if(tabs[currentChat].lastMessage.options.Count!=0)
        {
            manager.ChatStartButton();
        }
        

    }


    public void ChoiceMade(ChatOption _option, ChatText _nextText,List<string> _awnser)
    {
        string newAwnser = "";
        for(int i = 0;i< _awnser.Count;i++)
        {
            newAwnser += _awnser[i]+" ";
        }
        playerController.currentAwnser = newAwnser;
        playerController.currentOption = _option;


        //clear options
        for (int i = 0; i < optionsPanel.transform.childCount; i++)
        {
            optionsPanel.transform.GetChild(i).gameObject.GetComponent<ChoiceController>().ClearValues();
        }

        

    }

    public void SendAwnser(ChatOption _nextText)
    {
        playerController.currentAwnser = "";
        playerController.currentOption = null;
        manager.ClearAwnser();

        //print awnser

        for (int i = 0; i < _nextText.playerAwnser.Count; i++)//for each line in this message
        {
            if (i == 0)//check if it is the 1st message so the name is added
            {
                tabs[currentChat].displayedText.Add("[" + manager.playerName + "] " + _nextText.playerAwnser[i]);
            }
            else//dont add name
            {
                tabs[currentChat].displayedText.Add(_nextText.playerAwnser[i]);
            }


            //check if the scroll up is active
            if (!scrolling && currentChat == tabs[currentChat].tabNum)
            {
                GameObject newText = Instantiate(textPrefab, generalTextContainer.transform);//create a new line        
                if (i == 0)//check if it is the 1st message so the name is added
                {
                    newText.GetComponent<TextMeshProUGUI>().text = "[" + manager.playerName + "] " + _nextText.playerAwnser[i];
                }
                else//dont add name
                {
                    newText.GetComponent<TextMeshProUGUI>().text = _nextText.playerAwnser[i];
                }
                tabs[currentChat].currentText += 1;
                //chatNumb[0] += 1;//current chat possion

                CheckOverflow(0);//check if overflow
            }


        }

        //close options
        manager.CloseChat();

        if (_nextText.npcAnwser.options.Count != 0)
        {
            StartCoroutine(WaitForNextSentence(_nextText.npcAnwser.timer, tabs[currentChat], 1, _nextText.npcAnwser));
        }
        else
        {
            StartCoroutine(WaitForNextSentence(_nextText.npcAnwser.timer, tabs[currentChat], tabs[currentChat].tabChat.Count - 1, _nextText.npcAnwser));
        }


    }


  
}