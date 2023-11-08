using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2019.Bird
{
    public class Spawner : MonoBehaviour
    {
        public GameObject pfObject;
        public Transform[] spawnPoints;
        public float minFrequency, maxFrequency;
        public LayerMask layerMask;
        public int amount;

        private int count = 0;

        private void Start()
        {
            Invoke("Spawn", Random.Range(minFrequency, maxFrequency));
        }

        public void Spawn()
        {
            count++;

            int posID = Random.Range(0, spawnPoints.Length);    
            while (GameManager.instance.CheckPos(posID))
                posID = Random.Range(0, spawnPoints.Length);

            Vector2 pos = spawnPoints[posID].position;

            Instantiate(pfObject, pos, Quaternion.identity, transform)
                .GetComponent<Spawnable>().posID = posID;

            GameManager.instance.OccupyPos(posID);

            if (amount <= 0 || count < amount)
                Invoke("Spawn", Random.Range(minFrequency, maxFrequency));
        }
    }
}