using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorScript : MonoBehaviour, IHaveHealth
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject parent;
    [SerializeField] private MeteorScriptableObject meteorScriptableObject;
    [SerializeField] private BarScript healthBar;

    [SerializeField] private string meteorSize;

    private GameObject collidedObject;

    public float flySpeed = 3f;
    public float turnSpeed = 2f;

    public float maxHealth = 50f;
    public float health = 50f;
    public bool health30 = false;
    private float barSize = 1f;

    private float meteorDamage;


    private void OnCollisionEnter2D(Collision2D collision)
    {
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
        maxHealth = meteorScriptableObject.maxHealth;
        health = meteorScriptableObject.maxHealth;
        meteorSize = meteorScriptableObject.meteorSize;

        meteorDamage = health / 3;
        parent = transform.parent.gameObject;

        //rb = transform.Find("Meteor").GetComponent<Rigidbody2D>(); //Wyszukanie komponentu Rigidbody2D z dzieci o nazwie "Meteor"
        rb = this.GetComponent<Rigidbody2D>(); //Lokalne wyszukanie komponentu Rigidbody
        rb.gravityScale = 0;
        rb.angularDrag = 0;

        healthBar = parent.transform.Find("HealthBar").GetComponent<BarScript>();

        float rotationRandom;
        rotationRandom = Random.Range(-50f, 50f);

        rb.angularVelocity = turnSpeed * rotationRandom * 10 * Time.fixedDeltaTime;

        Vector2 randomForce;
        randomForce = new Vector2(flySpeed * Random.Range(-5f, 5f), flySpeed * Random.Range(-5f, 5f));

        Vector2 force;
        force = randomForce;

        rb.AddForce(force);

        healthBar.SetSize(barSize);
        healthBar.SetColor(Color.green);
    }

    private void Update()
    {
        Destroy(parent, 25f);
    }

    private void LateUpdate()
    {
        DoIColide();
    }

    private void DoIColide()
    {
        if(collidedObject == null)
        {
            return;
        }

        CollideRecoil();

        string tag;
        tag = collidedObject.tag;

        TakeDamage(meteorDamage);
        UpdateHealth();

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
        collidedObject = null;
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

    private void UpdateHealth()
    {
        barSize = health / maxHealth;
        healthBar.SetSize(barSize);

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

    private void CollideRecoil()
    {
        Vector2 vector;
        vector = transform.position - collidedObject.gameObject.transform.position;

        Vector2 force;
        force = rb.velocity + vector;

        rb.velocity = force / 2;
    }
}
