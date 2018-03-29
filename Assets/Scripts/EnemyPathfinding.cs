using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using Random = UnityEngine.Random;

public class EnemyPathfinding : MonoBehaviour
{
    [Tooltip("Range at which the enemy will detect goblins, in metres. Can be set randomly in right-click menu.")] 
    public float visibleRange; //The range the enemy can see you out to

    private IEnumerator goHitArray; //The coroutine to look for enemies

    private Collider2D[] hitObjects; //The array of objects hit by the enemy circle cast

    [Tooltip("Whether or not to be actively searching for the player")]
    public bool search = true; //Whether or not the enemy is currently looking for a player

    public enum enemyTypes    
    {    
        Slime,            //Slime behaviour               |Slimes wander haphazardly in the direction of the player, only doing damage on contact.
        Rogue,            //Rogue behaviour               |Rogues will try to get behind the player as directly as possible
        Archer,           //Archer behaviour              |Archer's will get to a set distance relative to the player to shooty shoot them
        Eyeball,          //Eyeball behaviour             |Eyeballs will take the most direct possible path to the target to boop into them
        Fighter,          //Fighter behaviour             |Fighters will try to make a beeline to the player and swing they sword
        Farmer,           //Farmer behaviour              |Tries to poke the Gobbleyboi with a pitchfork, but does get scared
        BiggerFighter,    //BiggerFighter behaviour       |BiggerFighter tries to stay out of range of the gobbldeegook attacks while hitting with a big sword
        Rat               //Rat behaviour                 |Rats rats rats rats rats rats rats rats RaTs rAtS RAts raTS RATS r475 BIG FOOD
    };    //Options for the ai behaviour dropdown menu

    [Tooltip("The type of AI to give this enemy." +
             "\n" +
             "\nSlime: wander towards goblin. Damage on contact." +
             " " +
             "\nRogue: Get behind enemy. Stab with dagger." +
             "\n" +
             "\nArcher: Set distance to goblins. Shoot arrows." +
             "\n" +
             "\nEyeball: Float to goblins. Damage on contact." +
             "\n" +
             "\nFighter: Sraight to player. Damage with sword." +
             "\n" +
             "\nFarmer: Hit and run with pitchfork." +
             "\n" +
             "\nBiggerFighter: Stay out of short range attacks. hit with sword." +
             "\n" +
             "\nRat: Straight toward goblin. Can be eaten as food.")]    //Tooltip for enemy ai types
    public enemyTypes aiType;    //The chosen aiType
    
    [Tooltip("Rigidbody for the enemy, should be the one on this prefab.")]
    public Rigidbody2D body;    //The enemy rigidBody

    [Range(0, 100)] [Tooltip("The speed at which the enemy tracks the player. Can be randomized in right-click menu.")]
    public float speed;    //Speed to chase the goblin


    [ContextMenu("Choose Random Values")]
    private void ChooseRandomValues()    //Does exactly what it says
    {
        visibleRange = Random.Range(100f, 1000f);
        speed = Random.Range(0f, 100f);
    }
    
    /// <summary>
    /// Code for enemy pathfinding and behaviours
    /// </summary>
    void Start()
    {
        StartCoroutine(checkHitArray(0.01f));
    }

    void FixedUpdate()
    {
        for (int i = 0; i < hitObjects.Length; i++) //Searches through the array of found objects
        {
            Debug.Log("FOR loop");
            if (hitObjects[i].tag == "Goblin")
            {
                body.velocity = (((Vector2) hitObjects[i].transform.position - body.position)).normalized * speed *
                                Time.fixedDeltaTime; //Moves the enemy towards the goal been
            }
        }
    }


    private IEnumerator checkHitArray(float waitTime)
    {
        Debug.Log("Coroutine");

        while (true)
        {
            if (search && (aiType == enemyTypes.Eyeball || aiType == enemyTypes.Rat || aiType == enemyTypes.Slime))    //Behaviour for beelining to the goblins
            {
                hitObjects =
                    Physics2D.OverlapCircleAll(transform.position, visibleRange); //Check for any goblins within range
            }

            yield return new WaitForSeconds(waitTime); //Wait for next poll time
                
        }
    }
}