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


    public float despawnTime = 5.0f;
    public bool gaveDamage = false;
    public bool hitPlayer = false;
    public bool ishit = false;
    public bool unrag = false;
    public bool displayFin;

    private void Start()
    {
        terrain = FindObjectOfType<Terrain>();
        spawnObject = FindObjectOfType<SpawnObject>();
        cs = FindObjectOfType<ComboSystem>();
        anim = GetComponentInChildren<Animator>();
        vehicle = FindObjectOfType<MSVehicleControllerFree>();
        ls = FindObjectOfType<LevelingSystem>();
        ds = FindObjectOfType<DamageSystem>();
        aib = GetComponent<AIBehaviour>();
        healthSlider = GameObject.Find("healthSlider").GetComponent<Slider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            hitPlayer = true;
            anim.SetBool("isWon", true);
            Debug.Log("HIT PLYER!");
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
            }
            else if (vehicle.KMh < 30.0f)
            {
                ds.RecievedDamage = Random.Range(1, 10);
                healthSlider.value -= ds.RecievedDamage * Time.deltaTime;
            }
            if (ls.finished())
            {
                displayFin = true;
            }
        }
    }

    private void Update()
    {

        float dist = Vector3.Distance(vehicle.transform.position, transform.position);

        if (dist > spawnObject.radius)
        {
            gameObject.SetActive(false);
        }


        //if (ishit)
        //{
        //    Vector3 pos = new Vector3(Random.Range(0, terrain.terrainData.size.x), 0, Random.Range(0, terrain.terrainData.size.z)); //sets pos vector randomly
        //    pos.y = terrain.SampleHeight(pos) + 1.0f;
        //    gameObject.transform.position = pos;
        //    
        //    aib.enabled = true;
        //    ishit = false;

        //}

        if (ishit)
        {
            Invoke("Destroy", 2.0f);
        }

        

        if (unrag)
        {
            //unragdoll();
            unrag = false;
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

    void unragdoll()
    {
        foreach (Rigidbody a in bones)
        {
            a.isKinematic = false;
            a.useGravity = false;
        }
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().useGravity = true;
        
    }

    void Destroy()
    {
        gameObject.SetActive(false);
        ishit = false;
    }
}



