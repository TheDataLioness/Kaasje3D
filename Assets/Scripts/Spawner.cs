using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace DefaultNamespace
{
    public class Spawner : MonoBehaviour
    {
        public GameObject[] GameObjects;

        private GameObject lastSpawned = null;

        private int currentSpawnX = 20;

        private void Start()
        {
            Random random = new Random();
            int amount = GameObjects.Length;
            for (int i = 0; i < 5; i++)
            {
                int key = random.Next(0, amount);
                GameObject tileObject = GameObjects[key];
                GameObject spawnObject = Instantiate(tileObject, new Vector3(currentSpawnX, 0,0), new Quaternion(0,0,0,0));
                lastSpawned = spawnObject;
                currentSpawnX += 20;
            }
           
        }

        private void Update()
        {
            PlayerController playerPos = PlayerController.getInstance();
            if (Vector3.Distance(playerPos.transform.position, lastSpawned.transform.position) < 100)
            {
                Random random = new Random();
                int amount = GameObjects.Length;
                int key = random.Next(0, amount);
                GameObject tileObject = GameObjects[key];
                GameObject spawnObject = Instantiate(tileObject, new Vector3(currentSpawnX, 0,0), new Quaternion(0,0,0,0));
                lastSpawned = spawnObject;
                currentSpawnX += 20;
            }
        }
    }
}