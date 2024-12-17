using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    [SerializeField] Text countdownText;

    void Update()
    {
        float remainingTime = CountdownManager.Instance.GetRemainingTime();

        if (remainingTime <= 10)
        {
            countdownText.color = Color.red;
        }
        if (remainingTime == 0)
        {
            SceneManager.LoadScene("GameOver");
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}