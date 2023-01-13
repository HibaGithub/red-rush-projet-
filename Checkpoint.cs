using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    public static Checkpoint instance;
    public int level;
    public Animator transition;

    // Dont't forget to instance checkpoint GameObject in unity with crossfade animation

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PauseMenu.instance.saveLevel();
            LoadNextLevel();
            playerMovement.player.body.transform.position = new Vector2(-7f, -1.15f);
        }
    }

    public void LoadNextLevel()
    {
        level = SceneManager.GetActiveScene().buildIndex + 1;
        StartCoroutine(LoadAnimation(level));
    }

    public IEnumerator LoadAnimation(int levelIndex)
    {
        transition.SetTrigger("start");

        yield return new WaitForSeconds(1 / 2);

        SceneManager.LoadScene(levelIndex);
    }
}
