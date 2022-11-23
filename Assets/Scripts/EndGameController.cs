using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndGameController : MonoBehaviour
{
    [SerializeField] float maxTime;
    [SerializeField] int maxPoints;
    [SerializeField] IntVariable pointsVariable;
    [SerializeField] IntVariable lifesVariable;
    [SerializeField] FloatVariable gameTimeVariable;
    [SerializeField] GameObjectVariable activeDiskVariable;


    [SerializeField] UnityEvent winGameEvent;
    [SerializeField] UnityEvent looseGameEvent;

    float gameTime;
    float lastUpdateTime;

    bool playing;

    Rigidbody disk;

    GameObject activeDisk;

    void Start()
    {
        gameTime = maxTime;
        gameTimeVariable.value = gameTime;
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
            gameTime -= Time.time - lastUpdateTime;
        }

        checkForGameEnd();

        gameTimeVariable.value = gameTime;

        lastUpdateTime = Time.time;
    }

    void checkForGameEnd()
    {
        if(pointsVariable.value > maxPoints)
        {
            winGameEvent.Invoke();
        }
        else if(gameTime <= 0 || lifesVariable.value == 0)
        {
            looseGameEvent.Invoke();
        }
        
    }

}
