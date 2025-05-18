using UnityEngine;

public class GameState : MonoBehaviour
{

    public static GameState Instance { get; private set; }
    public GameObject Player1;
    public GameObject Player2;
    public int GetExp = 0;
    public int SuccessExpGoal = 100;

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

    public bool IsWin()
    {
        return GetExp > SuccessExpGoal;
    }

    public void OnP1BuffGet(string title)
    {
        Player1.GetComponent<CharacterStats>().ApplyBuff(title);
    }

    public void OnP2BuffGet(string title)
    {

        Player2.GetComponent<CharacterStats>().ApplyBuff(title);
    }
}
