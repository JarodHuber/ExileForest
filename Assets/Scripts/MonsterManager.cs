using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class MonsterManager : MonoBehaviour
{
    public GameObject monsterFab;

    public Vector2 speedExtremes = new Vector2(1, 5);
    public Vector2 spawnDistExtremes = new Vector2(40, 10);
    public Vector2 chaseLengthExtremes = new Vector2(15, 60);
    public Vector2 aggressionLevel = new Vector2(0, 5);
    public float spawnRadius = 5;
    public float height = 2;

    AudioSource audioSource;
    GameObject player;
    NavMeshAgent agent;
    Timer chaseTimer;
    Timer stallTimer = new Timer(30);
    bool chasing = false;

    GameObject monster;
    Animator anim;

    static int aggressionPlaceholder = 0;
    static int maxAggression = 0;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        audioSource = GetComponent<AudioSource>();
        aggressionPlaceholder = 0;
        maxAggression = (int)aggressionLevel.y;
    }

    private void Update()
    {
        aggressionLevel.x = Mathf.Min(aggressionPlaceholder, aggressionLevel.y);

        if (ValueHolder.GameOver || ValueHolder.GameWin)
            return;

        if (!chasing)
        {
            if (stallTimer.Check())
            {
                if (Random.Range(0.0f,1.0f) <= aggressionLevel.x/aggressionLevel.y)
                {
                    StartChase();
                }
            }
            return;
        }
        agent.destination = player.transform.position;

        if(agent.velocity.magnitude > 0)
            anim.SetInteger("moving", 1);
        else
            anim.SetInteger("moving", 0);

        if (chaseTimer.Check())
        {
            EndChase();
        }
    }

    void StartChase()
    {
        audioSource.Play();
        monster = Instantiate(monsterFab, SetPos(), Quaternion.identity);
        anim = monster.GetComponent<Animator>();
        agent = monster.GetComponent<NavMeshAgent>();

        anim.SetInteger("moving", 0);
        if(aggressionLevel.x*2 >= aggressionLevel.y)
            anim.SetInteger("battle", 1);
        else
            anim.SetInteger("battle", 0);

        agent.destination = player.transform.position;
        agent.speed = LerpValue(speedExtremes);

        chaseTimer = new Timer(LerpValue(chaseLengthExtremes));

        chasing = true;
    }

    void EndChase()
    {
        Destroy(monster);

        chasing = false;
    }


    Vector3 SetPos()
    {
        Vector2 distFromPlayer = Random.insideUnitCircle.normalized * LerpValue(spawnDistExtremes);
        Vector3 positionToSpawn = new Vector3(distFromPlayer.x, 0, distFromPlayer.y);
        NavMeshHit hit;
        int radMod = 0;
        while (!NavMesh.SamplePosition(positionToSpawn, out hit, spawnRadius + radMod, NavMesh.AllAreas)) 
        {
            distFromPlayer = Random.insideUnitCircle.normalized * LerpValue(spawnDistExtremes); 
            positionToSpawn = new Vector3(distFromPlayer.x, 0, distFromPlayer.y);
            radMod += 2;
        }

        return hit.position + new Vector3(0, height / 2, 0);
    }

    float LerpValue(Vector2 valueToLerp)
    {
        return Mathf.Lerp(valueToLerp.x, valueToLerp.y, aggressionLevel.x / aggressionLevel.y);
    }

    public static void IncreaseAggression(bool maxAggressionB = false)
    {
        if (!maxAggressionB)
            aggressionPlaceholder++;
        else
            aggressionPlaceholder = maxAggression;
    }
}
