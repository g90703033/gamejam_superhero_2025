using UnityEngine;

public interface ISpeedBuff 
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void AddSpeedLevel(int level);
    public void RemoveSpeedLevel(int level);
}
