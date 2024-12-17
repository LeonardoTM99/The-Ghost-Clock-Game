using UnityEngine;

public class CountdownManager : MonoBehaviour
{
    public static CountdownManager Instance { get; private set; }

    [SerializeField] private float remainingTime;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else if (remainingTime < 0)
        {
            remainingTime = 0;
        }
    }

    public float GetRemainingTime()
    {
        return remainingTime;
    }

    public void SetRemainingTime(float time)
    {
        remainingTime = time;
    }
}