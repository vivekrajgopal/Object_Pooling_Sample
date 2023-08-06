using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            FireBullet();
        }
    }

    private void FireBullet()
    {
        GameObject bullet = ObjectPoolManager.Instance.GetObject();

        StartCoroutine(ReturnBullet(bullet, 0.5f));
    }

    IEnumerator ReturnBullet(GameObject bullet, float timer)
    {
        yield return new WaitForSeconds(timer);
        ObjectPoolManager.Instance.ReturnObject(bullet);
    }
}
