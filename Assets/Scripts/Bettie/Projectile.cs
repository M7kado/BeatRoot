using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Clockable
{
    public override void Action()
    {
        transform.position += new Vector3(-1, 0, 0);

        if (transform.position.x < MapManager.Instance.renderOffset.x)
        {
            // Clock.Instance.Unregister(this);
            // Destroy(gameObject);
            gameObject.SetActive(false);
        }
        if (transform.position == (Vector3)(PlayerManager.Instance.pos 
                                + MapManager.Instance.renderOffset))
        {
            PlayerManager.Instance.stunedFrame = Clock.Instance.Timer;
            // Clock.Instance.Unregister(this);
            // Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }
}
