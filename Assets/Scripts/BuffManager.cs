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
}
