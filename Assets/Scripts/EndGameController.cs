using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndGameController : MonoBehaviour
{
    [SerializeField] float maxTime;
    [SerializeField] int maxPoints;
    [SerializeField] int maxLifes;
    [SerializeField] IntVariable pointsVariable;
    [SerializeField] IntVariable lifesVariable;
    [SerializeField] FloatVariable gameTimeVariable;
    [SerializeField] GameObjectVariable activeDiskVariable;
    [SerializeField] float countdownAudioStart;
    [SerializeField] AudioSource countdownAudio;

    [SerializeField] UnityEvent winGameEvent;
    [SerializeField] UnityEvent looseGameEvent;

    float lastUpdateTime;
    bool playing;
    bool endGame;
    Rigidbody disk;
    GameObject activeDisk;

    void Start()
    {
        RestartVariables();
        countdownAudio.Stop();
        lastUpdateTime = Time.time;
    }

    void Update()
    {
        if(activeDisk == null || ! GameObject.ReferenceEquals(activeDisk, activeDiskVariable.value))
        {
            playing = false;
            activeDisk = activeDiskVariable.value;

            if(activeDisk != null)
            {
                disk = activeDisk.GetComponent<Rigidbody>();
            }
        }

        if(disk != null && disk.velocity.sqrMagnitude > 0.0f && ! endGame)
        {
            playing = true;
        }   

        if(playing && ! endGame)
        {
            gameTimeVariable.value -= Time.time - lastUpdateTime;
            if(gameTimeVariable.value <= countdownAudioStart && ! countdownAudio.isPlaying)
                countdownAudio.Play();
        }
        else if(countdownAudio.isPlaying)
        {
            countdownAudio.Pause();
        }

        checkForGameEnd();

        lastUpdateTime = Time.time;
    }

    void checkForGameEnd()
    {
        if(pointsVariable.value > maxPoints)
        {
            endGame = true;
            winGameEvent.Invoke();
        }
        else if(gameTimeVariable.value <= 0 || lifesVariable.value == 0)
        {
            endGame = true;
            looseGameEvent.Invoke();
        }
        
    }

    public void RestartVariables()
    {
        pointsVariable.value = 0;
        lifesVariable.value = maxLifes;
        gameTimeVariable.value = maxTime;
        endGame = false;
    }

}
