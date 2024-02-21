using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    // Public static variables can be accessed from any script but not from the editor
    public static bool gameOver, won;
    public static int score;

    // Set in inspector
    public TMP_Text textbox;
    public int scoreToWin;
    
    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        won = false;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver) { textbox.text = "Score: " + score; }

        if (score >= scoreToWin)
        {
            won = true;
            gameOver = true;
        }

        if (gameOver)
        {
            textbox.text = (won == true) ? "You win!\nPress R to try again." : "You lose...\nPress R to try again.";
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
