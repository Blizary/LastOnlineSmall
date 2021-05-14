﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChatType
{
    chatPublic,
    chatPrivate,
    chatSay
}

[CreateAssetMenu(fileName = "NewConv",menuName ="ScriptableObj/ Conversation", order =1)]
public class ChatConv : ScriptableObject
{
    public ChatType chatType;
    public List<ChatText> conversation;

}
