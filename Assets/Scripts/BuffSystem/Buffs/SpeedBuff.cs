using UnityEngine;

public class SpeedBuff : Buff
{
    protected int index;

    public SpeedBuff(int input)
    {
        buffName = "Speed Buff";
        index = input;
    }

    public override void Apply(GameObject go)
    {
        CharacterAttributes attributes = new CharacterAttributes();
        attributes.moveSpeed += index;

        go.GetComponent<CharacterStats>().ChangeAttribute(
            attributes
            );

        IMoveSpeedBuff controller = go.GetComponent<Hero>() as IMoveSpeedBuff;
        if (controller != null)
        {
            controller.AddMoveSpeedLevel(index);
        }
    }

    public override void Remove(GameObject go)
    {
        CharacterAttributes attributes = new CharacterAttributes();
        attributes.moveSpeed -= index;

        go.GetComponent<CharacterStats>().ChangeAttribute(
            attributes
            );

        IMoveSpeedBuff controller = go.GetComponent<Hero>() as IMoveSpeedBuff;
        if (controller != null)
        {
            controller.RemoveMoveSpeedLevel(index);
        }
    }
}
