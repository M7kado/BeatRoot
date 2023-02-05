using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour, Iinteractable
{
    public void Interact()
    {
        AnimationPlayer();
        

        PlayerManager.Instance.pos += PlayerManager.dirs[(int)Clock.Instance.playerAction];
    }

    void AnimationPlayer()
    {
        PlayerManager.playerAnimator.SetTrigger("trigger move");
    }
}
