using UnityEngine;
using TMPro; 

public class ShardUI : MonoBehaviour
{
    public TextMeshProUGUI shardText;

    void Start()
    {
        GameManager.Instance.shardUI = this; // automatically set refernece when scene loads
        UpdateText(GameManager.Instance.shardsCollected);
    }

    public void UpdateText(int count)
    {
        shardText.text = "Shards: " + count + " / 2";
    }
}