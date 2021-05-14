using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{
    public Sprite defaultSprite;
    public Sprite selectedSprite;

    private ChatBoxManager chatManager;
    public int tabnum;
    public string tabname;
    public GameObject blinkIconOBJ;
    // Start is called before the first frame update
    void Start()
    {
        chatManager = GameObject.FindGameObjectWithTag("ChatManager").GetComponent<ChatBoxManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPress()
    {
        chatManager.TabsButton(tabnum);
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
