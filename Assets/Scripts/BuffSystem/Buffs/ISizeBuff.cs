using UnityEngine;

public interface ISizeBuff 
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void AddSizeLevel(int level);
    public void RemoveSizeLevel(int level);
}
