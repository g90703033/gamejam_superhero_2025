using UnityEngine;

public class Score : MonoBehaviour
{
    //取得 UI Manager
    public UIManager uiManager;

    // 加累積值 以及 扣累積值
    public int plusValue = 50;
    public int minusValue = -50;

    //不同物件的 Tag 
    public string humanTag = "Human";
    public string rockTag = "Rock";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == humanTag){
            uiManager.PowerValueOperation(plusValue);
        }
        else if(other.gameObject.tag == rockTag)
        {
            uiManager.PowerValueOperation(minusValue);
        }
    }
}
