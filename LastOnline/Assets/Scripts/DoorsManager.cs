using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IngameEvent
{
    RpEvent,
    FriendLogOff
}
public class DoorsManager : MonoBehaviour
{

    [Header("Settings")]
    [SerializeField] private bool holdToType;
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
