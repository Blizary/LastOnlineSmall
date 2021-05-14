using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewOption", menuName = "ScriptableObj/ ChatOption", order = 3)]
public class ChatOption : ScriptableObject
{
    public string feelingName;
    //public ChatType chatType;
    public List<string> playerAwnser;
    public ChatText npcAnwser;
    [Header("In case of triggering an event")]
    public bool triggersEvents;
    public IngameEvent nextEvent;
}
