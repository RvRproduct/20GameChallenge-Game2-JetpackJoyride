using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemProjectile : BasePoolObject
{
    [SerializeField] private float speed = 8;
    protected override string ProvidePoolReturnTag()
    {
        return PoolTags.ProjectileReturnTags.ProjectileReturn;
    }

    protected override string ProvidePoolTag()
    {
        return PoolTags.ProjectileTags.GemProjectile;
    }

    private void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {


        if (collision.gameObject.tag == "Enemy")
        {
            if (!ProjectileManager.Instance.GetPlayerController().IsShieldActive())
            {
                ProjectileManager.Instance.GetPlayerController().ActivateShield();
            }

            // Return Objects To Pool
            collision.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
