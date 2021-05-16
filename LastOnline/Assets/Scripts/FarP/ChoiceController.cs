using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChoiceController : MonoBehaviour
{
    public ChatOption currentOption;
    public bool hasOptions;

    public bool inMSN;
    private ChatBoxManager chatManager;
    private MsnManager msnManager;

    // Start is called before the first frame update
    void Start()
    {
        chatManager = GameObject.FindGameObjectWithTag("ChatManager").GetComponent<ChatBoxManager>();
        msnManager = GameObject.FindGameObjectWithTag("MsnManager").GetComponent<MsnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(hasOptions)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }    
    }

    public void ChoiceChoosen()
    {
        if(inMSN)
        {
            msnManager.ChoiceMade(currentOption, currentOption.npcAnwser, currentOption.playerAwnser);
        }
        else
        {
            chatManager.ChoiceMade(currentOption, currentOption.npcAnwser, currentOption.playerAwnser);
        }
       
    }

   
    public void SetValues(ChatOption _newOption)
    {
        currentOption = _newOption;
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _newOption.feelingName;
        hasOptions = true;
    }
    

    public void ClearValues()
    {
        currentOption = null;
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        hasOptions = false;
    }
}
