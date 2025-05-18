using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class GameLoop : MonoBehaviour
{
    public UnityEvent<bool> OnGameComplete;

    public float maxPlayDuration = 180;
    private bool isPlaying = false;

    private float timer = 0.0f;

    public bool IsWin()
    {
        return GameState.Instance.IsWin(); 
    }

    public bool IsGameComplete()
    {
        return timer > maxPlayDuration;
    }

    public void Init()
    {
        timer = 0.0f;
    }

    public void Play()
    {
        Time.timeScale = 1.0f;
    }

    public void Pause()
    {
        Time.timeScale = 0.0f;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (IsGameComplete() )
        {
            OnGameComplete.Invoke(IsWin());
        }
    }
}
