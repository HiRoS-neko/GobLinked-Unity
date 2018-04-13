using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class EnemyPathfinding : MonoBehaviour
{
    private Animator _anim;
    private int _dir;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        atkDel = attackCooldown;
        switch (aiType) //Assigning the values by class
        {
            case enemyTypes.Slime: //Setting Slime values
                speed = 1;
                health = 15;
                armor = 0;
                attack = 2;
                break;

            case enemyTypes.Rogue: //Settign Rogue values    
                speed = 3;
                health = 20;
                armor = 4;
                attack = 4;
                break;

            case enemyTypes.Archer: //Setting Archer values
                speed = 3;
                health = 20;
                armor = 4;
                attack = 4;
                break;

            case enemyTypes.Eyeball: //Setting Eyeball values 
                speed = 3;
                health = 5;
                armor = 2;
                attack = 2;
                break;

            case enemyTypes.Fighter: //Setting Fighter values
                speed = 3;
                health = 20;
                armor = 4;
                attack = 4;
                break;

            case enemyTypes.Farmer: //Setting Farmer values
                speed = 2;
                health = 12;
                armor = 2;
                attack = 3;
                break;

            case enemyTypes.BiggerFighter: //Setting Bigger Fighter values
                speed = 0;
                health = 0;
                armor = 0;
                attack = 0;
                break;

            case enemyTypes.Rat: //Setting Rat values
                speed = 4;
                health = 10;
                armor = 3;
                attack = 2;
                break;
        }

        StartCoroutine(checkHitArray(checkDelay));
    }

    private void
        FixedUpdate() //Just used to decide what ai behaviour is being calles. Open potential for enemies that change behaviours
    {
        var temp = body.velocity.normalized;

        if (Mathf.Abs(temp.x) <= Mathf.Abs(temp.y) && Mathf.Abs(temp.y) > 0.1)
        {
            if (temp.y > 0) //up
                _anim.SetInteger("dir", 1);
            else if (temp.y < 0) //down
                _anim.SetInteger("dir", 3);
        }
        else if (Mathf.Abs(temp.x) > Mathf.Abs(temp.y) && Mathf.Abs(temp.x) > 0.1)
        {
            if (temp.x > 0) //right
                _anim.SetInteger("dir", 2);
            else if (temp.x < 0) //left
                _anim.SetInteger("dir", 4);
        }
        else
        {
            _anim.SetInteger("dir", 0);
        }


        switch (aiType)
        {
            case enemyTypes.Slime: //Calling Slime bheaviour for fixedUpdate
                if (atkDel >= 0)
                {
                    atkDel -= Time.deltaTime;
                }

                slimeBehaviour();
                break;

            case enemyTypes.Rogue: //Calling Rogue bheaviour for fixedUpdate   
                if (atkDel >= 0)
                {
                    atkDel -= Time.deltaTime;
                }

                rogueBehaviour();
                break;

            case enemyTypes.Archer: //Calling Archer bheaviour for fixedUpdate
                if (atkDel >= 0)
                {
                    atkDel -= Time.deltaTime;
                }

                archerBehaviour();
                break;

            case enemyTypes.Eyeball: //Calling Eyeball bheaviour for fixedUpdate
                if (atkDel >= 0)
                {
                    atkDel -= Time.deltaTime;
                }

                eyeballBehaviour();
                break;

            case enemyTypes.Fighter: //Calling Fighter bheaviour for fixedUpdate
                if (atkDel >= 0)
                {
                    atkDel -= Time.deltaTime;
                }

                fighterBehaviour();
                break;

            case enemyTypes.Farmer: //Calling Farmer bheaviour for fixedUpdate
                if (atkDel >= 0)
                {
                    atkDel -= Time.deltaTime;
                }

                farmerBehaviour();
                break;

            case enemyTypes.BiggerFighter: //Calling Bigger Fighter bheaviour for fixedUpdate
                if (atkDel >= 0)
                {
                    atkDel -= Time.deltaTime;
                }

                biggerFighterBehaviour();
                break;

            case enemyTypes.Rat: //Calling rat bheaviour for fixedUpdate
                if (atkDel >= 0)
                {
                    atkDel -= 2 * Time.deltaTime;
                }

                ratBehaviour();
                break;
        }
    }

    /// <summary>
    ///     The handler for the coroutine to update targetable players.
    /// </summary>
    /// <param name="waitTime"></param>
    /// waitTime is the time between scans.
    /// <returns></returns>
    private IEnumerator checkHitArray(float waitTime) //Updates the array of objects hit by the enemy checkzone
    {
        while (true)
        {
            hitObjects =
                Physics2D.OverlapCircleAll(transform.position, visibleRange); //Check for any goblins within range

            yield return new WaitForSeconds(waitTime); //Wait for next poll time
        }
    }

    /// <summary>
    ///     The behavior the slime enemy will take.
    /// </summary>
    private void slimeBehaviour() //The behaviour method for the Slime, including spit.
    {
        for (var i = 0; i < hitObjects.Length; i++) //Searches through the array of found objects
        {
            if (isSearching) //Behaviour for beelining to the goblins
            {
                if (hitObjects[i].tag == "Goblin")
                {
                    body.velocity = ((Vector2) hitObjects[i].transform.position - body.position).normalized * speed *
                                    PlayerController.SpeedMultiplier /
                                    enemySpeedMultiplier; //Moves the enemy towards the goalbeen
                    if (Vector2.Distance(body.transform.position, hitObjects[i].transform.position) <= 10f)
                    {
                        float quatInput = Vector3.Angle(gameObject.transform.position,
                            hitObjects[i].gameObject.transform.position); //Getting the angle difference between the 
                        //gameObject of the slime and the gameobject
                        //of the goblin

                        if (hitObjects[i].gameObject.tag == "Goblin" && atkDel <= 0)
                        {
                            print("Eww, spit + atkDel = " + atkDel + " attackCooldown = " +
                                  attackCooldown); //Debg check

                            Quaternion rotation =
                                Quaternion.LookRotation(new Vector3(0, 0,
                                    quatInput)); //Setting the initial rotation of the spit

                            GameObject projectileSpit =
                                Instantiate(spitObject, gameObject.transform.position,
                                    rotation); //Instantiating the spit

                            projectileSpit.GetComponentInChildren<Rigidbody2D>().velocity =
                                rotation.eulerAngles; //Making the spit actually shoot towards the player

                            atkDel = attackCooldown;
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    ///     Behaviour the rogue enemy will take.
    /// </summary>
    private void rogueBehaviour() //The behaviour method for Rogues. Includes attempts to stay behind the Goblins.
    {
        for (var i = 0; i < hitObjects.Length; i++) //Searches through the array of found objects
        {
            if (isSearching) //Behaviour for beelining to the goblins
            {
                if (hitObjects[i].tag == "Goblin")
                {
                    body.velocity = ((Vector2) hitObjects[i].transform.position - body.position).normalized * speed *
                                    PlayerController.SpeedMultiplier /
                                    enemySpeedMultiplier; //Moves the enemy towards the goalbeen

                    if (Vector3.Distance(gameObject.transform.position, hitObjects[i].transform.position) < 2f &&
                        atkDel <= 0)
                    {
                        _anim.SetTrigger("Attack");

                        hitObjects[i].gameObject.GetComponentInChildren<Goblin>().TakeDamage(attack);
                    }
                }
            }
        }
    }

    /// <summary>
    ///     Behaviour the archer enemy will take.
    /// </summary>
    private void
        archerBehaviour() //The behaviour method for Archers. Includes shooting their bow and maintaining distance.
    {
        for (var i = 0; i < hitObjects.Length; i++) //Searches through the array of found objects
            if (isSearching) //Behaviour for beelining to the goblins
            {
                if ((hitObjects[i].tag == "Goblin") &&
                    (Vector2.Distance(body.transform.position, hitObjects[i].transform.position) >= 5f)
                ) //Check if the goblins are outside near range but inside visible range
                    body.velocity = ((Vector2) hitObjects[i].transform.position - body.position).normalized * speed *
                                    PlayerController.SpeedMultiplier /
                                    enemySpeedMultiplier; //Moves the enemy towards the goalbeen
            }
    }

    /// <summary>
    ///     Behaviour the eyeball enemy will take.
    /// </summary>
    private void
        eyeballBehaviour() //The behaviour method for the Eyeballs. Includes charging at the Goblins to slam, then running away to do it again.
    {
        for (var i = 0; i < hitObjects.Length; i++) //Searches through the array of found objects
            if (isSearching) //Behaviour for beelining to the goblins
                if (hitObjects[i].tag == "Goblin")
                    body.velocity = ((Vector2) hitObjects[i].transform.position - body.position).normalized * speed *
                                    PlayerController.SpeedMultiplier /
                                    enemySpeedMultiplier; //Moves the enemy towards the goalbeen
    }

    /// <summary>
    ///     Behaviour the fighter enemy will take.
    /// </summary>
    private void
        fighterBehaviour() //The basic Adventurer class. Gets in to melee with the Goblins and attacks with his sword.
    {
        for (var i = 0; i < hitObjects.Length; i++) //Searches through the array of found objects
        {
            if (isSearching) //Behaviour for beelining to the goblins
            {
                if (hitObjects[i].tag == "Goblin")
                {
                    body.velocity = ((Vector2) hitObjects[i].transform.position - body.position).normalized * speed *
                                    PlayerController.SpeedMultiplier /
                                    enemySpeedMultiplier; //Moves the enemy towards the goalbeen
                    if (Vector3.Distance(gameObject.transform.position, hitObjects[i].transform.position) < 2f &&
                        atkDel <= 0)
                    {
                        _anim.SetTrigger("Attack");

                        hitObjects[i].gameObject.GetComponentInChildren<Goblin>().TakeDamage(attack);
                    }
                }
            }
        }
    }

    /// <summary>
    ///     Behaviour the farmer enemy will take.
    /// </summary>
    private void farmerBehaviour() //The behaviour for the peasant humans. Gets in melee and uses hit and run tactics.
    {
        for (var i = 0; i < hitObjects.Length; i++) //Searches through the array of found objects
        {
            if (isSearching) //Behaviour for beelining to the goblins
            {
                if (hitObjects[i].tag == "Goblin")
                {
                    body.velocity = ((Vector2) hitObjects[i].transform.position - body.position).normalized * speed *
                                    PlayerController.SpeedMultiplier /
                                    enemySpeedMultiplier; //Moves the enemy towards the goalbeen

                    if (Vector3.Distance(gameObject.transform.position, hitObjects[i].transform.position) < 2f &&
                        atkDel <= 0)
                    {
                        _anim.SetTrigger("Attack");

                        hitObjects[i].gameObject.GetComponentInChildren<Goblin>().TakeDamage(attack);
                    }
                }
            }
        }
    }

    /// <summary>
    ///     Behaviour of the larg fighter
    /// </summary>
    private void biggerFighterBehaviour()
    {
        for (var i = 0; i < hitObjects.Length; i++) //Searches through the array of found objects
        {
            if (isSearching) //Behaviour for beelining to the goblins
            {
                if (hitObjects[i].tag == "Goblin")
                {
                    body.velocity = ((Vector2) hitObjects[i].transform.position - body.position).normalized * speed *
                                    PlayerController.SpeedMultiplier /
                                    enemySpeedMultiplier; //Moves the enemy towards the goalbeen

                    if (Vector3.Distance(gameObject.transform.position, hitObjects[i].transform.position) < 4f &&
                        atkDel <= 0)
                    {
                        _anim.SetTrigger("Attack");

                        hitObjects[i].gameObject.GetComponentInChildren<Goblin>().TakeDamage(attack);
                    }
                }
            }
        }
    } //DONE

    /// <summary>
    ///     behaviour of the rat.
    /// </summary>
    private void ratBehaviour()
    {
        for (var i = 0; i < hitObjects.Length; i++) //Searches through the array of found objects
        {
            if (isSearching) //Behaviour for beelining to the goblins
            {
                if (hitObjects[i].tag == "Goblin")
                {
                    body.velocity = ((Vector2) hitObjects[i].transform.position - body.position).normalized * speed *
                                    PlayerController.SpeedMultiplier /
                                    enemySpeedMultiplier; //Moves the enemy towards the goalbeen

                    if (Vector3.Distance(gameObject.transform.position, hitObjects[i].transform.position) < 1f &&
                        atkDel <= 0)
                    {
                        _anim.SetTrigger("Attack");

                        hitObjects[i].gameObject.GetComponentInChildren<Goblin>().TakeDamage(attack);
                    }
                }
            }
        }
    } //DONE

    /// <summary>
    ///     Applies damage to the enemy.
    /// </summary>
    public void TakeDamage(int dam)
    {
        health -= Math.Max(dam - armor, 1);
        if (health <= 0)
        {
            enemyDied();
        }
    }

    /// <summary>
    ///     Handles the enemies health dropping to 0 or less
    /// </summary>
    private void enemyDied()
    {
        Instantiate(drop);
        Destroy(this);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Goblin" && atkDel <= 0)
        {
            _anim.SetTrigger("Attack");
            other.gameObject.GetComponentInChildren<Goblin>().TakeDamage(attack);
            atkDel = attackCooldown;
        }
    }


    #region //This is the region for class variables

    public enum enemyTypes
    {
        Slime, //Slime behaviour-----------------------|Slimes wander haphazardly in the direction of the player, only doing damage on contact.
        Rogue, //Rogue behaviour-----------------------|Rogues will try to get behind the player as directly as possible
        Archer, //Archer behaviour---------------------|Archer's will get to a set distance relative to the player to shooty shoot them
        Eyeball, //Eyeball behaviour-------------------|Eyeballs will take the most direct possible path to the target to boop into them
        Fighter, //Fighter behaviour-------------------|Fighters will try to make a beeline to the player and swing they sword
        Farmer, //Farmer behaviour---------------------|Tries to poke the Gobbleyboi with a pitchfork, but does get scared
        FarmerWoman, //Farmer woman behaviour----------|Support unit. Works in pair with a Farmer
        BiggerFighter, //BiggerFighter behaviour-------|BiggerFighter tries to stay out of range of the gobbldeegook attacks while hitting with a big sword
        Rat //Rat behaviour----------------------------|Rats rats rats rats rats rats rats rats RaTs rAtS RAts raTS RATS r475 BIG FOOD. Charges the player and tries to bite the Goblins
    } //Options for the ai behaviour dropdown menu

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
             "\nFarmer Woman: Attempts to help Farmers hit Goblin." +
             "\n" +
             "\nBiggerFighter: Stay out of short range attacks. hit with sword." +
             "\n" +
             "\nRat: Straight toward goblin. Can be eaten as food.")]
    //Tooltip for enemy ai types
    public enemyTypes aiType; //The chosen aiType

    [Range(0, 100)] [Tooltip("The enemies default armour value.")]
    public int armor; //Enemy armor value

    [Range(0, 50)] [Tooltip("The damage value of the enemy")]
    public int attack; //Enemy default attack value

    [Tooltip("The amount of experience for the mob to reward.")]
    public int experienceDrop; //Enemy experience reward value

    [Range(0, 100)] [Tooltip("The speed at which the enemy tracks the player. Can be randomized in right-click menu.")]
    public float speed; //Speed to chase the goblin

    [Range(0.01f, float.MaxValue)]
    [Tooltip(
        "The time to wait between updating the list of things the enemy could chase. Don't use the slider, it's bad. Just enter a value.")]
    public float checkDelay; //Speed of updating enemies list i of chase targets

    [Tooltip("Rigidbody for the enemy, should be the one on this prefab.")]
    public Rigidbody2D body; //The enemy rigidBody

    private int currHealth; //Enemies current hp value

    [Range(1, 10)]
    [SerializeField]
    [Tooltip("The speed multiplier for enemies. Since the player controller script is MEGA AUTISM")]
    private int enemySpeedMultiplier; //Changes the speel multiplier for the enemy. I highly recommend 5-8.

    private IEnumerator goHitArray; //The coroutine to look for enemies

    public int health; //Enemies maximum hp

    private Collider2D[] hitObjects; //The array of objects hit by the enemy circle cast

    [Tooltip("Whether or not to be actively searching for the player")]
    public bool isSearching = true; //Whether or not the enemy is currently looking for a player

    [Tooltip("The delay in seconds between the enemies attack damage being dealt.")]
    public float attackCooldown; //Delay between attack calls

    [Tooltip("Range at which the enemy will detect goblins, in metres. Can be set randomly in right-click menu.")]
    public float visibleRange; //The range the enemy can see you out to

    [SerializeField] private float atkDel;

    [Tooltip("The audio clip for impact damage. Slimes, etc")]
    public AudioClip impact;

    [Tooltip("Audio clip for weapon attacks. Swords etc")]
    public AudioClip weaponAttack;

    [Tooltip("The slime spit")] public GameObject spitObject;

    [Tooltip("The item this nemy will drop. Can be nothing")]
    public GameObject drop;

    [ContextMenu("Choose Random Values")]
    private void ChooseRandomValues() //Assigns random values to the enemies attributes. For testing.
    {
        visibleRange = Random.Range(100f, 1000f);
        speed = Random.Range(0f, 100f);
        health = (int) Random.Range(0f, 30f);
        armor = (int) Random.Range(0f, 100f);
        attack = (int) Random.Range(0f, 5f);
    }

    private GameObject spitIt;

    #endregion
}