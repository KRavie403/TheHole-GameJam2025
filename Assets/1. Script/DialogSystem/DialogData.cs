using System;
using System.Collections.Generic;

[Serializable]
public class DialogChoice
{
    public string text;
    public int nextId;
}

[Serializable]
public class DialogNode
{
    public int id;
    public string text;
    public List<DialogChoice> choices;
}

[Serializable]
public class DialogData
{
    public string npcName;
    public List<DialogNode> nodes;
}
