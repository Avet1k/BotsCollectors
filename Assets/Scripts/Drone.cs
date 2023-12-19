using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    private float _speed = 2f;
    
    public void SetMineral(Mineral mineral)
    {
        StartCoroutine(Mining(mineral));
    }

    private IEnumerator Mining(Mineral mineral)
    {
        WaitForEndOfFrame delay = new WaitForEndOfFrame();
        Vector3 targetPosition =
            new Vector3(mineral.transform.position.x, transform.position.y, mineral.transform.position.z);

        transform.rotation = Quaternion.LookRotation(targetPosition - transform.position);
        
        while (transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition,
                _speed * Time.deltaTime);
            
            yield return null;
        }
    }
}
