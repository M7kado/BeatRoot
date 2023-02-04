using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck : MonoBehaviour, Iinteractable
{
    public void Interact()
    {
        GameManager.Instance.BeetrootCollected += PlayerManager.Instance.StoredBeets;
        PlayerManager.Instance.StoredBeets = 0;
    }
}
