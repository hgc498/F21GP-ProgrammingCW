using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(PlayerStats))]
public class EnemyAI : MonoBehaviour
{
    NavMeshAgent navMeshA;
    PlayerStats playerStats;

    public Material[] mat;
    MeshRenderer render;

    public Vector3 position;

    [SerializeField]
    Transform player;

    //Stats 
    public float EnemyHealth = 50f;
    public float attackCooldown = 0f;
    public float attackspeed = 1f;

    //sets the default state to be the wander state 
    public StateTypes stateType = StateTypes.WANDER;

    //add the state types as enums 
    public enum StateTypes
    {
        WANDER,
        CHASE,
        ATTACK,
        DAMAGE,
        DIE
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshA = GetComponent<NavMeshAgent>();
        playerStats = GetComponent<PlayerStats>();

        render = GetComponent<MeshRenderer>();
        render.enabled = true;
        render.sharedMaterial = mat[0];

    }


    void Update()
    {

        //switch case
        switch (stateType)
        {
            case StateTypes.WANDER:
                NPCWander();
                //Debug.Log("In Wander State");
                //if the distance between the player and the enemy is less then the look radius then it will implememnt the chase state and make sure its facing the player
                if (CanSeePlayer() == true)
                {
                    stateType = StateTypes.CHASE;
                }

                break;
            case StateTypes.CHASE:
                ChasePlayer();
                //Debug.Log("In Chase State");
                //if the distance between enemy is less then or equal to the attack radius then it will implement the attack state and make sure its facing the player 
                if (AttackRange() == true)
                {
                    stateType = StateTypes.ATTACK;
                }
                else if (CanSeePlayer() == false)
                {
                    //Change to Wander State if player outside the look radius 
                    stateType = StateTypes.WANDER;
                }else if (Input.GetKeyDown(KeyCode.Q))
                {
                    //chnage to damage state 
                    stateType = StateTypes.DAMAGE;
                }

                break;
            case StateTypes.ATTACK:
                AttackPlayer();
                //Debug.Log("In Attack State");
                //change to chase state 
                if (CanSeePlayer() == true || AttackRange() == false)
                {
                    stateType = StateTypes.CHASE;
                }else if (Input.GetKeyDown(KeyCode.Q) || attackCooldown > 0)
                {
                    // change to Damage State, if the attack cooldown is greater than 0 or if the player inputs Ctrl
                    stateType = StateTypes.DAMAGE;
                }else if (EnemyHealth == 0)
                {
                    //change to die state when the enemies health becomes 0 
                    stateType = StateTypes.DIE;
                }

                break;
            case StateTypes.DAMAGE:
                TakeDamage();
                //Debug.Log("In Damage State");
                //change to attack playe, if the cooldown goes to zero or below then the enemy is ready to attack again
                if (attackCooldown <= 0f)
                {
                    stateType = StateTypes.ATTACK;
                }else if (EnemyHealth == 0)
                {
                    //change to die state 
                    stateType = StateTypes.DIE;
                }

                break;
            case StateTypes.DIE:
                NPCDie();
                //Debug.Log("In Die State");

                break;

            default:
                break;
        }
        
    }
    //method to see if the enemy can see the player
    public bool CanSeePlayer()
    {
        //Calculate the distance between the player and the enemy 
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= 10f)
        {
            //Debug.Log("Distance to Player" + distance);
            return true;
        }else
        {
            return false;
        }
        
    }
    //method to seee if the player is in attack range
    public bool AttackRange()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance <= 4)
        {
            return true;
        }
        else
            return false;

    }
    //add the wander method 
    public void NPCWander()
    {
        //gets the enemy to move between point on the level 
        //keep track of where the enemy has been 
        float x = gameObject.transform.position.x;
        float z = gameObject.transform.position.z;

        //add random ranges of positions 
        float xPosition = x + Random.Range(x - 100, x + 100);
        float zPosition = z + Random.Range(z - 100, z + 100);
        position = new Vector3(xPosition, gameObject.transform.position.y, zPosition);

        //sets the destination to the new vector position
        navMeshA.SetDestination(position);
    }
    //add the chase method
    //makes it so that the enemy will move towards the players position when they enter its look radius
    public void ChasePlayer()
    {
        if (CanSeePlayer())
        {
            navMeshA.SetDestination(player.position);

        }
    }
    //add the attack method 
    public void AttackPlayer()
    {
        //sets the cooldown   
        attackCooldown = 1f / attackspeed;
        render.sharedMaterial = mat[0];

    }
    //add the enmeies damage method 
    public void TakeDamage()
    {
        attackCooldown -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //if the player presses control then the enemy will take damage 
            EnemyHealth -= 10;
            render.sharedMaterial = mat[1];

        }

    }
    //add the die method 
    public void NPCDie()
    {
        //destroys a game object so that its no longer there once its dead 
        render.sharedMaterial = mat[2];
        Destroy(gameObject);

    }

}
