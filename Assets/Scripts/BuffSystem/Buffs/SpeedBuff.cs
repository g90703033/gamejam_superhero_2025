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
        ISpeedBuff controller = go.GetComponent<PlayerController2>() as ISpeedBuff;
        if (controller != null)
        {
            controller.AddSpeedLevel(index);
        }
    }

    public override void Remove(GameObject go)
    {
        ISpeedBuff controller = go.GetComponent<PlayerController2>() as ISpeedBuff;
        if (controller != null)
        {
            controller.RemoveSpeedLevel(index);
        }
    }
}
