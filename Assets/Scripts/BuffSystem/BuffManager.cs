using UnityEngine;

public class BuffManager : MonoBehaviour
{
    public static BuffManager Instance { get; private set; }

    public BuffAttributeList buffList;

    public string dataFileName = "NewBuffAttributeList";

    private void Awake()
    {
        // Singleton setup
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadBuffList();
    }

    private void LoadBuffList()
    {
        // Load the ScriptableObject from Resources
        buffList = Resources.Load<BuffAttributeList>(dataFileName);

        if (buffList == null)
        {
            Debug.LogError("BuffAttributeList asset not found in Resources folder!");
        }
        else
        {
            Debug.Log("Buff list loaded successfully.");
        }

        Debug.Log("item= " + buffList.attributes[0].title);
    }

    public BuffAttribute GetAttribute(string title)
    {
        for(int i = 0;i < buffList.attributes.Count;i++)
        {
            if (title.Equals(buffList.attributes[i].title))
            {
                return buffList.attributes[i];
            }
        }

        return null;
    }

    //Choose Three Attribute For UI Manager
    public List<BuffAttribute> GetThreeRandomAttribute()
    {
        List<BuffAttribute> temporaryBufferList = new List<BuffAttribute>();
        for(int i = 0;i < buffList.attributes.Count;i++){
            temporaryBufferList.Add(buffList.attributes[i]);
        }
        List<BuffAttribute> outputBufferList = new List<BuffAttribute>(); 
        for (int i = 0; i < 3; i++){
            int index = Random.Range(0, temporaryBufferList.Count - 1);
            outputBufferList.Add(temporaryBufferList[index]);
            temporaryBufferList.RemoveAt(index);
        }

        return outputBufferList;
    }
}
