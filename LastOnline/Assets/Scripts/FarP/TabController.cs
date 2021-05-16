using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{
    public Sprite defaultSprite;
    public Sprite selectedSprite;

    public bool inMSN;
    private ChatBoxManager chatManager;
    private MsnManager msnManager;
    public int tabnum;
    public string tabname;
    public GameObject blinkIconOBJ;
    public GameObject chatname;
    // Start is called before the first frame update
    void Start()
    {
        chatManager = GameObject.FindGameObjectWithTag("ChatManager").GetComponent<ChatBoxManager>();
        msnManager = GameObject.FindGameObjectWithTag("MsnManager").GetComponent<MsnManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPress()
    {
        if(inMSN)
        {
            msnManager.TabsButton(tabnum);
        }
        else
        {
            chatManager.TabsButton(tabnum);
        }
        
        blinkIconOBJ.SetActive(false);//turn off blink
        this.GetComponent<Image>().sprite = selectedSprite;
    }

    public void NotSelected()
    {
        this.GetComponent<Image>().sprite = defaultSprite;
    }

    public void SomethingNew()
    {
        blinkIconOBJ.SetActive(true);//turn on blink
    }

    public void Selected()
    {
        this.GetComponent<Image>().sprite = selectedSprite;
    }
}
