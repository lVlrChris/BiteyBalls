using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField]
    private float startDelay = 3f;
    [SerializeField]
    private float endDelay = 3f;
    [SerializeField]
    public GameObject playerPrefab;
    [SerializeField]
    public PlayerManager[] players;

    private WaitForSeconds startWait;
    private WaitForSeconds endWait;    
    private PlayerManager roundWinner;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        // DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        startWait = new WaitForSeconds(startDelay);
        endWait = new WaitForSeconds(endDelay);

        SpawnAllPlayers();

        StartCoroutine(GameLoop());
    }

    void SpawnAllPlayers()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].instance = Instantiate(playerPrefab, players[i].spawnPoint.position, players[i].spawnPoint.rotation) as GameObject;
            players[i].playerIndex = i + 1;
            players[i].Setup();
        }
    }

    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(RoundStarting());
        yield return StartCoroutine(RoundPlaying());
        yield return StartCoroutine(RoundEnding());

        if (roundWinner != null) 
        {
            // TODO: Async loading
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            StartCoroutine(GameLoop());
        }
    }

    private IEnumerator RoundStarting()
    {
        DisableControl();
        yield return startWait;
    }

    private IEnumerator RoundPlaying()
    {
        EnableControl();
        
        while (!OnePlayerLeft())
        {
            yield return null;
        }
    }

    private IEnumerator RoundEnding()
    {
        // Should we disable controls?
        DisableControl();

        roundWinner = null;
        roundWinner = GetRoundWinner();

        if (roundWinner != null)
        {
            roundWinner.wins++;
        }
        
        yield return endWait;
    }

    private bool OnePlayerLeft()
    {
        int playersLeft = 0;

        foreach (PlayerManager player in players)
        {
            if (player.instance.activeSelf)
                playersLeft++;
        }

        return playersLeft <= 1;
    }

    private PlayerManager GetRoundWinner()
    {
        foreach (PlayerManager player in players)
        {
            if (player.instance.activeSelf)
                return player;
        }

        return null;
    }

    private void DisableControl()
    {
        foreach(PlayerManager player in players)
        {
            player.DisableControl();
        }
    }
    
    private void EnableControl()
    {
        foreach(PlayerManager player in players)
        {
            player.EnableControl();
        }
    }
}
