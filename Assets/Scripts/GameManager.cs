using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    StageGenerator stageGenerator;

    public float roundLength = 5f;
    float prevTime;

    public Slider timeSlider;

    int player1Score = 0;
    int player2Score = 0;

    public GameObject[] player1images;
    public GameObject[] player2images;

    bool gameActive = true;

    public GameObject endScreen;
    public TextMeshProUGUI endText;

    void Start()
    {
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

        if (gameActive)
        {
            //DEBUG - REMOVE FOR FINAL BUILD
            if (Input.GetKeyDown(KeyCode.V))
            {
                pluckedby1();
            }
            if (Input.GetKeyDown(KeyCode.B))
            {
                pluckedby2();
            }
        }
    }

    void winnerPicked(int winner) {
        if (winner == 1)
        {
            endText.text = "Player 1 wins!";
        }
        else {
            endText.text = "Player 2 wins!";
        }
        Time.timeScale = 0;
        endScreen.SetActive(true);
        gameActive = false;
    }

    public void pluckedby1() {
        ++player1Score;
        for (int i = 0; i < player1Score; ++i)
        {
            player1images[i].SetActive(true);
        }
        if (player1Score == 5) {
            winnerPicked(1);
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
    }
}
