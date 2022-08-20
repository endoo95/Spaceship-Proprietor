using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDamageHandler : MonoBehaviour, IShip
{
    [SerializeField] private ShipScriptableObject _shipScriptableObject;
    [SerializeField] private Transform _shieldObject;
    [SerializeField] private Transform _shipBarsObject;
    [SerializeField] private GameObject _shipObject;

    private GameObject _shieldBack;
    private GameObject _shieldFront;
    private GameObject _shieldRing;

    private BarScript _healthBar;
    private BarScript _armorBar;
    private BarScript _shieldBar;

    private CircleCollider2D _shieldCollider;
    private PolygonCollider2D _hullCollider;

    [SerializeField] private float _shipHull;
    [SerializeField] private float _shipArmor;
    [SerializeField] private int _shipShield;
    [SerializeField] private float _shieldRecharge;

    private float timeBetweenRecharge;
    private float countdown;
    private bool isTimerRunning = false;

    private float barSize = 1f;

    private void Awake()
    {
        if (_shipScriptableObject == null || _shieldObject == null || _shipObject == null)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //Find Shield objects
        _shieldBack = _shieldObject.Find("Back").gameObject;
        _shieldFront = _shieldObject.Find("Front").gameObject;
        _shieldRing = _shieldObject.Find("Ring").gameObject;

        //Find bars objects
        _healthBar = _shipBarsObject.Find("HealthBar").gameObject.GetComponent<BarScript>();
        _armorBar = _shipBarsObject.Find("ArmorBar").gameObject.GetComponent<BarScript>();
        _shieldBar = _shipBarsObject.Find("ShieldBar").gameObject.GetComponent<BarScript>();

        //Assign colliders
        _shieldCollider = _shipObject.GetComponent<CircleCollider2D>();
        _hullCollider = _shipObject.GetComponent<PolygonCollider2D>();

        //Load scriptable object values
        _shipArmor = _shipScriptableObject.maxArmor;
        _shipShield = _shipScriptableObject.maxShield;
        _shipHull = _shipScriptableObject.maxHealth;
        _shieldRecharge = _shipScriptableObject.shieldRecharge;

        //Set bars info
        _healthBar.SetSize(barSize);
        _healthBar.SetColor(Color.green);
        _armorBar.SetSize(barSize);
        _armorBar.SetColor(Color.yellow);
        _shieldBar.SetSize(barSize);
        _shieldBar.SetColor(Color.cyan);

        //Init shield
        CalculateTime(_shieldRecharge);
    }

    private void Update()
    {
        //Recharge shield until it's full
        if (_shipShield < _shipScriptableObject.maxShield && isTimerRunning == false)
        {
            isTimerRunning = true;
            StartCoroutine(WaitSomeTime(timeBetweenRecharge));
        }

        ShieldStatus(_shipShield);
    }

    public void TakeDamage(float damage)
    {
        float damageLeft = damage;
        if (_shipShield > 0)
        {
            ModifyShield((int)(damage));
            return;
        }
        else
        {
            if (_shipArmor > 0)
            {
                damageLeft -= _shipArmor;
                ModifyArmor(damage);
                _armorBar.SetSize(_shipArmor / 100f);
                if (damageLeft <= 0)
                {
                    return;
                }
                damage = damageLeft;
            }
            ModifyHealth(damage);
            _healthBar.SetSize(_shipHull / 100);
        }
        if (_shipHull <= 0)
        {
            //TODO: 
            //Ship destruction
            Destroy(gameObject.transform.parent.gameObject);
        }
    }

    public int ModifyShield(int damage)
    {
        _shipShield -= 1;
        return _shipShield;
    }

    public float ModifyArmor(float damage)
    {
        _shipArmor -= damage;
        if (_shipArmor < 0)
        {
            _shipArmor = 0;
        }
        return _shipArmor;
    }

    public float ModifyHealth(float damage)
    {
        _shipHull -= damage;
        if (_shipHull < 0)
        {
            _shipHull = 0;
        }
        return _shipHull;
    }

    //TODO: Shield bar in segments
    private void ShieldStatus(int shields)
    {
        float maxShield = (float)_shipScriptableObject.maxShield;
        float shieldProcentage;

        shieldProcentage = (shields / maxShield) * 100f;
        _shieldBar.SetSize(shieldProcentage / 100f);

        if (shieldProcentage > 70f)
        {
            _shieldBack.SetActive(true);
            return;
        }
        else
        {
            _shieldBack.SetActive(false);
            if (shieldProcentage > 35f)
            {
                _shieldFront.SetActive(true);
                return;
            }
            else
            {
                _shieldFront.SetActive(false);
                if(shieldProcentage <= 0f)
                {
                    _hullCollider.enabled = true;
                    _shieldCollider.enabled = false;
                    _shieldRing.SetActive(false);
                }
                else
                {
                    _hullCollider.enabled = false;
                    _shieldCollider.enabled = true;
                    _shieldRing.SetActive(true);
                }
            }
        }
    }

    private float CalculateTime(float rechargeAmount)
    {
        timeBetweenRecharge = 1f / rechargeAmount;

        return timeBetweenRecharge;
    }

    IEnumerator WaitSomeTime(float time)
    {
        yield return new WaitForSeconds(time);
        RechargeShield();
        isTimerRunning = false;
    }

    private void RechargeShield()
    {
        _shipShield += 1;
    }
}
