using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState Instance { get; private set; }

    public Transform groundTransform;
    private void Awake()
    {
        Instance = this;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
