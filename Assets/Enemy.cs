using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject Player;
    public Transform player;
    private NavMeshAgent naveMesh;
    private float PlayerDistance, DistanceFromPoint;
    public float DistanceOfView = 20, DistanceOfFollowing = 15, DistanceOfAttacking = 5, SpeedOfWalking = 8, SpeedOfChasing = 6, TimePerAttack = 1.5f, Damage = 20;
    public bool SeesPlayer;
    public Transform[] RandomPoints;
    private int Point;
    private bool ChasingSomething, counterChasingSomething, attackingSomething;
    private float chasingCronometer, attackCronometer;

    //El enemigo se esconde, desvía de obstaculos, sigue al jugador, ataca, se pasea aleatoriamente por el escenario, etc, etc.
    void Start()
    {
        Point = Random.Range(0, RandomPoints.Length);
        naveMesh = transform.GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        PlayerDistance = Vector3.Distance(player.transform.position, transform.position);
        DistanceFromPoint = Vector3.Distance(RandomPoints[Point].transform.position, transform.position);
        //============================== RAYCAST ===================================//
        RaycastHit hit;
        Vector3 fromWhere = transform.position;
        Vector3 toWhere = player.transform.position;
        Vector3 direction = toWhere - fromWhere;
        if (Physics.Raycast(transform.position, direction, out hit, 1000) && PlayerDistance < DistanceOfView)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                SeesPlayer = true;
            }
            else
            {
                SeesPlayer = false;
            }
        }
        //================ CHECHAGENS E DECISOES DO INIMIGO ================//
        if (PlayerDistance > DistanceOfView)
        {
            Walk();
        }
        if (PlayerDistance <= DistanceOfView && PlayerDistance > DistanceOfFollowing)
        {
            if (SeesPlayer == true)
            {
                Looks();
            }
            else
            {
                Walk();
            }
        }
        if (PlayerDistance <= DistanceOfFollowing && PlayerDistance > DistanceOfAttacking)
        {
            if (SeesPlayer == true)
            {
                Chase();
                ChasingSomething = true;
            }
            else
            {
                Walk();
            }
        }
        if (PlayerDistance <= DistanceOfAttacking)
        {
            Attack();
        }
        //COMANDOS DE PASSEAR
        if (DistanceFromPoint <= 10)
        {
            Point = Random.Range(0, RandomPoints.Length);
            Walk();
        }
        //CONTADORES DE PERSEGUICAO
        if (counterChasingSomething == true)
        {
            chasingCronometer += Time.deltaTime;
        }
        if (chasingCronometer >= 5 && SeesPlayer == false)
        {
            counterChasingSomething = false;
            chasingCronometer = 0;
            ChasingSomething = false;
        }
        // CONTADOR DE ATAQUE
        if (attackingSomething == true)
        {
            attackCronometer += Time.deltaTime;
        }
        if (attackCronometer >= TimePerAttack && PlayerDistance <= DistanceOfAttacking)
        {
            attackingSomething = true;
            attackCronometer = 0;
            Player.GetComponent<PLAYER>().life -= Damage;
            Debug.Log("received attack");
        }
        else if (attackCronometer >= TimePerAttack && PlayerDistance > DistanceOfAttacking)
        {
            attackingSomething = false;
            attackCronometer = 0;
            Debug.Log("failed");
        }
    }
    void Walk()
    {
        if (ChasingSomething == false)
        {
            naveMesh.acceleration = 5;
            naveMesh.speed = SpeedOfWalking;
            naveMesh.destination = RandomPoints[Point].position;
        }
        else if (ChasingSomething == true)
        {
            counterChasingSomething = true;
        }
    }
    void Looks()
    {
        naveMesh.speed = 0;
        transform.LookAt(player);
    }
    void Chase()
    {
        naveMesh.acceleration = 8;
        naveMesh.speed = SpeedOfChasing;
        naveMesh.destination = player.position;
    }
    void Attack()
    {
        attackingSomething = true;

    }
}


//# si un te ha visto todos los personajes te van a perseguir
//# que no se choquen, que van a distinctos lugares