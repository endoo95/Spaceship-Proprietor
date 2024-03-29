﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorsSpawnScript : MonoBehaviour
{
    [SerializeField] private GameObject[] meteorPrefab;
    [SerializeField] private Transform playerPosition;

    [SerializeField] private float timeBetwenMeteors = 1.5f;

    private int spawnSide;
    private int spawnOffsetX = 15;
    private int spawnOffsetY = 10;

    private Vector2 spawnPoint;
    private Vector2 meteorPosition;

    private void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.Find("Ship").transform;
        StartCoroutine(Spawner());
    }

    IEnumerator Spawner()
    {
        while (true)
        {
            //Check if player is still alive, and you have meteor prefab.
            if (playerPosition == null || meteorPrefab == null)
            {
                Destroy(gameObject);
                break;
            }

            //Do spawning.
            SpawnMeteor();
            yield return new WaitForSeconds(timeBetwenMeteors);
        }
    }

    void SpawnMeteor()
    {
        GetSpawnSide();
        //Debug.Log("Side: " + spawnSide);
        CalculateOffset(spawnSide);
        //Debug.Log("Offset: " + spawnPoint);

        meteorPosition.x = playerPosition.position.x + spawnPoint.x;
        meteorPosition.y = playerPosition.position.y + spawnPoint.y;
        //Debug.Log("Meteor Position: " + meteorPosition);

        int random;
        random = Random.Range(0, meteorPrefab.Length);

        Vector2 vector;
        vector = new Vector2(meteorPosition.x, meteorPosition.y);

        var newMeteor = Instantiate(meteorPrefab[random], vector, Quaternion.identity);
        newMeteor.transform.parent = gameObject.transform;
    }

    void GetSpawnSide()
    {
        spawnSide = Random.Range(1, 9);
    }

    void CalculateOffset(int side)
    {
        switch (side)
        {
            case 1:
                spawnPoint = new Vector2(0, spawnOffsetY);
                break;
            case 2:
                spawnPoint = new Vector2(spawnOffsetX, spawnOffsetY);
                break;
            case 3:
                spawnPoint = new Vector2(spawnOffsetX, 0);
                break;
            case 4:
                spawnPoint = new Vector2(spawnOffsetX, -spawnOffsetY);
                break;
            case 5:
                spawnPoint = new Vector2(0, -spawnOffsetY);
                break;
            case 6:
                spawnPoint = new Vector2(-spawnOffsetX, -spawnOffsetY);
                break;
            case 7:
                spawnPoint = new Vector2(-spawnOffsetX, 0);
                break;
            case 8:
                spawnPoint = new Vector2(-spawnOffsetX, spawnOffsetY);
                break;
            case 9: //Nie wiedzieć czemu random robi o jeden mniej. To jest w razie co xD
                spawnPoint = new Vector2(-spawnOffsetX, spawnOffsetY);
                break;
        }
    }
}
