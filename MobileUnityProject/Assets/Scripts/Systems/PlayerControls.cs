using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField]
    private Player m_Player;

    public void MovePlayerButton()
    {
        if (m_Player.transform.position.y + 0.9f > Camera.main.ScreenToWorldPoint(Input.mousePosition).y)
        {

            if (m_Player.transform.position.x + 0.08f < Camera.main.ScreenToWorldPoint(Input.mousePosition).x)
            {
                MovePlayerRight();
            }

            if (m_Player.transform.position.x - 0.08f > Camera.main.ScreenToWorldPoint(Input.mousePosition).x)
            {
                MovePlayerLeft();
            }
        }
    }

    //private void OnMouseDown()
    //{
    //    if (m_Player.transform.position.y + 1f < Camera.main.ScreenToWorldPoint(Input.mousePosition).y)
    //    {
    //        PressSwordThrow();
    //    }
    //}

    private void OnMouseDrag()
    {
        MovePlayerButton();
    }

    private void OnMouseUp()
    {
        ResetPlayer();
    }

    //private void PressSwordThrow()
    //{
    //    m_Player.ThrowSword();
    //}

    private void MovePlayerLeft()
    {
        m_Player.MoveLeft();
    }

    private void MovePlayerRight()
    {
        m_Player.MoveRight();
    }

    private void ResetPlayer()
    {
        m_Player.ResetPlayerToGround();
    }
}
