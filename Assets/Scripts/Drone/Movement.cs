using UnityEngine;

public class Movement : MonoBehaviour
{
    private Transform _target;
    private Vector3 _movingPosition;
    private float _speed = 2f;
    
    private void Update()
    {
        if (_target is null)
            return;
        
        transform.position = Vector3.MoveTowards(transform.position, _movingPosition,
            _speed * Time.deltaTime);
    }
    
    public void Route(Transform target)
    {
        _target = target;
        _movingPosition = new Vector3(_target.position.x, transform.position.y, _target.position.z);
        transform.rotation = Quaternion.LookRotation(_movingPosition - transform.position);
    }
    
    public void ResetTarget()
    {
        _target = null;
    }
}
