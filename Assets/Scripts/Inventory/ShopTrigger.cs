using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _shopPanel;

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.TryGetComponent(out PlayerController player))
        {
            _shopPanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.TryGetComponent(out PlayerController player))
        {
            _shopPanel.SetActive(false);
        }
    }
}
