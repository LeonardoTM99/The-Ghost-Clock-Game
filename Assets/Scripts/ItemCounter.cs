using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemCounter : MonoBehaviour
{
    public int itemCount = 0;
    public static ItemCounter Instance;

    private bool victorySceneLoaded = false;

    private void Awake()
    {
        // Ensure that there's only one instance of ItemCounter
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

    // Update is called once per frame
    void Update()
    {
        if (itemCount == 8 && !victorySceneLoaded)
        {
            victorySceneLoaded = true;
            SceneManager.LoadScene("Victory");
        }
    }
}
