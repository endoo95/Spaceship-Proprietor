using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorScript : MonoBehaviour, IHaveHealth
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject parent;
    [SerializeField] private MeteorScriptableObject meteorScriptableObject;
    [SerializeField] private HealthBarScript healtBar;

    private GameObject collidedObject;

    public float flySpeed = 3f;
    public float turnSpeed = 2f;
    private float rotationRandom;

    public float health = 50f;
    public bool health30 = false;
    private float barSize = 1f;

    private float meteorDamage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision!");
        collidedObject = collision.gameObject;
    }

    private void Awake()
    {
        if(meteorScriptableObject == null)
        {
            Destroy(parent);
        }


    }
    void Start()
    {
        flySpeed = meteorScriptableObject.flySpeed;
        turnSpeed = meteorScriptableObject.turnSpeed;
        health = meteorScriptableObject.maxHealth;

        meteorDamage = health / 3;
        parent = transform.parent.gameObject;

        //rb = transform.Find("Meteor").GetComponent<Rigidbody2D>(); //Wyszukanie komponentu Rigidbody2D z dzieci o nazwie "Meteor"
        rb = this.GetComponent<Rigidbody2D>(); //Lokalne wyszukanie komponentu Rigidbody
        rb.gravityScale = 0;
        rb.angularDrag = 0;

        healtBar = parent.transform.Find("HealthBar").GetComponent<HealthBarScript>();

        rotationRandom = Random.Range(-50f, 50f);

        rb.angularVelocity = turnSpeed * rotationRandom * 10 * Time.fixedDeltaTime;

        Vector2 randomForce;
        randomForce = new Vector2(flySpeed * Random.Range(-5f, 5f) * 10, flySpeed * Random.Range(-5f, 5f) * 10);

        Vector2 force;
        force = randomForce;

        rb.AddForce(force);

        healtBar.SetSize(barSize);
        healtBar.SetColor(Color.green);
    }

    private void Update()
    {
        DoIColide();

        Destroy(parent, 25f);
    }

    void DoIColide()
    {
        if(collidedObject == null)
        {
            return;
        }

        string tag;
        tag = collidedObject.tag;

        TakeDamage(meteorDamage);
        UpdateHealth();

        Debug.Log(tag);
        switch (tag)
        {
            case "Ship":
                collidedObject.GetComponent<ShipDamageHandler>().TakeDamage(meteorDamage);
                break;
            case "Player":
                collidedObject.GetComponent<ShipDamageHandler>().TakeDamage(meteorDamage);
                break;
            case "Meteor":
                collidedObject.GetComponent<MeteorScript>().TakeDamage(meteorDamage);
                break;
            default:
                break;
        }
    }

    public void TakeDamage(float damage)
    {
        ModifyHealth(damage);
        if (health <= 0)
        {
            Destroy(parent);
        }
    }

    public float ModifyHealth(float damage)
    {
        health -= damage;
        return health;
    }

    void UpdateHealth()
    {
        barSize = health / 50f;
        healtBar.SetSize(barSize);

        if (barSize > 0)
        {

            if (barSize < 0.3f)
            {
                /*if ((int)(barSize * 100f) % 3 == 0)
                {
                    healtBar.SetColor(Color.white);
                }
                else
                {
                    healtBar.SetColor(Color.green);
                }*/
                health30 = true;
            }
            else
            {
                health30 = false;
            }
        }
        else
        {
            Destroy(parent);
        }
    }
}
