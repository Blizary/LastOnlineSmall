using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum IngameEvent
{
    RpEvent,
    RpInterract,
    FriendLogOff,
    RosieEnter,
    BlazeOff,
    RosieOff,
}
public class DoorsManager : MonoBehaviour
{

    [Header("Settings")]
    [SerializeField] private bool holdToType;
    public ChatBoxManager chatManager;
    public ChatConv rpConvo;

    public UnityEvent rpStart;
    public UnityEvent rpInterract;
    public UnityEvent rosieEnter;
    public UnityEvent blazeLogsOff;
    public UnityEvent rosieLogsOff;
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
            case IngameEvent.RosieEnter:
                Debug.Log("Rosie enters the scene");
                rosieEnter.Invoke();
                break;
            case IngameEvent.BlazeOff:
                Debug.Log("Blaze logs off");
                blazeLogsOff.Invoke();
                break;
            case IngameEvent.RosieOff:
                Debug.Log("Rosie logs off");
                rosieLogsOff.Invoke();
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
