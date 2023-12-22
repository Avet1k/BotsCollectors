using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Scanner))]
public class Base : MonoBehaviour
{
    private Scanner _scanner;
    private Queue<Drone> _drones = new();
    private List<Mineral> _mineralsInProcess = new();
    private int _resources;

    private void OnEnable()
    {
        _scanner.MineralFound += DistributeMineral;
        
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent(out Drone drone))
            {
                _drones.Enqueue(drone);
                drone.MineralGiven += TakeMineral;
            }
        }
    }
    
    private void OnDisable()
    {
        _scanner.MineralFound -= DistributeMineral;

        foreach (Drone drone in _drones)
            drone.MineralGiven -= TakeMineral;
    }

    private void Awake()
    {
        _scanner = GetComponent<Scanner>();
    }

    private void DistributeMineral(Mineral mineral)
    {
        if (_mineralsInProcess.Contains(mineral))
            return;
        
        if (_drones.TryDequeue(out Drone drone) == false)
            return;
        
        drone.ExtractMineral(mineral);
        _mineralsInProcess.Add(mineral);
    }

    private void TakeMineral(Mineral mineral, Drone drone)
    {
        Destroy(mineral.gameObject);
        _drones.Enqueue(drone);
        _mineralsInProcess.Remove(mineral);
        _resources++;
        
        Debug.Log(_resources);
    }
}
