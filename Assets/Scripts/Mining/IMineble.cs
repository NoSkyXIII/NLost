using UnityEngine;

public interface IMineble
{
    public bool IsActiveCheck();
    public int Mine(int minePowe, Vector3 position);
}
