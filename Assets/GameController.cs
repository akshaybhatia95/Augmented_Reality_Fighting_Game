using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class GameController : MonoBehaviour {
    public static GameController instance;
    public static bool allowMovement=false;
    public GameObject flashButton;
    public GameObject cameraButton;
    public GameObject playerScoreOnScreen;
    public GameObject enemyScoreOnScreen;
    public GameObject backButton;
    public GameObject forwardButton;
    public GameObject punchButton;
    public GameObject kickButton;
    private bool played321 = false;
    public AudioClip[] audioclip;
    AudioSource audioSource;
    public static int playerScore = 0;
    public static int enemyScore = 0;
    public GameObject[] points;
    public static int round = 0;

    // Use this for initialization
    void Awake()
    {
        if (instance == null) {
            instance = this;
        }
        
    }
    void Start () {
        audioSource = GetComponent<AudioSource>();
	}

    void playAudio(int clip) {
        audioSource.clip = audioclip[clip];
        audioSource.Play();
    }

    public void scorePlayer()
    {
        playerScore++;

    }

    public void scoreEnemy()
    {
        enemyScore++;
    }


    // Update is called once per frame
    void Update () {
        if (played321 == false)
        {
           
                played321 = true;
                StartCoroutine(round1());
            
            
        }
	}

    IEnumerator round1() {

        yield return new WaitForSeconds(0f);
        playAudio(0);
        StartCoroutine(prepareyourself());
    }
    IEnumerator prepareyourself()
    {
        yield return new WaitForSeconds(1.2f);
        playAudio(1);
        StartCoroutine(start321());
    }
    IEnumerator start321() {
        yield return new WaitForSeconds(2f);
        playAudio(2);
        StartCoroutine(allowPlayerMovement());
    }
    IEnumerator allowPlayerMovement() {
        yield return new WaitForSeconds(5f);
        allowMovement = true;
    }

    public void OnScreenPoints() {
        if (playerScore == 1)
        {
            points[0].SetActive(true);
        }
        else if (playerScore == 2) {
            points[1].SetActive(true);
        }

        if (enemyScore == 1)
        {
            points[2].SetActive(true);
        }
        else if (enemyScore == 2)
        {
            points[3].SetActive(true);
        }
    }

    public void rounds() {
        round = playerScore + enemyScore;
        if (round == 1) {
            playAudio(3);
        }
        if (round == 2 && playerScore == 1 && enemyScore == 1) {
            playAudio(4);
        }
    }
}
