// Game and Code By RvRproduct (Roberto Reynoso)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFloorCheck : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            PlayerAnimationManager.Instance.TryTriggerRun();
            PlayerAnimationManager.Instance.SetOnFloor(true);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            if (!PlayerAnimationManager.Instance.GetOnFloor())
            {
                PlayerAnimationManager.Instance.SetOnFloor(true);
            }

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            PlayerAnimationManager.Instance.SetOnFloor(false);
        }
    }
}
