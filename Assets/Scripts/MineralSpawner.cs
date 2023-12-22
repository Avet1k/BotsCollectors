using System.Collections;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class MineralSpawner : MonoBehaviour
{
    [SerializeField] private Mineral _mineral;
    [SerializeField] private Ground _ground;

    private float _size = 4.5f;
    private float _delay = 3f;

    private void OnEnable()
    {
        StartCoroutine(SpawningMineral());
    }

    private IEnumerator SpawningMineral()
    {
        bool isWorking = true;
        float fullRotate = 360;
        WaitForSeconds delay = new WaitForSeconds(_delay);
        
        while (isWorking)
        {
            yield return delay;

            Mineral newMineral = Instantiate(
                _mineral,
                GenerateSpawnPosition(), 
                Quaternion.Euler(
                    Random.Range(0, fullRotate),
                    Random.Range(0, fullRotate),
                    Random.Range(0, fullRotate)));
            
            newMineral.transform.parent = transform;
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        float originHight = 20f;
        Vector3 position = _ground.transform.position;
        RaycastHit hitInfo;

        do
        {
            position.x = Random.Range(-_size, _size);
            position.z = Random.Range(-_size, _size);
            
            Vector3 rayOrigin = new Vector3(position.x, originHight, position.z);

            Physics.Raycast(rayOrigin, Vector3.down, out hitInfo);
        } while (hitInfo.transform.TryGetComponent(out Ground _) == false);
        
        return position;
    }
}
