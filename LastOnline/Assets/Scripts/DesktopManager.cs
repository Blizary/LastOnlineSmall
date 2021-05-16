using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DesktopManager : MonoBehaviour
{
    public string playerName;
    public int awnserMaxSize;

    public List<ChatConv> chatbox;


    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject playerAwnser;
  


    public bool inChat;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

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
        if (playerAwnser.GetComponent<TextMeshProUGUI>().text.Length > awnserMaxSize)
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
        oldAwnser = oldAwnser.Remove(0, 1);
        playerAwnser.GetComponent<TextMeshProUGUI>().text = oldAwnser;


    }

    public void ClearAwnser()
    {
        playerAwnser.GetComponent<TextMeshProUGUI>().text = "";
    }




    public void ExitGame()
    {
        Application.Quit();
    }


  
}
