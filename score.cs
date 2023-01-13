using TMPro;
using UnityEngine;

public class score : MonoBehaviour
{
    public static score instance;
    public TextMeshProUGUI text;
    public TextMeshProUGUI[] texts;
    public int collectedScore = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
      

    }

    void FixedUpdate()
    {

        texts = FindObjectsOfType<TextMeshProUGUI>();
        // change index in case of error
        text = texts[texts.Length - 1];
        text.text = collectedScore.ToString();
    }

    // function that change score if player collected a coin
    public void ChangeScore(int coinValue)
    {
        collectedScore += coinValue;
        text.text = collectedScore.ToString();
    }

    public void ResetScore()
    {
        collectedScore = 0;
        text.text = 0.ToString();
    }
}
