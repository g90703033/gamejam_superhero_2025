using UnityEngine;

public class SizeBuff : Buff
{
    protected int index;

    public SizeBuff(int input)
    {
        buffName = "Size Buff";
        index = input;
    }

    public override void Apply(GameObject go)
    {
        CharacterAttributes attributes = new CharacterAttributes();
        attributes.size += index;

        go.GetComponent<CharacterStats>().ChangeAttribute(
            attributes
            );
        //TODO: implement 
    }

    public override void Remove(GameObject go)
    {
        CharacterAttributes attributes = new CharacterAttributes();
        attributes.size -= index;

        go.GetComponent<CharacterStats>().ChangeAttribute(
            attributes
            );
        //TODO: implement
    }
}