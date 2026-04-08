using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int shardsCollected = 0;
    public int shardsRequiredPerLevel = 2;

    public ShardUI shardUI;
    public GameObject completionText;
    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // persists between levels
        }
        else
        {
            Destroy(gameObject); // destroy duplicate if scene reloads
        }
    }

    public void CollectShard()
    {
        shardsCollected++;
        Debug.Log("Shards: " + shardsCollected);
        shardUI.UpdateText(shardsCollected);

        if (shardsCollected >= shardsRequiredPerLevel)
        {
            LevelComplete();
        }
    }

    void LevelComplete()
    {
        Debug.Log("Game Win");
        completionText.SetActive(true);
        //shardsCollected = 0; // reset for next level
        // load next scene:
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}