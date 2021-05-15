using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatBubbleController : MonoBehaviour
{
    public float activeTimer;
    private float innerActiveTimer;
    public GameObject textBubble;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(innerActiveTimer>=0)
        {
            innerActiveTimer -= Time.deltaTime;
            
        }
        else
        {
            gameObject.SetActive(false);
            textBubble.GetComponent<TextMeshProUGUI>().text = "";
        }
    }

    public void StartTimer()
    {
        innerActiveTimer = activeTimer;
        gameObject.SetActive(true);
    }

    public void SetChat(string _newText)
    {

        textBubble.GetComponent<TextMeshProUGUI>().text = _newText;
    }
}
