using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Princess : FallingObjects
{
    public override void Collision(GameObject hitObject)
    {
        base.Collision(hitObject);

        if (hitObject.CompareTag("Ground"))
        {
            LosePrincess();
        }
    }

    private void LosePrincess()
    {
        GameManager.m_Instance.GetScoreSystem.LosePrincess();
    }
}
