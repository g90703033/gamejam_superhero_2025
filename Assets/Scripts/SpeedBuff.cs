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
        IPlayerController controller = go.GetComponent<PlayerController2>() as IPlayerController;
        if (controller != null)
        {
            controller.AddSpeedLevel(index);
        }
    }

    public override void Remove(GameObject go)
    {
        IPlayerController controller = go.GetComponent<PlayerController2>() as IPlayerController;
        if (controller != null)
        {
            controller.RemoveSpeedLevel(index);
        }
    }
}
