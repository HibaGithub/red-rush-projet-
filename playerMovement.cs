using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class playerMovement : MonoBehaviour
{
    public static playerMovement player;
    [SerializeField] float speed = 6;
    [SerializeField] private float jump_height = 4;
    public Rigidbody2D body;
    private bool in_ground;
    private Vector2 checkpoint;
    public GameObject underGround;
    public int heart = 5;
    public bool respawn = false;
    public GameObject[] coin;
    public List<int> collectedCoin;
    public int currentLevel;
    public float knockBack = 10f;

    //public int gameScore;

    void Awake()
    {
        if(player == null)
        {
            player = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }

        // get player and affected it to variable
        body = GetComponent<Rigidbody2D>();

        underGround = GameObject.FindGameObjectWithTag("underGround");

        // player checkpoint position
        checkpoint = transform.position;

        coin = GameObject.FindGameObjectsWithTag("coin");

        

    }

    // Update is called once per frame
    void Update()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(currentLevel);

        //control player movement by left and right by adding -1 or 1
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);

        if (Input.GetKey(KeyCode.Space) && in_ground)
        {
            jump();
        }

        if (heart == 0)
        {
            respawn = true;
            respawnAfterDie();
            body.constraints = RigidbodyConstraints2D.None;
        }

        underGround.transform.position = new Vector2(transform.position.x, underGround.transform.position.y);

        //gameScore = score.instance.collectedScore;
        //Debug.Log(gameScore);
    }

    private void jump()
    {
        body.velocity = new Vector2(body.velocity.x, jump_height);
        in_ground = false;
    }

    // check if player is dead
    private void respawnAfterDie()
    {
        if (respawn)
        {
            transform.position = checkpoint;
            respawn = false;
            heart = 5;
            // inherted from class health
            health.healthSystem.HealthState(heart);
            reward.rewardSystem.RestCoin(coin);
        }
    }



    // collision check if there is interction between two object
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // check if player in touch with ground
        if (collision.gameObject.tag == "Ground")
        {
            in_ground = true;
        }

        // check if player has fallen underground
        if (collision.gameObject.tag == "underGround")
        {
            if (heart >= 1)
            {
                transform.position = checkpoint;
                heart--;
                health.healthSystem.HealthState(heart);
            }
            else
            {
                Debug.Log("you died");
            }

        }
    }

    // check if there is a in intraction between objects
    private void OnTriggerEnter2D(Collider2D other)
    {
        // remove collected coin
        if (other.gameObject.tag == "coin")
        {
            other.gameObject.SetActive(false);
            collectedCoin.Add(other.gameObject.transform.GetSiblingIndex());
            
        }

        if (other.gameObject.CompareTag("enemy_type1"))
        {
            heart--;
            health.healthSystem.HealthState(heart);
        }
    }
}