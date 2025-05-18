using UnityEngine;

public interface IMoveSpeedBuff 
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void AddMoveSpeedLevel(int level);
    public void RemoveMoveSpeedLevel(int level);
}
