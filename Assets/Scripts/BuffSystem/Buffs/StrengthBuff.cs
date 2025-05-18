using UnityEngine;

public class StrengthBuff : Buff
{
    protected int index;

    public StrengthBuff(int input)
    {
        buffName = "Strenth Buff";
        index = input;
    }

    public override void Apply(GameObject go)
    {
        CharacterAttributes attributes = new CharacterAttributes();
        attributes.strength += index;

        go.GetComponent<CharacterStats>().ChangeAttribute(
            attributes
            );

        //TODO: implement
        IStrengthBuff controller = go.GetComponent<Hero>() as IStrengthBuff;
        if (controller != null)
        {
            controller.AddStrengthLevel(index);
        }
    }

    public override void Remove(GameObject go)
    {
        CharacterAttributes attributes = new CharacterAttributes();
        attributes.strength -= index;

        go.GetComponent<CharacterStats>().ChangeAttribute(
            attributes
            );
        //TODO: implement
        IStrengthBuff controller = go.GetComponent<Hero>() as IStrengthBuff;
        if (controller != null)
        {
            controller.RemoveStrengthLevel(index);
        }
    }
}
