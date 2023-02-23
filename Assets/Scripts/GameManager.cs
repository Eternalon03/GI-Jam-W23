using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    StageGenerator stageGenerator;

    public float roundLength = 5f;
    public float prevTime;

    public Slider timeSlider;

    int player1Score = 0;
    int player2Score = 0;

    public GameObject[] player1images;
    public GameObject[] player2images;

    //bool gameActive = true;

    public GameObject endScreen;
    public TextMeshProUGUI endText;

    AudioManager am;
    void Start()
    {
        am = FindObjectOfType<AudioManager>();

        endScreen.SetActive(false);

        prevTime = Time.time;
        stageGenerator = FindObjectOfType<StageGenerator>();
        timeSlider.maxValue = roundLength;
        timeSlider.value = roundLength;

        foreach (GameObject g in player1images) {
            g.SetActive(false);
        }

        foreach (GameObject g in player2images)
        {
            g.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time - prevTime >= roundLength) {
            stageGenerator.cycle();
            prevTime = Time.time;
        }

        timeSlider.value = roundLength - (Time.time - prevTime);
    }

    void winnerPicked(int winner) {
        FindObjectOfType<AudioManager>().Stop("theme");
        
        if (winner == 1)
        {
            FindObjectOfType<AudioManager>().winner = "Player One";
            FindObjectOfType<AudioManager>().loser = "Player Two";
        }
        else {
            FindObjectOfType<AudioManager>().winner = "Player Two";
            FindObjectOfType<AudioManager>().loser = "Player One";
        }
        //Time.timeScale = 0;
        //endScreen.SetActive(true);
        //gameActive = false;
        SceneManager.LoadScene("EndCutscene");
    }

    public void pluckedby1() {
        ++player1Score;
        for (int i = 0; i < player1Score; ++i)
        {
            player1images[i].SetActive(true);
        }
        if (player1Score == 5)
        {
            winnerPicked(1);
        }
        else {
            FindObjectOfType<AudioManager>().Play("round win");
        }
    }
    
    public void pluckedby2() {
        ++player2Score;
        for (int i = 0; i < player2Score; ++i)
        {
            player2images[i].SetActive(true);
        }
        if (player2Score == 5)
        {
            winnerPicked(2);
        }
        else {
            FindObjectOfType<AudioManager>().Play("round win");
        }
    }

    public void startGame()
    {
        FindObjectOfType<AudioManager>().Play("press");
        Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene");
    }

    public void BackToMenu() {
        FindObjectOfType<AudioManager>().Play("press");
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
}

