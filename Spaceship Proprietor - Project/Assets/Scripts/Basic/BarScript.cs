using UnityEngine;

public class BarScript : MonoBehaviour
{
    [SerializeField] private Transform _myTarget;
    [SerializeField] private Vector3 _offset;

    [SerializeField] private float _followSpeed = 1f;

    private Transform _bar;

    private void Awake()
    {
        _bar = transform.Find("Bar");
    }

    private void Update()
    {
        if(_myTarget != null)
        {
            Vector2 targetPosition = _myTarget.position + _offset;
            Vector2 smoothedPosition = Vector2.Lerp(transform.position, targetPosition, _followSpeed);
            transform.position = smoothedPosition;
        }
    }

    public void SetSize(float sizeNormalized)
    {
        _bar.localScale = new Vector3(sizeNormalized, 1);
    }

    public void SetColor(Color color)
    {
        _bar.Find("BarSprite").GetComponent<SpriteRenderer>().color = color;
    }
}
