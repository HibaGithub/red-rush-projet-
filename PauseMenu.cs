using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;
    public static bool isGamePaused = false;
    public GameObject pauseMenu;
    public int level;

    void Awake()
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))

        {
            if (isGamePaused)
            {
                Resume();
            }
            else
            {
                //Debug.Log("paused");
                Paused();
            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        //Time.timeScale = 1f;
        isGamePaused = false;
    }

    public void Paused()
    {
        pauseMenu.SetActive(true);
        //Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void saveLevel()
    {
        //BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/gameDatabase.txt";
        //FileStream stream = new FileStream(path, FileMode.Create);

        

        //formatter.Serialize(stream, data);

        

        using (StringWriter writer = new StringWriter(path))
        {
            PlayerData data = new PlayerData(score.instance, playerMovement.player);
            string json = JsonUtility().ToJson(PlayerData);
            writer.Write(json);
        }
        //stream.Close();
    }

    public GameObject [] allCollectedCoin;
    public void loadLevel()
    {
        string path = Application.persistentDataPath + "/gameDatabase.txt";

        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string dataToLoad = reader.ReadToEnd();
                data = JsonUtility.FromJson<Data>(dataToLoad);
            }

            p
            // open database file and deserialize it
            //BinaryFormatter formatter = new BinaryFormatter();
            //FileStream stream = new FileStream(path, FileMode.Open);

            //PlayerData data = (PlayerData)formatter.Deserialize(stream);

            // load last level
            Checkpoint.instance.level = data.Levels;
            SceneManager.LoadScene(Checkpoint.instance.level);

            if (SceneManager.GetActiveScene().isLoaded)
            {
                // load coin that didn't get picked up
                allCollectedCoin = new GameObject[data.hasCoin.Count];
                for (int i = 0; i < data.hasCoin.Count; i++)
                {
                    allCollectedCoin[i] = GameObject.FindGameObjectWithTag("reward").gameObject.transform.GetChild(data.hasCoin[i]).gameObject;
                    Debug.Log(allCollectedCoin[i]);
                    reward.rewardSystem.loadedCoin.Add(allCollectedCoin[i]);
                }

                //Debug.Log(playerMovement.player.loadedCoin.Count);

                //foreach( GameObject coin in allCollectedCoin)
                //{
                //    playerMovement.player.collectedCoin.Add(coin);

                //}

                //get score of last saved game
                score.instance.collectedScore = data.score;
                //get player position of last saved game
                playerMovement.player.body.transform.position = new Vector2(data.position[0], data.position[1]);

                //Debug.Log(data.score);
                //Debug.Log(data.position[0]);
                //Debug.Log(data.position[1]);
                //Debug.Log(playerMovement.player.body.transform.position);
            }


            

            stream.Close();
            pauseMenu.SetActive(false);
            //Time.timeScale = 1f;
            isGamePaused = false;

        }
        else
        {
            Debug.LogError("file path does not exists");
        }
    }

}

[System.Serializable]
public class PlayerData
{
    //public int levels;
    public int score;
    public float[] position;
    public List<int> hasCoin;
    public int Levels;
 
    public PlayerData(score scoreState, playerMovement playerState)
    {

        position = new float[2];
        position[0] = playerState.transform.position.x;
        position[1] = playerState.transform.position.y;
        score = scoreState.collectedScore;
        hasCoin = playerState.collectedCoin;
        Levels = playerState.currentLevel;
    }
}