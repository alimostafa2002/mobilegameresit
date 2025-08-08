using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading;
using System.Security.Cryptography.X509Certificates;
using System;

[DefaultExecutionOrder(-1)]
public class gameManager : MonoBehaviour
{
    public static gameManager instance { get; private set; }

    public float initialGameSpeed = 5f;
    public float gameSpeedIncrease = 0.1f;
    public float gameSpeed { get; private set; }
    public TextMeshProUGUI gameOverText;
    public Button retryButton;
    public TextMeshProUGUI score;
    public TextMeshProUGUI highscore;

    private float scoree;




    private PlayerMovement player;
    private Spawner spawner;

    

    private void Awake()
    {
        if (instance != null) {
            DestroyImmediate(gameObject);
        } else {
            instance = this;
        }
    }

    private void OnDestroy()
    {
        if (instance == this) {
            instance = null;
        }
    }

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        spawner = FindObjectOfType<Spawner>();

        NewGame();
    }

    public void NewGame()
    {
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();

        foreach (var obstacle in obstacles){

            Destroy(obstacle.gameObject);
        }

        gameSpeed = initialGameSpeed;
        enabled = true;

        player.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);

    }

    public void GameOver()
    {
        gameSpeed = 0f;
        enabled = false;

        player.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);

       

      
    }

    private void Update()
    {
        gameSpeed += gameSpeedIncrease * Time.deltaTime;
        scoree += gameSpeed * Time.deltaTime;
        score.text = Mathf.FloorToInt(scoree).ToString("D5");
    }

    

    

}