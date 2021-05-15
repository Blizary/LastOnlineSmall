using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum IngameEvent
{
    RpEvent,
    RpInterract,
    FriendLogOff,
}
public class DoorsManager : MonoBehaviour
{

    [Header("Settings")]
    [SerializeField] private bool holdToType;
    public ChatBoxManager chatManager;
    public ChatConv rpConvo;

    public UnityEvent rpStart;
    public UnityEvent rpInterract;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartEvent(IngameEvent _event)
    {
        switch(_event)
        {
            case IngameEvent.RpEvent:
                Debug.Log("Rp event started");
                rpStart.Invoke();
                chatManager.AddChat(rpConvo);
                break;
            case IngameEvent.RpInterract:
                Debug.Log("Rp interact started");
                rpInterract.Invoke();
                break;
            case IngameEvent.FriendLogOff:
                Debug.Log("Friend log off event started");
                break;

        }
    }

    public bool HoldToType()
    {
        return holdToType;
    }
}
