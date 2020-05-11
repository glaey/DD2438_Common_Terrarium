using Assets.Scripts.CreatureBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Creature class holds all small constraints of the common environment.
/// You may use this class to find all character traits of the animal you're controlling, such as
/// + Max Speed,
/// + Sensing
/// + etc.
/// 
/// The only property on which you get no hands is the energy of the creature, so that you can't manually set it.
/// 
/// Remember you can extend this class and build more upon it.
/// </summary>
public class Creature : MonoBehaviour
{
    ///------------PROPERTIES
    
    /// <summary>
    /// The Sensor of the creature keeps track of what is surrounding it
    /// Call it when you need to detect something.
    /// </summary>
    public ISensor Sensor { get; set; }

    /// <summary>
    /// The EnergyManager determines the ernergy rewards/loss
    /// for eating/holding on to life.
    /// </summary>
    public ICostFunction EnergyManager { get; private set; }

    public IReproduction Reproducer { get; private set; }


    /// <summary>
    /// States what kind of food this creature can eat.
    /// </summary>
    public Regime CreatureRegime { get; set; }

    /// <summary>
    /// The size of the creature.
    /// You can access it normally, but when setting it, the gameObject will also get bigger in the real scene.
    /// </summary>
    public float Size { get { return transform.localScale.magnitude; }
        set { transform.localScale = transform.localScale.normalized * value; } }

    /// <summary>
    /// The maximal speed of the creature. How fast it moves at best.
    /// </summary>
    public float MaxSpeed { get; set; }

    /// <summary>
    /// The current Energy of the creature. You have absolutely not hands on it and can
    /// just consult its state.
    /// </summary>
    public float Energy {
        get => currentEnergy;
        private set { currentEnergy = Mathf.Min(MaxEnergy, value); } }
    private float currentEnergy;

    /// <summary>
    /// The maximum amount of energy a creature can reach.
    /// </summary>
    public float MaxEnergy { get; set; }



    /// ----- ATTRIBUTES

    protected string SpeciesName;
    protected int individualId;
    protected int speciesId;

    [SerializeField]
    private float initialSize;
    [SerializeField]
    private Regime initialRegime;
    [SerializeField]
    private float initialMaxEnergy;
    [SerializeField]
    private float initialMaxSpeed;
    [SerializeField]
    private float initialSensingRadius;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log($"Instantiating the creature !, initial setup {initialSize}, {initialRegime},{initialMaxSpeed},{initialMaxEnergy}");
        CreatureRegime = initialRegime;
        MaxSpeed = initialMaxSpeed;
        MaxEnergy = initialMaxEnergy;
        Size = initialSize;
        Sensor = new CircularSensor(initialSensingRadius);

        //Define the reproduction strategy
        Reproducer = new AsexualCommonDuplication();
        //Define the cost function for the energy
        EnergyManager = new CostFunction();
        //Set the energy to max since newborn !
        Energy = MaxEnergy;


    }

    // Update is called once per frame
    void Update()
    {
        Energy -= EnergyManager.LivingCost(this, Time.deltaTime);

        // Die
        if (Energy <= 0){
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Moves the creature towards a specific direction with some amount of the speed.
    /// </summary>
    /// <param name="direction">The direction towards which the creature is heading</param>
    /// <param name="speed">A percentage of the Speed property to use when moving, e.g. 0.5 is 50% of the Max Speed.</param>
    public virtual void Move(Vector3 direction, float speed)
    {
        speed = Mathf.Clamp(speed, 0, 1);
        direction.y = 0;
        Vector3 speedVector= direction.normalized * speed * MaxSpeed;
        transform.position += speedVector * Time.deltaTime;
        Energy -= EnergyManager.MoveCost(this, speed * MaxSpeed);
    }

    /// <summary>
    /// Call this method when you are sure you want to eat some piece of food.
    /// The piece of food is then destroyed/killed and the creature
    /// receives some energy reward.
    /// </summary>
    /// <param name="food">What the creature eats</param>
    public void Eat(GameObject food)
    {
        Energy += EnergyManager.EatingReward(food, CreatureRegime);
        Destroy(food);
    }

    /// <summary>
    /// Call this method when you want your creature to reproduce.
    /// The creature then loses some energy because of reproduction,
    /// and a baby is spawned.
    /// </summary>
    public void Reproduce()
    {
        if (Energy <= EnergyManager.ReproductionCost(this))
            return;
        //Instantiate the baby
        Creature baby = Instantiate<Creature>(this, getClosestFreePoint(transform.position),transform.rotation);
        //Modify its characteristics
        this.Reproducer.CreateBaby(this, baby);
        // The parent loses energy
        Energy -= EnergyManager.ReproductionCost(baby);
    }

    Vector3 getClosestFreePoint(Vector3 point)
    {
        Vector3 closestPoint = Vector3.positiveInfinity;
        Vector3 testPoint = point;
        float radius = 1f;
        int i = 0;

        while (closestPoint.magnitude == float.PositiveInfinity)
        {
            switch (i)
            {
                case 0:
                    testPoint = point + new Vector3(0, 0, radius);
                    break;
                case 1:
                    testPoint = point + new Vector3(0, 0, -radius);
                    break;
                case 2:
                    testPoint = point + new Vector3(radius, 0, 0);
                    break;
                case 3:
                    testPoint = point + new Vector3(-radius, 0, 0);
                    radius = radius + 1f;
                    break;
            }

            if (!isObstacle(testPoint))
            {
                closestPoint = testPoint;
            }

            i = (i + 1) % 4;
        }
        return closestPoint;
    }

    bool isObstacle(Vector3 point)
    {
        LayerMask mask = LayerMask.GetMask("Default");
        bool obstacle = false;

        Collider[] hitColliders = Physics.OverlapSphere(point, transform.localScale.x, mask);
        if (hitColliders.Length != 0)
        {
            obstacle = true;
        }
        return obstacle;
    }

    public enum Regime
    {
        HERBIVORE,
        CARNIVORE
    }
}
