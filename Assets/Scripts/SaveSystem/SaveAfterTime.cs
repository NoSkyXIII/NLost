using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAfterTime : MonoBehaviour
{
    [SerializeField] private float saveInterval = 15f;
    private float timer = 0f;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= saveInterval)
        {
            SaveSystem.Instance.SaveAll();
            timer = 0f;
        }
    }
}
