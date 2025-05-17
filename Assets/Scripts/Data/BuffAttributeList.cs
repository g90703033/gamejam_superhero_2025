using NUnit.Framework;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewBuffAttributeList", menuName = "Buff System/Buff Attribute List")]
[System.Serializable]
public class BuffAttributeList:ScriptableObject
{
    public List<BuffAttribute> attributes = new List<BuffAttribute>();
}
