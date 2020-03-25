using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemigo : MonoBehaviour
{
    public GameObject Player;
    public GameObject enemy;
    public float spawnTime = 30;
    public Transform[] spawnPoints;
    public GameObject spawnedEnemy;
    public float[] randomSpeeds;

    void Start()
    {
        //Para chamar a funcao depois do time de spawn e continuar a chamar a funcao 
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    void Spawn()
    {
        StartCoroutine(BehaviourSpawn());
    }

    IEnumerator BehaviourSpawn()
    {
        // Encontrar um ponto aleatorio entre todos os pontos 
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        int speedIndex = Random.Range(0, randomSpeeds.Length);

        if (Player.GetComponent<PLAYER>().life == 100)
        {
            // Criar una instancia da preface do inimigo no ponto aleatorio.
            spawnedEnemy = Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
            spawnedEnemy.transform.position = transform.position * speedIndex;
            yield return new WaitForSeconds(15);
            spawnTime = spawnTime * 2;
        }
    }
}
