using UnityEngine;

public class WeightLeftingBuff1 : Buff
{
    protected int index;

    public WeightLeftingBuff1(int input)
    {
        buffName = "Weight Lifting Buff";
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
