using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Scanner))]
[RequireComponent(typeof(Collider))]
public class Base : MonoBehaviour
{
    private Scanner _scanner;
    private Queue<Drone> _drones = new();
    private List<Mineral> _mineralsInProcess = new();

    private void OnEnable()
    {
        _scanner.MineralFound += DistributeMineral;
    }
    
    private void OnDisable()
    {
        _scanner.MineralFound -= DistributeMineral;
    }

    private void Awake()
    {
        _scanner = GetComponent<Scanner>();

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent(out Drone drone))
                _drones.Enqueue(drone);
        }
    }

    private void DistributeMineral(Mineral mineral)
    {
        if (_mineralsInProcess.Contains(mineral))
            return;
        
        if (_drones.TryDequeue(out Drone drone) == false)
            return;
        
        drone.SetMineral(mineral);
        _mineralsInProcess.Add(mineral);
    }
}
