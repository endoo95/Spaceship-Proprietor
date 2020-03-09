using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorScript : MonoBehaviour
{
    public float flySpeed = 3f;
    public float turnSpeed = 2f;
    private float rotationRandom;

    public float health = 50f;
    public bool health30 = false;
    private float barSize = 1f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private HealthBarScript healtBar;

    // Start is called before the first frame update
    void Start()
    {
        rb = transform.Find("Meteor").GetComponent<Rigidbody2D>(); //Wyszukanie komponentu Rigidbody2D z dzieci o nazwie "Meteor"
        //rb = this.GetComponent<Rigidbody2D>(); //Lokalne wyszukanie komponentu Rigidbody
        healtBar = transform.Find("HealthBar").GetComponent<HealthBarScript>();

        rotationRandom = Random.Range(-50f, 50f);

        Vector2 randomForce;
        randomForce = new Vector2(flySpeed * Random.Range(-5f, 5f) * 10, flySpeed * Random.Range(-5f, 5f) * 10);

        Vector2 force;
        force = randomForce;

        rb.AddForce(force);

        //rb.MoveRotation(turnSpeed * Random.Range(-5f, 5f));
        //rb.rotation = turnSpeed * Random.Range(-5f, 5f);

        healtBar.SetSize(barSize);
        healtBar.SetColor(Color.green);
    }

    private void Update()
    {
        if(barSize > 0)
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
            Destroy(gameObject);
        }

        Destroy(gameObject, 15f);
    }

    private void FixedUpdate()
    {
        rb.angularVelocity = turnSpeed * rotationRandom * 10 * Time.fixedDeltaTime;
        

    }
}
