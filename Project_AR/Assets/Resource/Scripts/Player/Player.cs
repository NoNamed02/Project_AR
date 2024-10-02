using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 플레이어의 상태를 저장하는 함수
    private enum State
    {
        Idle,
        Move,
        Jump,
        Interactive,
        Damaged
    }
    public static Player Instance { get { return Instance; } }
    public PlayerStateMachine stateMachine { get;private set; }
    public Rigidbody rigidBody { get; private set; }
    public Animator animator { get; private set; }
    public CapsuleCollider capsuleCollider { get; private set; }

    private static Player instance;

    #region #캐릭터 스탯
    public float MaxHP      {  get { return MaxHP; } }
    public float CurrentHP  { get { return currentHP; } }
    public float Armor      { get { return armor; } }
    public float MoveSpeed  { get { return moveSpeed; } }
    public int DashCount    { get { return dashCount; } }
    public int JumpCount    {  get { return jumpCount; } }


    [Header("캐릭터 스탯")]
    [SerializeField] protected float maxHP;
    [SerializeField] protected float currentHP;
    [SerializeField] protected float armor;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected int dashCount;
    [SerializeField] protected int jumpCount;
    #endregion

    #region #Unity 함수
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            rigidBody = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
            capsuleCollider = GetComponent<CapsuleCollider>();
            DontDestroyOnLoad(gameObject);
            return;
        }
        DestroyImmediate(gameObject);
    }

    private void Start()
    {
        InitStateMachine();
    }

    private void Update()
    {
        stateMachine?.UpdateState();
    }
    private void FixedUpdate()
    {
        stateMachine?.FixedUpdateState();
    }
#endregion

    public void OnUpdateStat(float maxHp,float currentHp,float armor, float moveSpeed, int dashCount, int jumpCount)
    {
        this.maxHP = maxHp;
        this.currentHP = currentHp;
        this.armor = armor;
        this.moveSpeed = moveSpeed;
        this.dashCount = dashCount;
        this.jumpCount = jumpCount;
    }

    private void InitStateMachine()
    {

    }
}