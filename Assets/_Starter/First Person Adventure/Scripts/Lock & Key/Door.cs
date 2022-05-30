using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] KeyType doorType;
    [SerializeField] bool removeUsedKey = true;

    Animator anim;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        Inventory playerInventory = other.GetComponent<Inventory>();

        if (playerInventory == null) { return; }
        if (anim == null) { return; }

        if (playerInventory.IsHoldingKey(doorType))
        {
            anim.SetBool("isOpen", true);
            if (removeUsedKey)
            {
                playerInventory.RemoveKey(doorType);
            }
        }
    }
}
