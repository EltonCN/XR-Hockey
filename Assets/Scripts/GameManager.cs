using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI diskCountdownText;
    [SerializeField] Transform diskSpawn;
    [SerializeField] GameObject diskPrefab;
    [SerializeField] Transform playerSpawn;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] Transform computerSpawn;
    [SerializeField] int maxLifes;
    [SerializeField] IntVariable pointsVariable;
    [SerializeField] IntVariable lifesVariable;
    private GameObject computerPrefab;
    private GameObject disk;
    private GameObject player;
    private GameObject computer;

    void Awake()
    {
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
        diskCountdownText.gameObject.SetActive(true);
        for(int i = 3; i > 0; i--)
        {
            diskCountdownText.text = i.ToString();
            yield return new WaitForSeconds(1.0f);
        }
        
        diskCountdownText.gameObject.SetActive(false);

        disk = Instantiate(diskPrefab, diskSpawn);
    }

    public void Goal()
    {
        Destroy(disk);
        StartCoroutine(DiskCountdown());
    }


}
