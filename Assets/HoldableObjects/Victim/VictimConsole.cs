using UnityEngine;
using WildWind.ObjectPool;

public class VictimConsole : MonoBehaviour
{
    public ObjectPoolSystem victimPool;
    public float radius;
    public Vector3 offset;

    public float gap;
    private float lastCreatedTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastCreatedTime > gap)
        {
            GameObject victimObj = victimPool.GetPooledInstance(null, false);

            victimObj.transform.position = transform.position + offset + Random.insideUnitSphere * radius;

            victimObj.SetActive(true);

            lastCreatedTime = Time.time;
         }
    }
}
