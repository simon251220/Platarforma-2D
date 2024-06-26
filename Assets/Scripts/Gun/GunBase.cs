using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{

    public ProjectileBase projectilePrefab;
    public Transform positionToShoot;
    public Transform playerSideReference;
    public float timeBetWeenShoot = .3f;
    private Coroutine _shootCoroutine;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
           _shootCoroutine = StartCoroutine(StartShoot());
        }
        else if(Input.GetKeyUp(KeyCode.K))
        {
            if(_shootCoroutine != null)
            {
                StopCoroutine(_shootCoroutine);
            }
        }
    }

    IEnumerator StartShoot()
    {
        while(true)
        {
            Shoot();
            yield return new WaitForSeconds(timeBetWeenShoot);
        }
    }

    public void Shoot()
    {
        var projectile = Instantiate(projectilePrefab);
        projectile.transform.position = positionToShoot.position;
        projectile.side = playerSideReference.transform.localScale.x;
    }
}
