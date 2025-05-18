using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public CharacterAttributes baseAttributes = new CharacterAttributes();
    
    //TODO: temp set to public for debug
    public CharacterAttributes buffedAttributes = new CharacterAttributes();

    private CharacterAttributes activeBuff = null;

    void Start()
    {
        CopyAttributes(baseAttributes, buffedAttributes);
    }

    public CharacterAttributes GetCurrentStats()
    {
        return buffedAttributes;
    }

    // 
    public void ChangeAttribute(CharacterAttributes attributes)
    {
        buffedAttributes += attributes;
    }

    private List<Buff> activeBuffs = new List<Buff>();

    // [TODO] temp kind of hard code it. refactor later
    public void ApplyBuff(BuffAttribute buff)
    {
        if (buff.attributes.moveSpeed != 0)
        {
            ApplyBuff(new SpeedBuff(buff.attributes.moveSpeed));
        }
        if (buff.attributes.size != 0)
        {
            ApplyBuff(new SizeBuff(buff.attributes.size));
        }
        if (buff.attributes.strength != 0)
        {
            ApplyBuff(new StrengthBuff(buff.attributes.strength));
        }
        if (buff.attributes.weightLifting != 0)
        {
            ApplyBuff(new WeightLeftingBuff(buff.attributes.weightLifting));
        }
    }

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
