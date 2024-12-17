using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private AudioSource audioSource;
    public float mainSceneVolume = 1.0f;
    public float otherScenesVolume = 0.5f;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string sceneName = scene.name;
        if (sceneName == "MainMenu" || sceneName == "GameOver" || sceneName == "Introduction" || sceneName == "Victory")
        {
            audioSource.Stop();
        }
        else if (sceneName == "Downtown")
        {
            audioSource.volume = mainSceneVolume;
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.volume = otherScenesVolume;
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }
}


