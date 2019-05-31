using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private float startDelay = 4f;
    [SerializeField]
    private float endDelay = 3f;
    [SerializeField]
    private int winsForMatch = 3;
    [SerializeField]
    private GameObject countdownText;
    [SerializeField]
    private GameObject[] playerUIs;
    [SerializeField]
    private Transform[] spawnpoints;

    private GameManager gameManager;
    private WaitForSeconds startWait;
    private WaitForSeconds endWait;    
    private PlayerManager roundWinner;
    private PlayerManager matchWinner;

    void Start()
    {
        startWait = new WaitForSeconds(startDelay);
        endWait = new WaitForSeconds(endDelay);
        gameManager = FindObjectOfType<GameManager>();
        SpawnAllPlayers();

        StartCoroutine(GameLoop());
    }

    void SpawnAllPlayers()
    {
        for (int i = 0; i < gameManager.players.Length; i++)
        {
            gameManager.players[i].instance = Instantiate(gameManager.players[i].playerPrefab, spawnpoints[i].position, spawnpoints[i].rotation) as GameObject;
            gameManager.players[i].playerIndex = i + 1;
            gameManager.players[i].Setup(playerUIs[i]);
        }
    }

    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(RoundStarting());
        yield return StartCoroutine(RoundPlaying());
        yield return StartCoroutine(RoundEnding());

        if (matchWinner != null) 
        {
            ResetWins();

            if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
                StartCoroutine(AsyncSceneLoad(SceneManager.GetActiveScene().buildIndex + 1));
            else
                StartCoroutine(AsyncSceneLoad(SceneManager.GetActiveScene().buildIndex));
        }
        else
        {
            StartCoroutine(GameLoop());
        }
    }

    private IEnumerator RoundStarting()
    {
        ResetAllPlayers();
        DisableControl();

        StartCoroutine(Countdown(startDelay, countdownText));
        
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
            roundWinner.wins++;

        matchWinner = null;
        matchWinner = GetMatchWinner();
        
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

    private PlayerManager GetMatchWinner()
    {
        foreach (PlayerManager player in gameManager.players)
        {
            if (player.wins >= winsForMatch)
                return player;
        }

        return null;
    }

    private void ResetAllPlayers()
    {
        foreach (PlayerManager player in gameManager.players)
        {
            player.Reset(spawnpoints[player.playerIndex - 1]);
        }
    }

    private void ResetWins()
    {
        foreach(PlayerManager player in gameManager.players)
        {
            player.wins = 0;
        }
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

    private IEnumerator Countdown(float timer, GameObject text)
    {
        Debug.Assert(text != null, "Countdown text not assigned in inspector.");
        TextMeshProUGUI tmText = text.GetComponent<TextMeshProUGUI>();

        text.SetActive(true);
        while (timer > 1)
        {
            tmText.SetText((timer - 1).ToString());
            timer--;

            yield return new WaitForSeconds(1);
        }
        
        tmText.SetText("Start!");
        yield return new WaitForSeconds(1);
        text.SetActive(false);
    }

    private IEnumerator AsyncSceneLoad(int buildIndex)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(buildIndex);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
