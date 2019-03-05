using UnityEngine.UI;
using UnityEngine;

public class AICollision : MonoBehaviour
{
    private Animator anim;
    private SpawnObject spawnObject;
    private MSVehicleControllerFree vehicle;
    private LevelingSystem ls;
    private Slider healthSlider;
    private DamageSystem ds;
    private ComboSystem cs;


    public float despawnTime = 0.01f;
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
        healthSlider = GameObject.Find("healthSlider").GetComponent<Slider>();
    }

    private void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Player")
        {
            hitPlayer = true;
            anim.SetBool("isWon", true);
        }


        if (collisionInfo.collider.tag == "Vehicle" || collisionInfo.collider.tag == "Vehicle1" || collisionInfo.collider.tag == "Vehicle2" || collisionInfo.collider.tag == "Vehicle3" || collisionInfo.collider.tag == "activeVehicle")
        {
            
            if (vehicle.KMh > 30.0f)
            {
                cs.zombieIsDead = true;
                ls.currentXP += (10 + (int)Mathf.Sqrt(vehicle.KMh));
                ls.UpdateLevelingSystem();
                ds.RecievedDamage = Random.Range(1, 10);
                healthSlider.value -= ds.RecievedDamage;
                Destroy(gameObject, despawnTime);
                spawnObject = FindObjectOfType<SpawnObject>();//Finds the AI object
                spawnObject.CurrentNumberAi--;//Removes it from list of all ai on scene

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

}
