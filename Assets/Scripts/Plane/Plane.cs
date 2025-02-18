using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Serialization;

public class Plane : MonoBehaviour
{
    PlaneGameManager planeGameManager;
    
    private static readonly int IsDie = Animator.StringToHash("isDie");
    
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    // Start is called before the first frame update

    public float flapForce = 6f;
    public float forwardSeed = 3f;
    public bool isDead = false;
    public float deathCoolDown = 0f;

    private bool _isFlap = false;
    [FormerlySerializedAs("godMode")] public bool infiniteMode = false;
    
    void Start()
    {
        // 게임 매니저 접근
        planeGameManager = PlaneGameManager.Instance;
        
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();

        if (!_rigidbody && !_animator) { Debug.LogWarning("No components found!"); }
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            if (deathCoolDown <= 0)
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    // 게임 재시작
                    PlaneGameManager.Instance.RestartGame();
                }
            }
            else
            {
                deathCoolDown -= Time.deltaTime;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            _isFlap = true;
        }
        
    }

    private void FixedUpdate()
    {
        if(isDead) { return; }
        
        Vector3 velocity = _rigidbody.linearVelocity;
        velocity.x = flapForce;

        if (_isFlap)
        {
            velocity.y += flapForce;
            _isFlap = false;
        }
        
        _rigidbody.linearVelocity = velocity;
        
        float angle = Mathf.Clamp(_rigidbody.linearVelocity.y * 10f, -90f, 90f);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    
    // 충돌은 콜리전 또는 트리거(트리거는 물리 충돌은 발생 X)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(infiniteMode) { return; }
        if(isDead) { return; }
        
        isDead = true;
        deathCoolDown = 1f;
        
        _animator.SetBool(IsDie, true);
        
        // 게임 오버
        PlaneGameManager.Instance.GameOver();
    }
}
