using System.Collections;
using UnityEngine;
using WildWind.ObjectPool;

public class VictimConsole : MonoBehaviour
{
    public ObjectPoolSystem victimPool;
    public Vector3 createZone;
    public Vector2 createGapBegin;
    public Vector2 createGapFinal;
    public Vector2 createAmountBegin;
    public Vector2 createAmountFinal;
    public float createDelay = 0.2f;

    public AnimationCurve easingCurve;

    private float gap;
    public float maxGameLength;
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
            //To Do: progress should be replace
            float progress = Time.time - maxGameLength;
            CreateVictims(progress);

            //Update Gap
            Vector2 createGap = Vector2.Lerp(createGapBegin, createGapFinal, progress);
            gap = Random.Range(createGap.x, createGap.y);
            lastCreatedTime = Time.time;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;

        // Draw a wire cube to represent the create zone
        Vector3 center = transform.position;
        Vector3 size = createZone; 

        Gizmos.DrawWireCube(center, size);
    }


    public float GetRandomRangeCoef()
    {
        return Random.Range(-0.5f, 0.5f);
    }

    public void CreateVictims(float progress)
    {
        //Random a pos
        Vector3 pos = new Vector3(GetRandomRangeCoef() * createZone.x, GetRandomRangeCoef() * createZone.y, GetRandomRangeCoef() * createZone.z);

        //Get random amount
        Vector2 createAmount = Vector2.Lerp(createAmountBegin, createAmountFinal, progress);

        int amount = Mathf.RoundToInt(Random.Range(createAmount.x, createAmount.y));

        StartCoroutine(CreateVictimProcess(transform.position + pos, amount));
    }

    public void CreateVictimAtPosition(Vector3 pos)
    {
        GameObject victimObj = victimPool.GetPooledInstance(null, false);

        victimObj.transform.position = pos;
        victimObj.transform.localRotation = Random.rotation;

        HoldableObject holdableObject = victimObj.GetComponent<HoldableObject>();
        holdableObject.Init();

        victimObj.SetActive(true);
    }

    IEnumerator CreateVictimProcess(Vector3 pos, int amount)
    {
        WaitForSeconds delay = new WaitForSeconds(createDelay);
        for (int i = 0; i < amount; i++)
        {
            CreateVictimAtPosition(pos);
            yield return delay;
         }
     }
}
