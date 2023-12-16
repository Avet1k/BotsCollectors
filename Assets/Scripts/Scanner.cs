using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Base))]
public class Scanner : MonoBehaviour
{
    private float _scanDelay = 0.05f;
    private float _distance = 7f;
    private float _deg = 3f;
    private float _rad;
    private Base _base;
    
    public event UnityAction MineralFound;
    
    private void OnEnable()
    {
        StartCoroutine(Scaning());
    }

    private void Start()
    {
        _rad = _deg * Mathf.Deg2Rad;
        _base = GetComponent<Base>();
    }

    private IEnumerator Scaning()
    {
        bool _isWorking = true;
        WaitForSeconds delay = new WaitForSeconds(_scanDelay);
        Vector3 direction = new Vector3(0, 0.1f, _distance);
        float newX;
        float newZ;
        RaycastHit hitInfo;

        while (_isWorking)
        {
            yield return delay;

            newX = direction.x * Mathf.Cos(_rad) - direction.z * Mathf.Sin(_rad);
            newZ = direction.x * Mathf.Sin(_rad) + direction.z * Mathf.Cos(_rad);

            direction = new Vector3(newX, direction.y, newZ);

            Physics.Raycast(_base.transform.position, direction, out hitInfo, _distance);
            Debug.DrawRay(_base.transform.position, direction, Color.red, 0.1f);

            if (hitInfo.transform is not null && hitInfo.transform.TryGetComponent(out Mineral _))
            {
                Debug.Log("Found Mineral!");
                MineralFound?.Invoke();
            }
        }
    }
}
