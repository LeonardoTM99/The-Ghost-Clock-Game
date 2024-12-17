using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    [SerializeField] Button start;

    private void Start()
    {
        start.onClick.AddListener(LoadScene);
    }

    void LoadScene()
    {
        SceneManager.LoadScene("Introduction");
    }

}
