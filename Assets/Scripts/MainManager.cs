using NUnit.Framework.Internal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class MainManager : MonoBehaviour
{
    public static MainManager instance;
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;
    //public string playerName;
   //public string playerScore;
    public Text ScoreText;
    public Text BestScoreText;
    public GameObject GameOverText;

    
    private bool m_Started = false;
    private int m_Points;
    private int brickCount;
    private bool m_GameOver = false;


    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance.level == 0)
        {
            GameManager.instance.level = 1;//greater if we've cleared all the bricks once.
        }
        else
        {
            int.TryParse(GameManager.instance.playerScore, out int score);
            m_Points = score;
            ScoreText.text = $"Level : {GameManager.instance.level} Score : {m_Points}";
        }
        //Best Score : Name: 0
        //  string logMessage = string.Format("Player health is now: {0} and the player's name is: {1}", playerHealth, gameObject.name);
        BestScoreText.text = string.Format("Best Score : {0}: {1}", GameManager.instance.highScoreName, GameManager.instance.highScore);
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        brickCount = 0;
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
                brickCount++;
            }
        }
    }


    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                Debug.Log("Starting for: " + GameManager.instance.playerName);
                //turn off Level and message.
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
            
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(0);//back to menu, only if stopped game
            }
        }else if (brickCount == 0)
        {
            GameManager.instance.level++;
            SceneManager.LoadScene(1);
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Level : {GameManager.instance.level} Score : {m_Points}";
        GameManager.instance.playerScore = m_Points.ToString();
        brickCount--;
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        if (GameManager.instance.CheckScore())
        {
            BestScoreText.text = string.Format("Best Score : {0}: {1}", GameManager.instance.playerName, GameManager.instance.playerScore);
            GameManager.instance.SaveScore();
        }
        else
        {
            GameManager.instance.ClearScore();
        }
        
    }
}
