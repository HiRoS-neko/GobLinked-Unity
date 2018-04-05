using System.Collections;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    public enum enemyTypes
    {
        Slime, //Slime behaviour               |Slimes wander haphazardly in the direction of the player, only doing damage on contact.
        Rogue, //Rogue behaviour               |Rogues will try to get behind the player as directly as possible
        Archer, //Archer behaviour              |Archer's will get to a set distance relative to the player to shooty shoot them
        Eyeball, //Eyeball behaviour             |Eyeballs will take the most direct possible path to the target to boop into them
        Fighter, //Fighter behaviour             |Fighters will try to make a beeline to the player and swing they sword
        Farmer, //Farmer behaviour              |Tries to poke the Gobbleyboi with a pitchfork, but does get scared
        FarmerWoman, //Farmer woman behaviour        |Beats up the Goblins with a ladle and shoe
        BiggerFighter, //BiggerFighter behaviour       |BiggerFighter tries to stay out of range of the gobbldeegook attacks while hitting with a big sword
        Rat //Rat behaviour                 |Rats rats rats rats rats rats rats rats RaTs rAtS RAts raTS RATS r475 BIG FOOD
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
             "\nBiggerFighter: Stay out of short range attacks. hit with sword." +
             "\n" +
             "\nRat: Straight toward goblin. Can be eaten as food.")]
    //Tooltip for enemy ai types
    public enemyTypes aiType; //The chosen aiType

    [Range(0, 100)] [Tooltip("The enemies default armour value.")]
    public int armor; //Enemy armor value

    [Range(0, 5)] [Tooltip("The damage value of the enemy")]
    public int attack; //Enemy default attack value

    [Tooltip("Rigidbody for the enemy, should be the one on this prefab.")]
    public Rigidbody2D body; //The enemy rigidBody

    private int currHealth; //Enemies current hp value

    [Range(1, 10)]
    [SerializeField]
    [Tooltip("The speed multiplier for enemies. Since the player controller script is MEGA AUTISM")]
    private int enemySpeedMultiplier;

    private IEnumerator goHitArray; //The coroutine to look for enemies

    [Range(0, 30)] [Tooltip("The enemies health value. 0-30")]
    public int health; //Enemies maximum hp

    private Collider2D[] hitObjects; //The array of objects hit by the enemy circle cast

    [Tooltip("Whether or not to be actively searching for the player")]
    public bool search = true; //Whether or not the enemy is currently looking for a player

    [Range(0, 100)] [Tooltip("The speed at which the enemy tracks the player. Can be randomized in right-click menu.")]
    public float speed; //Speed to chase the goblin

    [Tooltip("Range at which the enemy will detect goblins, in metres. Can be set randomly in right-click menu.")]
    public float visibleRange; //The range the enemy can see you out to

    [ContextMenu("Choose Random Values")]
    private void ChooseRandomValues() //Assigns random values to the enemies attributes. For testing.
    {
        visibleRange = Random.Range(100f, 1000f);
        speed = Random.Range(0f, 100f);
        health = (int) Random.Range(0f, 30f);
        armor = (int) Random.Range(0f, 100f);
        attack = (int) Random.Range(0f, 5f);
    }

    /// <summary>
    ///     Code for enemy pathfinding and behaviours
    /// </summary>
    private void Start()
    {
        switch (aiType) //Assigning the values by class
        {
            case enemyTypes.Slime: //Setting Slime values
                speed = 1;
                health = 15;
                armor = 0;
                attack = 2;
                break;


            case enemyTypes.Rogue: //Settign Rogue values    
                speed = 0;
                health = 0;
                armor = 0;
                attack = 0;
                break;


            case enemyTypes.Archer: //Settign Archer values
                speed = 0;
                health = 0;
                armor = 0;
                attack = 0;
                break;


            case enemyTypes.Eyeball: //Setting Eyeball values (Floatsee in the doc)
                speed = 3;
                health = 5;
                armor = 2;
                attack = 2;
                break;


            case enemyTypes.Fighter: //Setting Fighter values (referred to as adventurer in the doc)
                speed = 3;
                health = 20;
                armor = 4;
                attack = 4;
                break;


            case enemyTypes.FarmerWoman: //Setting Farmer Woman values
                speed = 0;
                health = 0;
                armor = 0;
                attack = 0;
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

        StartCoroutine(checkHitArray(0.01f));
    }

    private void FixedUpdate()
    {
        for (var i = 0; i < hitObjects.Length; i++) //Searches through the array of found objects
            if (search &&
                (aiType == enemyTypes.Eyeball || aiType == enemyTypes.Rat ||
                 aiType == enemyTypes.Slime || //Behaviour for beelining to the goblins
                 aiType == enemyTypes.Farmer ||
                 aiType == enemyTypes.FarmerWoman)) //Behaviour for beelining to the goblins
                if (hitObjects[i].tag == "Goblin")
                {
                    body.velocity = ((Vector2) hitObjects[i].transform.position //Moves the enemy towards the goalbeen
                                     - body.position).normalized //Moves the enemy towards the goalbeen
                                    * speed //Moves the enemy towards the goalbeen
                                    * PlayerController.SpeedMultiplier /
                                    enemySpeedMultiplier; //Moves the enemy towards the goalbeen

                    print("Slime activated");
                }
    }


    private IEnumerator checkHitArray(float waitTime)
    {
        Debug.Log("Coroutine");

        while (true)
        {
            hitObjects =
                Physics2D.OverlapCircleAll(transform.position, visibleRange); //Check for any goblins within range


            yield return new WaitForSeconds(waitTime); //Wait for next poll time
        }
    }
}