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
    [SerializeField] GameObject computerPrefab;
    [SerializeField] Transform computerSpawn;
    
    private GameObject disk;
    private GameObject player;
    private GameObject computer;

    bool respawningDisk;

    void Start()
    {
        Restart();
        respawningDisk = false;
    }

    void OnEnable()
    {
        respawningDisk = false;
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

    IEnumerator DiskCountdown()
    {
        if(respawningDisk)
        {
            yield break;
        }

        respawningDisk = true;
        diskCountdownText.gameObject.SetActive(true);
        for(int i = 3; i > 0; i--)
        {
            diskCountdownText.text = i.ToString();
            yield return new WaitForSeconds(1.0f);
        }
        
        diskCountdownText.gameObject.SetActive(false);

        disk = Instantiate(diskPrefab, diskSpawn);

        respawningDisk = false;
    }

    public void Goal()
    {
        Destroy(disk);
        StartCoroutine(DiskCountdown());
    }

    public void DestroyElements()
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

}
