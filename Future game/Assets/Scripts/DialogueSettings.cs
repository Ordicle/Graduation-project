﻿using System.IO;
using System.Xml.Serialization;
using UnityEngine;



[XmlRoot("dialogue")]
public class DialogueSettings {

    [XmlElement("node")]
   public Node[] node;

    public static DialogueSettings Load(TextAsset textAsset)
    {

        XmlSerializer xmlSerializationReader = new XmlSerializer(typeof(DialogueSettings));
        StringReader reader = new StringReader(textAsset.text);
        DialogueSettings dialogueSettings = xmlSerializationReader.Deserialize(reader) as DialogueSettings;
        return dialogueSettings;

    }
	

}

[System.Serializable]
public class Node
{
    [XmlElement("text")]
    public string text_dialogue;
}