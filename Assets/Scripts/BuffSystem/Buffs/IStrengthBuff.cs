using UnityEngine;

public interface IStrengthBuff 
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void AddStrengthLevel(int level);
    public void RemoveStrengthLevel(int level);
}
