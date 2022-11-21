using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject ui;
    [SerializeField] private TextMeshProUGUI diskCountdownText;
    [SerializeField] private Transform ballSpawn;
    [SerializeField] private Transform playerSpawn;
    [SerializeField] private Transform computerSpawn;
    [SerializeField] private int maxLifes;
    [SerializeField] IntVariable pointsVariable;
    [SerializeField] IntVariable lifesVariable;

    private GameObject diskPrefab;
    private GameObject playerPrefab;
    private GameObject computerPrefab;
    private GameObject disk;
    private GameObject player;
    private GameObject computer;

    void Awake()
    {
        diskPrefab = Resources.Load("Disk") as GameObject;
        playerPrefab = Resources.Load("Player") as GameObject;
        computerPrefab = Resources.Load("Computer") as GameObject;
    }

    void Start()
    {
        Restart();
    }

    public void Restart()
    {
        Destroy(player);
        Destroy(computer);
        Destroy(disk);

        pointsVariable.value = 0;
        lifesVariable.value = maxLifes;
        
        player = Instantiate(playerPrefab, playerSpawn.position, playerSpawn.rotation);
        computer = Instantiate(computerPrefab, computerSpawn.position, computerSpawn.rotation);

        StartCoroutine(DiskCountdown());
    }

    IEnumerator DiskCountdown()
    {
        ui.SetActive(true);
        for(int i = 3; i > 0; i--)
        {
            diskCountdownText.text = i.ToString();
            yield return new WaitForSeconds(1.0f);
        }
        
        ui.SetActive(false);
        disk = Instantiate(diskPrefab, ballSpawn);
    }

    public void Goal(bool isGoalPlayer)
    {
        if (isGoalPlayer)
        {
            pointsVariable.value++;
        }
        else
        {
            lifesVariable.value--;
        }

        StartCoroutine(DiskCountdown());
    }


}
