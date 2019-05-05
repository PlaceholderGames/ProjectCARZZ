using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class AICollision : MonoBehaviour
{
    private Animator anim;
    private AIBehaviour aib;
    private SpawnObject spawnObject;
    private MSVehicleControllerFree vehicle;
    private LevelingSystem ls;
    private Slider healthSlider;
    private DamageSystem ds;
    private ComboSystem cs;
    public Rigidbody[] bones;
    private Terrain terrain;
    public MeshRenderer cube;
    public AudioSource zombieCry;
    private Collider sphereColider;
    private TimeBodyZombies[] timeBodyZombies;

    
    public bool gaveDamage = false;
    public bool hitPlayer = false;
    public bool ishit = false;
    public bool unrag = false;
    public bool displayFin;
    public bool dead = false;

    float time = 10.0f;

    private void Start()
    {
        sphereColider = GetComponent<Collider>();
        terrain = FindObjectOfType<Terrain>();
        spawnObject = FindObjectOfType<SpawnObject>();
        cs = FindObjectOfType<ComboSystem>();
        anim = GetComponentInChildren<Animator>();
        vehicle = FindObjectOfType<MSVehicleControllerFree>();
        ls = FindObjectOfType<LevelingSystem>();
        ds = FindObjectOfType<DamageSystem>();
        aib = GetComponent<AIBehaviour>();
        healthSlider = GameObject.Find("healthSlider").GetComponent<Slider>();
        timeBodyZombies = GetComponentsInChildren<TimeBodyZombies>();
        zombieCry = gameObject.GetComponent<AudioSource>();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            hitPlayer = true;
            anim.SetBool("isWon", true);
        }

        if (other.tag == "Vehicle" || other.tag == "Vehicle1" || other.tag == "Vehicle2" || other.tag == "Vehicle3" || other.tag == "activeVehicle")
        {
            if (vehicle.KMh > 30.0f)
            {
                anim.enabled = false;
                StartCoroutine(ragdoll());
                cs.zombieIsDead = true;
                ishit = true;
                ls.currentXP += (10 + (int)Mathf.Sqrt(vehicle.KMh));
                ls.UpdateLevelingSystem();
                ds.RecievedDamage = Random.Range(1, 10);
                healthSlider.value -= ds.RecievedDamage;
                aib.enabled = false;
                sphereColider.enabled = false;
                cube.enabled = false;
                time = 10.0f;
            }
            if (ls.finished())
            {
                displayFin = true;
            }
        }
    }

    private void LateUpdate()
    {

        float dist = Vector3.Distance(vehicle.transform.position, transform.position);

        if (dist > spawnObject.radius)
        {
            gameObject.SetActive(false);
        }



        if (ishit)
        {
            Invoke("Destroy", 5.0f);
            time -= Time.deltaTime;
            if(Input.GetKeyUp(KeyCode.R) && time > 0)
            {
                CancelInvoke();
                anim.enabled = true;
                cs.zombieIsDead = false;
                ishit = false;
                aib.enabled = true;
                sphereColider.enabled = true;
                cube.enabled = true;
            }
            else if (Input.GetKeyUp(KeyCode.R) && time <= 0)
            {
                foreach(TimeBodyZombies o in timeBodyZombies)
                {
                    o.StopKinematic();
                }
            }
        }
       

        /*
         * 
         * Respawning of zombies code
         *  
         */

        //if (ishit)
        //{
        //    Vector3 pos = new Vector3(Random.Range(0, terrain.terrainData.size.x), 0, Random.Range(0, terrain.terrainData.size.z)); //sets pos vector randomly
        //    pos.y = terrain.SampleHeight(pos) + 1.0f;
        //    gameObject.transform.position = pos;
        //    
        //    aib.enabled = true;
        //    ishit = false;

        //}



        //if (unrag)
        //{
        //    //unragdoll();
        //    unrag = false;
        //}
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Vehicle" || other.tag == "Vehicle1" || other.tag == "Vehicle2" || other.tag == "Vehicle3" || other.tag == "activeVehicle")
        {
            if (vehicle.KMh < 30.0f)
            {
                ds.RecievedDamage = Random.Range(1, 10);
                healthSlider.value -= ds.RecievedDamage * Time.deltaTime;
            }
        }
    }

    IEnumerator ragdoll()
    {
        
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().useGravity = true;
        foreach (Rigidbody a in bones)
        {
            a.isKinematic = false;
            a.useGravity = true;
        }
        yield return null;
    }

    void Destroy()
    {
        dead = true;
        gameObject.SetActive(false);
    }
}



