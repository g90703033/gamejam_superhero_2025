using UnityEngine;

public class BuffReceiver : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        BuffCarry carry = other.GetComponent<BuffCarry>();
        if (carry != null)
        {
            GetComponent<CharacterStats>().ApplyBuff(carry.attribute);
        }
    }
}
