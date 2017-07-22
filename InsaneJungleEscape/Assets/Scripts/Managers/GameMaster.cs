using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {

    private static GameMaster instance;
    //UI variables
#pragma warning disable 649
    [Header("UI Elements")]
    [SerializeField]
	private Text scoreTxt;
    [SerializeField]
	private Text VRScoreTxt;
    [SerializeField]
    private Text gameOverTxt;
    [SerializeField]
    private Text regularTitleTxt;
    [SerializeField]
    private GameObject regUIInstructionsPanel;
    [SerializeField]
    private GameObject regPlayButton;
    [SerializeField]
    private GameObject regPlayAgainButton;
    [SerializeField]
    private Canvas regularCanvas;
    [SerializeField]
	private Canvas VRCanvas;
    [SerializeField]
	private Text VRGameOverTxt;
    [SerializeField]
    private Text VRTitleTxt;
    [SerializeField]
    private GameObject VRUIInstructionsPanel;
    [SerializeField]
    private GameObject VRPlayButton;
    [SerializeField]
    private GameObject VRPlayAgainButton;
    [Header("Win and Lose Conditions")]
    [SerializeField]
    private int scoreForVictory = 25;
    [SerializeField]
    private int monkeysMissedForLoss = 10;

    private int numOfMnksPassed = 0;
#pragma warning restore 649
    private int currentScore;

    private bool isFirstGameStarted = false;
    private bool isGameOver;
    private bool isVictory;

    /// <summary>
    /// instance of Gamemaster
    /// </summary>
    public static GameMaster Instance
    {
        get
        {
            if (instance == null)
            {
                if (GameObject.FindObjectOfType(typeof(GameMaster)) != null)
                {
                    instance = GameObject.FindObjectOfType(typeof(GameMaster)) as GameMaster;

                }
                else
                {
                    GameObject temp = new GameObject();
                    temp.name = "~GameMaster";
                    temp.AddComponent<GameMaster>();
                    temp.tag = "GameMaster";
                    temp.isStatic = true;
                    DontDestroyOnLoad(temp);
                    instance = temp.GetComponent<GameMaster>();

                }
            }
            return instance;
        }
    }

    public static void init()
    {
        if (GameObject.FindObjectOfType(typeof(GameMaster)) != null)
        {
            instance = GameObject.FindObjectOfType(typeof(GameMaster)) as GameMaster;

        }
        else
        {
            if (instance == null)
            {
                GameObject temp = new GameObject();
                temp.name = "~GameMaster";
                temp.AddComponent<GameMaster>();
                temp.tag = "GameMaster";
                temp.isStatic = true;
                DontDestroyOnLoad(temp);
                instance = temp.GetComponent<GameMaster>();
            }
        }
    }

    /// <summary>
    /// Updates the score UI
    /// </summary>
	public void UpdateScore() {
		currentScore++;
		scoreTxt.text =  "" + currentScore;
		VRScoreTxt.text = "" + currentScore;
		if (currentScore >= scoreForVictory) {
			GameOver(true);
		}
	}

    /// <summary>
    /// Handles game over logic and displays proper message to play
    /// </summary>
    /// <param name="didPlayerWin">Whether or not player won</param>
    public void GameOver(bool didPlayerWin)
    {
        isGameOver = true;
        isVictory = didPlayerWin;
        string finalTxt = (isVictory) ? "You won!" : "Game Over!";
        if (GvrViewer.Instance.VRModeEnabled)
        {
            Debug.Log("Game is over. I am showing the VR Canvas");
            VRCanvas.enabled = true;
            VRGameOverTxt.text = finalTxt;
            VRGameOverTxt.enabled = true;
            VRPlayAgainButton.SetActive(true);
        }
        else
        {
            regularCanvas.enabled = true;
            gameOverTxt.text = finalTxt;
            gameOverTxt.enabled = true;
            regPlayAgainButton.SetActive(true);
        }
    }

    /// <summary>
    /// Enable proper canvas items and if game is over call gameover function
    /// </summary>
    public void RefreshUI()
    {
        
        if (GvrViewer.Instance.VRModeEnabled)
        {
            VRCanvas.enabled = true;
            regularCanvas.enabled = false;
            if (!isFirstGameStarted)
            {
                VRTitleTxt.enabled = true;
                VRUIInstructionsPanel.SetActive(true);
                VRPlayButton.SetActive(true);
            }
        }
        else
        {
            VRCanvas.enabled = false;
            regularCanvas.enabled = true;
            if (!isFirstGameStarted)
            {
                regularTitleTxt.enabled = true;
                regUIInstructionsPanel.SetActive(true);
                regPlayButton.SetActive(true);
            }

        }
        if (isGameOver)
        {
            GameOver(isVictory);
        }
    }

    /// <summary>
    /// Sets variables for playing a fresh game upon app opening
    /// </summary>
    public void PlayGame()
    {

        VRTitleTxt.enabled = false;
        VRUIInstructionsPanel.SetActive(false);
        VRPlayButton.SetActive(false);
        VRScoreTxt.enabled = true;


        regularTitleTxt.enabled = false;
        regUIInstructionsPanel.SetActive(false);
        regPlayButton.SetActive(false);


        isFirstGameStarted = true;

        ResetGame();
    }

    /// <summary>
    /// Resets a new game
    /// </summary>
    public void ResetGame()
    {
        // Reset the interface
        if (isGameOver)
        {

            VRCanvas.enabled = false;
            VRGameOverTxt.enabled = false;
            VRPlayAgainButton.SetActive(false);

            regularCanvas.enabled = false;
            gameOverTxt.enabled = false;
            regPlayButton.SetActive(false);

            isGameOver = false;
        }

        currentScore = 0;
        numOfMnksPassed = 0;
        scoreTxt.text = "--";
        VRScoreTxt.text = "--";

        // Remove any remaining game objects
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

        GameObject[] bananas = GameObject.FindGameObjectsWithTag("Banana");
        foreach (GameObject banana in bananas)
        {
            Destroy(banana);
        }
    }
	

    /// <summary>
    /// Gets wheter or not game is over
    /// </summary>
    /// <value>Whether or not game is over</value>
    public bool getIsGameOver
    {
        get { return isGameOver; }
    }

    /// <summary>
    /// Gets the number of monkeys that must pass the player before lose conditions are met
    /// </summary>
    /// <value>the number of monkeys that must pass the player before lose conditions are met</value>
    public int MissedMnksReqLoss
    {
        get { return monkeysMissedForLoss; }
    }

    /// <summary>
    /// Gets/sets the number of monkeys that have passed the player
    /// </summary>
    /// <value>The number of monkeys that have passed the player</value>
    public int MnksPassed
    {
        get { return numOfMnksPassed; }
        set { numOfMnksPassed = value; }
    }

    /// <summary>
    /// Gets whether or not the first game was started
    /// </summary>
    /// <value>Whether or not the first game was started</value>
    public bool didFirstGameStart
    {
        get { return isFirstGameStarted; }
    }

    private void Start()
    {
       //Screen.SetResolution(600, 400, true);
    }
}
