using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public CharacterAttributes baseAttributes = new CharacterAttributes();
    private CharacterAttributes buffedAttributes = new CharacterAttributes();

    private CharacterAttributes activeBuff = null;

    void Start()
    {
        CopyAttributes(baseAttributes, buffedAttributes);
    }

    public CharacterAttributes GetCurrentStats()
    {
        return buffedAttributes;
    }


    private List<Buff> activeBuffs = new List<Buff>();

    public void ApplyBuff(Buff buff)
    {
        buff.Apply(gameObject);
        activeBuffs.Add(buff);
    }

    public void RemoveBuff(Buff buff)
    {
        if (activeBuffs.Contains(buff))
        {
            buff.Remove(gameObject);
            activeBuffs.Remove(buff);
        }
    }

    public void RemoveAllBuffs()
    {
        foreach (var buff in activeBuffs)
        {
            buff.Remove(gameObject);
        }
        activeBuffs.Clear();
    }

    private void CopyAttributes(CharacterAttributes source, CharacterAttributes target)
    {
        target.weightLifting = source.weightLifting;
        target.moveSpeed = source.moveSpeed;
        target.strength = source.strength;
        target.size = source.size;
    }
}
