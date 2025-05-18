using UnityEngine;

public class WeightLeftingBuff : Buff
{
    protected int index;

    public WeightLeftingBuff(int input)
    {
        buffName = "Weight Lifting Buff";
        index = input;
    }

    public override void Apply(GameObject go)
    {
        CharacterAttributes attributes = new CharacterAttributes();
        attributes.weightLifting += index;

        go.GetComponent<CharacterStats>().ChangeAttribute(
            attributes
            );
        //TODO: implement 
    }

    public override void Remove(GameObject go)
    {
        CharacterAttributes attributes = new CharacterAttributes();
        attributes.weightLifting -= index;

        go.GetComponent<CharacterStats>().ChangeAttribute(
            attributes
            );
        //TODO: implement
    }
}
