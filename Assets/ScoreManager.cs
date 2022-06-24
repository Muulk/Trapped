using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ScoreManager : MonoBehaviour
{
    private float timeBetweenScore, startTimeScore;
    private bool oneShot;
    private int score;
    public Text scoreText;
    public GameObject endScoreText;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
        timeBetweenScore = 0f;
        startTimeScore = 2f;
    }

    void OnSceneLoaded(Scene aScene, LoadSceneMode aMode)
    {
        if (aScene.name == "EndGame")
        {
            endScoreText = GameObject.Find("ScoreText");
            endScoreText.GetComponent<Text>().text = "Score: " + score.ToString();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (timeBetweenScore <= 0)
        {
            score++;
            timeBetweenScore = startTimeScore;
            Debug.Log(score);
        } else
        {
            timeBetweenScore -= Time.deltaTime;
        }

        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();

        }
    }
}
