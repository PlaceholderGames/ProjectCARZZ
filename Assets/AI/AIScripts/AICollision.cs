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


    public float despawnTime = 5.0f;
    public bool gaveDamage = false;
    public bool hitPlayer = false;
    public bool ishit = false;
    public bool displayFin;

    private void Start()
    {
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
                ls.currentXP += (10 + (int)Mathf.Sqrt(vehicle.KMh));
                ls.UpdateLevelingSystem();
                ds.RecievedDamage = Random.Range(1, 10);
                healthSlider.value -= ds.RecievedDamage;
                aib.enabled = false;
                spawnObject = FindObjectOfType<SpawnObject>();//Finds the AI object
                spawnObject.CurrentNumberAi--;//Removes it from list of all ai on scene
                Destroy(gameObject, despawnTime);
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

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.collider.tag == "Player")
    //    {
    //        hitPlayer = true;
    //        anim.SetBool("isWon", true);
    //    }

    //    if (collision.collider.tag == "Vehicle" || collision.collider.tag == "Vehicle1" || collision.collider.tag == "Vehicle2" || collision.collider.tag == "Vehicle3" || collision.collider.tag == "activeVehicle")
    //    {

    //        if (vehicle.KMh > 30.0f)
    //        {
    //            anim.enabled = false;
    //            StartCoroutine(ragdoll());
    //            cs.zombieIsDead = true;
    //            ls.currentXP += (10 + (int)Mathf.Sqrt(vehicle.KMh));
    //            ls.UpdateLevelingSystem();
    //            ds.RecievedDamage = Random.Range(1, 10);
    //            healthSlider.value -= ds.RecievedDamage;
    //            aib.enabled = false;
    //            spawnObject = FindObjectOfType<SpawnObject>();//Finds the AI object
    //            spawnObject.CurrentNumberAi--;//Removes it from list of all ai on scene
    //            Destroy(gameObject, despawnTime);
    //        }
    //        else if (vehicle.KMh < 30.0f)
    //        {
    //            ds.RecievedDamage = Random.Range(1, 10);
    //            healthSlider.value -= ds.RecievedDamage * Time.deltaTime;
    //        }
    //        if (ls.finished())
    //        {
    //            displayFin = true;
    //        }
    //    }
    //}

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
}



