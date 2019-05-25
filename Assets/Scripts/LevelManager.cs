using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private float startDelay = 3f;
    [SerializeField]
    private float endDelay = 3f;
    [SerializeField]
    public Transform[] spawnpoints;

    private GameManager gameManager;
    private WaitForSeconds startWait;
    private WaitForSeconds endWait;    
    private PlayerManager roundWinner;

    void Start()
    {
        startWait = new WaitForSeconds(startDelay);
        endWait = new WaitForSeconds(endDelay);
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        SpawnAllPlayers();

        StartCoroutine(GameLoop());
    }

    void SpawnAllPlayers()
    {
        for (int i = 0; i < gameManager.players.Length; i++)
        {
            gameManager.players[i].instance = Instantiate(gameManager.players[i].playerPrefab, spawnpoints[i].position, spawnpoints[i].rotation) as GameObject;
            gameManager.players[i].playerIndex = i + 1;
            gameManager.players[i].Setup();
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

        foreach (PlayerManager player in gameManager.players)
        {
            if (player.instance.activeSelf)
                playersLeft++;
        }

        return playersLeft <= 1;
    }

    private PlayerManager GetRoundWinner()
    {
        foreach (PlayerManager player in gameManager.players)
        {
            if (player.instance.activeSelf)
                return player;
        }

        return null;
    }

    private void DisableControl()
    {
        foreach(PlayerManager player in gameManager.players)
        {
            player.DisableControl();
        }
    }
    
    private void EnableControl()
    {
        foreach(PlayerManager player in gameManager.players)
        {
            player.EnableControl();
        }
    }
}
