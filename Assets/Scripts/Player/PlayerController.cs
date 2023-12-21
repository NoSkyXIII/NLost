using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [SerializeField] private AudioClip[] _sounds;
    [SerializeField] private GameObject[] _tools;
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float gravityValue = -9.81f;
    private Animator _animator;
    private CharacterController _controller;
    private Vector3 _playerVelocity;
    private PlayerInput _playerInput;

    public void SetTool(int toolNumber)
    {
        foreach (var tool in _tools)
        {
            tool.SetActive(false);
        }

        _tools[toolNumber].SetActive(true);
    }

    public void PlaySound(int number)
    {
        AudioManager.Instance.PlaySound(_sounds[number]);
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        _playerInput = new PlayerInput();
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void Update()
    {
        Vector2 movementInput = _playerInput.Control.Move.ReadValue<Vector2>();
        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);

        _controller.Move(move * Time.deltaTime * playerSpeed);
        _animator.SetFloat("Speed", _controller.velocity.magnitude);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        _playerVelocity.y += gravityValue * Time.deltaTime;
        _controller.Move(_playerVelocity * Time.deltaTime);
    }
}