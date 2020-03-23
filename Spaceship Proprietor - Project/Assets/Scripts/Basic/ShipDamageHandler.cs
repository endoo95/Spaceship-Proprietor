using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDamageHandler : MonoBehaviour, IShip
{
    [SerializeField] private ShipScriptableObject _shipScriptableObject;
    [SerializeField] private Transform _shieldObject;
    [SerializeField] private GameObject _shipObject;

    private GameObject _shieldBack;
    private GameObject _shieldFront;
    private GameObject _shieldRing;

    private CircleCollider2D _shieldCollider;
    private PolygonCollider2D _hullCollider;

    [SerializeField] private float _shipHull;
    [SerializeField] private float _shipArmor;
    [SerializeField] private int _shipShield;
    [SerializeField] private float _shieldRecharge;

    private float timeBetweenRecharge;
    private float countdown;
    private bool isTimerRunning = false;

    private void Awake()
    {
        if (_shipScriptableObject == null || _shieldObject == null || _shipObject == null)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _shieldBack = _shieldObject.Find("Back").gameObject;
        _shieldFront = _shieldObject.Find("Front").gameObject;
        _shieldRing = _shieldObject.Find("Ring").gameObject;

        _shieldCollider = _shipObject.GetComponent<CircleCollider2D>();
        _hullCollider = _shipObject.GetComponent<PolygonCollider2D>();

        _shipArmor = _shipScriptableObject.maxArmor;
        _shipShield = _shipScriptableObject.maxShield;
        _shipHull = _shipScriptableObject.maxHealth;
        _shieldRecharge = _shipScriptableObject.shieldRecharge;

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
                if (damageLeft <= 0)
                {
                    return;
                }
                damage = damageLeft;
            }
            ModifyHealth(damage);
        }
        if (_shipHull <= 0)
        {
            Destroy(gameObject);
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
        return _shipArmor;
    }

    public float ModifyHealth(float damage)
    {
        _shipHull -= damage;
        return _shipHull;
    }

    private void ShieldStatus(int shields)
    {
        float maxShield = (float)_shipScriptableObject.maxShield;
        float shieldProcentage;

        shieldProcentage = (shields / maxShield) * 100f;

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
