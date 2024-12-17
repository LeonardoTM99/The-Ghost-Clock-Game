using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    [SerializeField] private GameObject player;
    
    [SerializeField] private LevelConnection connection;

    [SerializeField] private string targetSceneName;

    [SerializeField] private Transform spawnPoint;

    private void Start()
    {
        if (connection == LevelConnection.ActiveConnection)
        {
            player.transform.position = spawnPoint.position;
        }
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LevelConnection.ActiveConnection = connection;
            SceneManager.LoadScene(targetSceneName);
        }
    }

}
