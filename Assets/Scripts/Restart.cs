using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Restart : MonoBehaviour
{

    [SerializeField] Button restart;

    // Start is called before the first frame update
    void Start()
    {
        restart.onClick.AddListener(RestartGame);
    }

    void RestartGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

    

}
