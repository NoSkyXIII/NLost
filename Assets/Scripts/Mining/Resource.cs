using UnityEngine;
using DG.Tweening;
using System.Collections;

public class Resource : MonoBehaviour, IMineble
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private GameObject _resourceBlock;
    [SerializeField] private float _dropDelay;
    [SerializeField] private string _animationName;
    [SerializeField] private float _fullRestoreTime;
    [SerializeField] private Mesh[] _meshes;
    [SerializeField] private MeshFilter _meshFilter;
    [SerializeField] private ReductionSlider _reductionSlider;

    private int _animationHash;
    private int _currentStrength;
    private int _strength;
    private bool _isActive = true;
    private float _timeToRestoreLeft = 0;

    public int Mine(int minePower, Vector3 position)
    {
        _currentStrength -= minePower;

        StartCoroutine(DropResource(position));

        if (_currentStrength <= 0)
            _isActive = false;

        return _animationHash;
    }

    public bool IsActiveCheck()
    {
        return _isActive;
    }

    private void Start()
    {
        _animationHash = Animator.StringToHash(_animationName);
        _strength = _meshes.Length;
        _currentStrength = _strength - 1;
    }

    private IEnumerator DropResource(Vector3 position)
    {
        yield return new WaitForSeconds(_dropDelay);

        Vector3 instantiatePostion = new Vector3(0, 1, 0);
        Vector3 direction = (position - transform.position).normalized;
        GameObject miniblock = Instantiate(_resourceBlock, transform.position + instantiatePostion, Quaternion.identity);
        miniblock.GetComponent<Rigidbody>().AddForce(direction * 5f, ForceMode.Impulse);

        _particleSystem.Play();
        _meshFilter.mesh = _meshes[_strength - _currentStrength - 1];
        transform.DOShakeScale(0.3f, 0.3f);

        if (_currentStrength <= 0)
            _reductionSlider.gameObject.SetActive(true);
    }

    private void Restore()
    {
        _reductionSlider.gameObject.SetActive(false);
        _isActive = true;
        _meshFilter.mesh = _meshes[0];
        _currentStrength = _strength - 1;
        transform.DOShakeScale(0.3f, 0.3f);
    }

    private void Update()
    {
        if (_isActive)
            return;

        _timeToRestoreLeft += Time.deltaTime;

        _reductionSlider.Set(_timeToRestoreLeft, _fullRestoreTime);

        if( _timeToRestoreLeft >= _fullRestoreTime)
        {
            _timeToRestoreLeft = 0;
            Restore();
        }
    }
}
