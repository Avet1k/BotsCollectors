using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Movement))]
public class Drone : MonoBehaviour
{
    private Mineral _mineral;
    private Movement _movement;
    private Base _base;
    private Vector3 _positionCorrection;

    public event UnityAction<Mineral, Drone> MineralGiven;
    
    private void Start()
    {
        _movement = GetComponent<Movement>();
        _base = transform.GetComponentInParent<Base>();
        _positionCorrection = new Vector3(0, transform.position.y, 0);
    }
    
    private void FixedUpdate()
    {
        if (_mineral is null)
            return;

        if (transform.position == _mineral.transform.position + _positionCorrection)
        {
            TakeMineral();
            _movement.Route(_base.transform);
        }

        if (transform.position == _base.transform.position + _positionCorrection)
        {
            GiveMineral();
            _movement.ResetTarget();
        }
    }

    public void ExtractMineral(Mineral mineral)
    {
        _mineral = mineral;
        _movement.Route(_mineral.transform);
    }
    
    private void TakeMineral()
    {
        _mineral.transform.parent = transform;
        _mineral.transform.rotation = transform.rotation;
        _mineral.transform.localPosition = new Vector3(0, 0, 1);
    }

    private void GiveMineral()
    {
        MineralGiven?.Invoke(_mineral, this);
        _mineral = null;
    }
}
