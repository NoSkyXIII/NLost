using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giver : MonoBehaviour
{
    [SerializeField] private InventoryModel _inventoryModel;
    [SerializeField] private float _delay;

    private bool _isInTrigger;
    private ITakeble _takeble;
    private float _timer;
    private bool _isUsing;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out ITakeble takeble))
        {
            _isInTrigger = true;
            _takeble = takeble;
            _timer = 0f;
            _isUsing = false;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.TryGetComponent(out ITakeble takeble))
        {
            _isInTrigger = false;
            takeble.StopTake();
        }
    }

    private void Update()
    {
        if (_isInTrigger && !_isUsing)
        {
            _timer += Time.deltaTime;

            if (_timer >= _delay)
            {
                _takeble.Take(_inventoryModel);
                _isUsing = true;
            }
        }
    }
}