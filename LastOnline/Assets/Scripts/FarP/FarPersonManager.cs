using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FarPersonManager : MonoBehaviour
{
    public string playerName;
    public int awnserMaxSize;

    public List<ChatConv> chatbox;
    [SerializeField] private GameObject inventoryOBJ;
    [SerializeField] private GameObject characterOBJ;
    [SerializeField] private string noTargetMessage;
    [SerializeField] private string friendlyTargetMessage;
    [SerializeField] private GameObject warningOBJ;

    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject playerAwnser;
    [SerializeField] private GameObject rpPanel;
    [SerializeField] private GameObject tooltipPanel;
    [SerializeField] private GameObject escPanel;

    [Header("Target")]
    [SerializeField] private GameObject targetFrame;
    [SerializeField] private GameObject targetName;
    [SerializeField] private GameObject targetIcon;
    [SerializeField] private GameObject targetPanel;

    [Header("Covement frames")]
    public Sprite airFrame;
    public Sprite waterFrame;
    public Sprite fireFrame;
    public Sprite earthFrame;
    public Sprite rpAirFrame;
    public Sprite rpWaterFrame;
    public Sprite rpFireFrame;
    public Sprite rpEarthFrame;

    [Header("Item Read")]
    public GameObject popUpItem;
    public GameObject popUpText;


    public bool inChat;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /// <summary>
    /// Copies the chats into list to be used during the game
    /// </summary>
    void CopyChats()
    {

    }

    
    public void AbilityClicked()
    {
        if(player.GetComponent<ThirdPersonMovement>().hasTarget)
        {
            string oldText = warningOBJ.GetComponent<TextMeshProUGUI>().text;
            warningOBJ.GetComponent<TextMeshProUGUI>().text = friendlyTargetMessage;
            warningOBJ.GetComponent<TextMeshProUGUI>().text += "\n";
            warningOBJ.GetComponent<TextMeshProUGUI>().text += oldText;
            warningOBJ.GetComponent<WarningController>().StartTimer();
        }
        else
        {
            string oldText = warningOBJ.GetComponent<TextMeshProUGUI>().text;
            warningOBJ.GetComponent<TextMeshProUGUI>().text = noTargetMessage;
            warningOBJ.GetComponent<TextMeshProUGUI>().text += "\n" ;
            warningOBJ.GetComponent<TextMeshProUGUI>().text += oldText;
            warningOBJ.GetComponent<WarningController>().StartTimer();
        }
    }


    public void UpdateTarget(NPC _newTarget)
    {

        switch(_newTarget.affiliation)
        {
            case Affiliation.Air:
                targetPanel.GetComponent<Image>().sprite = airFrame;
                break;
            case Affiliation.Earth:
                targetPanel.GetComponent<Image>().sprite = earthFrame;
                break;
            case Affiliation.Fire:
                targetPanel.GetComponent<Image>().sprite = fireFrame;
                break;
            case Affiliation.Water:
                targetPanel.GetComponent<Image>().sprite = waterFrame;
                break;
        }
        targetIcon.GetComponent<Image>().sprite = _newTarget.npcImage;
        targetName.GetComponent<TextMeshProUGUI>().text = _newTarget.npcName;
        targetFrame.SetActive(true);
        player.GetComponent<ThirdPersonMovement>().hasTarget = true;
    }

    public void NoTarget()
    {
        targetFrame.SetActive(false);
        player.GetComponent<ThirdPersonMovement>().hasTarget = false;
    }

    public void ChatStartButton()
    {
        inChat = true;
        optionsPanel.SetActive(true);
        for (int i = 0; i < optionsPanel.transform.childCount; i++)
        {
            optionsPanel.transform.GetChild(i).gameObject.SetActive(true);
        }
    }


    public void CloseChat()
    {
        inChat = false;
        optionsPanel.SetActive(false);
    }

    public void AddAwnser(char _letter)
    {
        playerAwnser.GetComponent<TextMeshProUGUI>().text += _letter;
        
    }


    public bool CheckAwnserSize()
    {
        if(playerAwnser.GetComponent<TextMeshProUGUI>().text.Length> awnserMaxSize)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AwnserClearOne()
    {
        string oldAwnser = playerAwnser.GetComponent<TextMeshProUGUI>().text;
        oldAwnser=oldAwnser.Remove(0, 1);
        playerAwnser.GetComponent<TextMeshProUGUI>().text = oldAwnser;


    }

    public void ClearAwnser()
    {
        playerAwnser.GetComponent<TextMeshProUGUI>().text = "";
    }

    public void OpenRPPanel(NPC _npc)
    {
        switch (_npc.affiliation)
        {
            case Affiliation.Air:
                rpPanel.GetComponent<Image>().sprite = rpAirFrame;
                break;
            case Affiliation.Earth:
                rpPanel.GetComponent<Image>().sprite = rpEarthFrame;
                break;
            case Affiliation.Fire:
                rpPanel.GetComponent<Image>().sprite = rpFireFrame;
                break;
            case Affiliation.Water:
                rpPanel.GetComponent<Image>().sprite = rpWaterFrame;
                break;
        }

        rpPanel.GetComponent<RpPanelControll>().nameobj.GetComponent<TextMeshProUGUI>().text = _npc.rpName;
        rpPanel.GetComponent<RpPanelControll>().eyeColorObj.GetComponent<TextMeshProUGUI>().text = _npc.eyeColour;
        rpPanel.GetComponent<RpPanelControll>().affiliationObj.GetComponent<TextMeshProUGUI>().text = _npc.affiliation.ToString();
        rpPanel.GetComponent<RpPanelControll>().descriptionObl.GetComponent<TextMeshProUGUI>().text = _npc.description;


        rpPanel.SetActive(true);
    }

    public void CloseRPPanel()
    {
        rpPanel.SetActive(false);
    }

    public void ShowToolTip(NPC _npc)
    {
        if(!tooltipPanel.activeInHierarchy || tooltipPanel.transform.GetChild(0).GetComponent< TextMeshProUGUI>().text!= _npc.tooltipInfo)
        {
            tooltipPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _npc.tooltipInfo;
            tooltipPanel.SetActive(true);

        }
            
    }
    
    public void CloseToolTip()
    {
        if(tooltipPanel.activeInHierarchy)
        {
            tooltipPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
            tooltipPanel.SetActive(false);
        }
    }


    public void OpenInventory()
    {
        if(!inventoryOBJ.activeInHierarchy)
        {
            inventoryOBJ.SetActive(true);
        }
        else
        {
            inventoryOBJ.SetActive(false);
        }
        
    }

    public void CloseInventory()
    {
        inventoryOBJ.SetActive(false);
    }


    public void OpenCharacter()
    {
        if(!characterOBJ.activeInHierarchy)
        {
            characterOBJ.SetActive(true);
        }
        else
        {
            characterOBJ.SetActive(false);
        }
        
    }

    public void CloseCharacter()
    {
        characterOBJ.SetActive(false);
    }


    public void OpenEscPanel()
    {
        
        if(escPanel.activeInHierarchy)
        {
            escPanel.SetActive(false);
        }
        else
        {
            escPanel.SetActive(true);
            Debug.Log("open panel");
        }
    }

    public bool CheckOpenPanels()
    {
        if (inventoryOBJ.activeInHierarchy)
        {
            return false;
        }

        if(characterOBJ.activeInHierarchy)
        {
            return false;
        }

        if(rpPanel.activeInHierarchy)
        {
            return false;
        }    

        if(targetFrame.activeInHierarchy)
        {
            return false;
        }

        return true;
    }

    public void CloseEscPanel()
    {
        escPanel.SetActive(false);
    }


    public void ExitGame()
    {
        Application.Quit();
    }
   

    public void InteractedWithItem(string _text)
    {
        popUpItem.SetActive(true);
        popUpText.GetComponent<TextMeshProUGUI>().text = _text;
    }

    public void ClosePopUp()
    {
        popUpItem.SetActive(false);
    }
}
