using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndGameController : MonoBehaviour
{
    [SerializeField] Rigidbody disk;

    [SerializeField] float maxTime;
    [SerializeField] int maxPoints;
    [SerializeField] IntVariable pointsVariable;
    [SerializeField] IntVariable lifesVariable;

    [SerializeField] UnityEvent winGameEvent;
    [SerializeField] UnityEvent looseGameEvent;

    float gameTime;
    float lastUpdateTime;

    bool playing;

    void Start()
    {
        gameTime = 0f;
        lastUpdateTime = Time.time;
    }

    void Update()
    {
        if(disk != null && disk.velocity.sqrMagnitude > 0.0f)
        {
            playing = true;
        }   

        if(playing)
        {
            gameTime += Time.time - lastUpdateTime;
        }

        checkForGameEnd();

        lastUpdateTime = Time.time;

        print(playing.ToString()+" "+gameTime.ToString());
    }

    void checkForGameEnd()
    {
        if(pointsVariable.value > maxPoints)
        {
            winGameEvent.Invoke();
        }
        else if(gameTime > maxTime || lifesVariable.value == 0)
        {
            looseGameEvent.Invoke();
        }
        
    }

}
