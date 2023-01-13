using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reward : MonoBehaviour
{
    public int coinValue = 1;
    public static reward rewardSystem;
    public List<GameObject> loadedCoin;

    void Awake()
    {
       if (rewardSystem == null)
        {
            rewardSystem = this;
            DontDestroyOnLoad(gameObject);
        }
      
    }

    void Update()
    {
        coinToLoad(loadedCoin);
        Debug.Log(loadedCoin.Count);
    }

    public void coinToLoad(List<GameObject> coins)
    {
        foreach (GameObject coin in coins)
        {
            coin.SetActive(false);
        }
    }

    public void RestCoin(GameObject[] coin)
    {
        
        try
        {
            foreach (GameObject position in coin)
            {
                position.gameObject.SetActive(true);
            }
            score.instance.ResetScore();
        } catch
        {
            Debug.Log("error");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ( other.gameObject.CompareTag("Player"))
        {
            score.instance.ChangeScore(coinValue);
        }
    }
}
