using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [Header("GameManager")]
    [SerializeField] TextMeshProUGUI diskCountdownText;
    [SerializeField] Canvas guideUI;
    [SerializeField] Transform diskSpawn;
    [SerializeField] GameObject diskPrefab;
    [SerializeField] Transform playerSpawn;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject computerPrefab;
    [SerializeField] Transform computerSpawn;
    [SerializeField] AudioSource restartAudio;
    [SerializeField] UnityEvent startGameEvent;

    [Header("EndGame")]
    [SerializeField] float maxTime;
    [SerializeField] int maxPoints;
    [SerializeField] int maxLifes;
    [SerializeField] IntVariable pointsVariable;
    [SerializeField] IntVariable lifesVariable;
    [SerializeField] FloatVariable gameTimeVariable;
    [SerializeField] float countdownAudioStart;
    [SerializeField] AudioSource countdownAudio;
    [SerializeField] UnityEvent winGameEvent;
    [SerializeField] UnityEvent looseGameEvent;
    
    private GameObject disk;
    private Rigidbody diskRigidbody;
    private GameObject player;
    private GameObject computer;
    private float lastUpdateTime;
    private bool playing;
    bool respawningDisk = false;

    void Update()
    {
        playing = (disk != null && diskRigidbody.velocity.sqrMagnitude > 0.0f);

        if(playing)
        {
            gameTimeVariable.value -= Time.time - lastUpdateTime;
            if(gameTimeVariable.value <= countdownAudioStart && ! countdownAudio.isPlaying)
                countdownAudio.Play();
        }
        else if(countdownAudio.isPlaying)
        {
            countdownAudio.Pause();
        }

        CheckForGameEnd();

        lastUpdateTime = Time.time;
    }

    public void StartGame()
    {
        StopAllCoroutines();
        startGameEvent.Invoke();
        StartCoroutine(StartGameCoroutine());
    }

    public void Restart()
    {
        DestroyElements();
        
        player = Instantiate(playerPrefab, playerSpawn.position, playerSpawn.rotation);
        computer = Instantiate(computerPrefab, computerSpawn.position, computerSpawn.rotation);
        
        player.transform.parent = this.transform;
        computer.transform.parent = this.transform;

        StartCoroutine(DiskCountdown());
    }

    public void Goal()
    {
        Destroy(disk);

        if (!CheckForGameEnd())
            StartCoroutine(DiskCountdown());
    }

    private bool CheckForGameEnd()
    {
        bool isEndGame = false;

        if(pointsVariable.value >= maxPoints)
        {
            EndGame();
            winGameEvent.Invoke();
            isEndGame = true;
        }
        else if(gameTimeVariable.value <= 0 || lifesVariable.value == 0)
        {
            EndGame();
            looseGameEvent.Invoke();
            isEndGame = true;
        }

        return isEndGame;
    }

    private void EndGame()
    {
        DestroyElements();
        RestartVariables();
    }

    private void DestroyElements()
    {
        GameObject[] elements = {player, computer, disk};

        foreach(GameObject go in elements)
        {
            if(go != null)
            {
                Destroy(go);
            }
        }
    }

    private void RestartVariables()
    {
        countdownAudio.Stop();
        lastUpdateTime = Time.time;
        pointsVariable.value = 0;
        lifesVariable.value = maxLifes;
        gameTimeVariable.value = maxTime;
    }

    #region Coroutines
    IEnumerator DiskCountdown()
    {
        if(respawningDisk)
        {
            yield break;
        }

        respawningDisk = true;
        diskCountdownText.gameObject.transform.parent.gameObject.SetActive(true);

        if(restartAudio != null)
        {
            restartAudio.Play();
        }
        
        for(int i = 3; i > 0; i--)
        {
            diskCountdownText.text = i.ToString();
            yield return new WaitForSeconds(1.0f);
        }
        
        diskCountdownText.gameObject.transform.parent.gameObject.SetActive(false);

        disk = Instantiate(diskPrefab, diskSpawn);
        diskRigidbody = disk.GetComponent<Rigidbody>();

        respawningDisk = false;
    }

    IEnumerator StartGameCoroutine()
    {
        guideUI.gameObject.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        guideUI.gameObject.SetActive(false);

        RestartVariables();
        Restart();
    }
    #endregion

}
