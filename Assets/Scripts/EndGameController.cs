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


    [SerializeField] UnityEvent winGameEvent;
    [SerializeField] UnityEvent looseGameEvent;

    float lastUpdateTime;

    bool playing;

    Rigidbody disk;

    GameObject activeDisk;

    void Start()
    {
        RestartVariables();
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

        if(disk != null && disk.velocity.sqrMagnitude > 0.0f)
        {
            playing = true;
        }   

        if(playing)
        {
            gameTimeVariable.value -= Time.time - lastUpdateTime;
        }

        checkForGameEnd();

        lastUpdateTime = Time.time;
    }

    void checkForGameEnd()
    {
        if(pointsVariable.value > maxPoints)
        {
            winGameEvent.Invoke();
        }
        else if(gameTimeVariable.value <= 0 || lifesVariable.value == 0)
        {
            looseGameEvent.Invoke();
        }
        
    }

    public void RestartVariables()
    {
        pointsVariable.value = 0;
        lifesVariable.value = maxLifes;
        gameTimeVariable.value = maxTime;
    }

}
