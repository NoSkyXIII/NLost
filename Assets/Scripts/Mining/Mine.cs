using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] LayerMask _resourceLayer;
    [SerializeField] private float _radius;
    [SerializeField] private Animator _animator;

    [Header("Tools")]
    [SerializeField] private GameObject _axe;
    [SerializeField] private GameObject _pickAxe;

    [Header("Mining Power")]
    [SerializeField] private int _miningPower;

    [Header("PlayerController")]
    [SerializeField] private PlayerController _playerController;

    private float _mineSpeed = 1.5f;
    private float _timeBetweenLastMine = 3f;

    private void Update()
    {
        _timeBetweenLastMine += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        Collider[] resources = Physics.OverlapSphere(transform.position, _radius, _resourceLayer);

        if (resources != null && _mineSpeed <= _timeBetweenLastMine)
        {
            foreach (Collider resource in resources)
            {
                if (resource.TryGetComponent(out IMineble mineble))
                {
                    if (mineble.IsActiveCheck())
                    {
                        int animation = mineble.Mine(_miningPower, transform.position);

                        _animator.SetTrigger(animation);

                        _timeBetweenLastMine = 0;
                    }
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position, _radius);
        Gizmos.color = Color.yellow;
    }
}