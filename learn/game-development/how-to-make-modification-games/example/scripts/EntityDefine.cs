using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[System.Serializable]
[XmlType("Entity")]
public class EntityDefine
{
    [XmlAttribute]
    public string id;

    [XmlElement("Sprite")]
    public string sprite;

    [XmlElement("Script")]
    public string script;
}
