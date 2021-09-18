using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class EnemyAI : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Follow,
        Attack
    }


    //reference to charcontroller
    private CharacterController _enemyController;
    private Transform _player;
    [Range(1, 20)]
    [SerializeField] private float _speed;
    private Vector3 _direction;
    private Vector3 _velocity;
    [Range(1,10)]
    [SerializeField] private float _damper = 3;
    [SerializeField] EnemyState _currentState = EnemyState.Follow;
    [SerializeField] private bool _attacking = false;

    // Start is called before the first frame update
    void Start()
    {
        _enemyController = GetComponent<CharacterController>();
        if (_enemyController == null) Debug.LogError(transform.name + ":: CharacterController is null");

        _player = GameObject.FindGameObjectWithTag("Player").transform;
        if (_player == null)  Debug.LogError(transform.name + ":: Player transform is null");
    
    }

    // Update is called once per frame
    void Update()
    {
     /*   // stop following player if attacking 
        if (_currentState == EnemyState.Attack)
        {
            return;
        }
        if (_currentState == EnemyState.Follow)
        {
            FollowPlayer();
        }*/
        switch (_currentState)
        {
            case EnemyState.Idle:
                //idle walk around looking at stuff
                break;
            case EnemyState.Follow:
                FollowPlayer(); 
                break;
            case EnemyState.Attack:                
                break;
            default:
                break;
        }

        if (Vector3.Distance(_player.position, transform.position) < 10f)
        {
            _currentState = EnemyState.Follow;
        }

    }

    
    
    private void FollowPlayer()
    {
        if (_enemyController.isGrounded)
        {
            _direction = _player.position - transform.position;
            _direction.Normalize();
            _direction.y = 0;
            #region Different ways of looking at the target/player
            //always look at player
            //    transform.rotation = Quaternion.LookRotation(_direction);

            //slowly look towards the player
            var rotationAngle = Quaternion.LookRotation(_direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotationAngle, Time.deltaTime * _damper);
            #endregion
            _velocity = _direction * _speed;

        }
        _velocity.y = -0.4f;
        _enemyController.Move(_velocity * Time.deltaTime);

        if (Vector3.Distance( _player.position,transform.position) > 8f)
        {
            _currentState = EnemyState.Idle;
        }
        #region pseudo code
        //check if grounded
        //calculate direction = destination(player or target) - source (self(transform))
        //calculate velocity, direction * speed

        //subtract gravity
        //move to velocity
        #endregion
    }
    public void Attack(Transform transform)
    {
        if (!_attacking)
        {
            _attacking = true;
            StartCoroutine(AttackPlayer(transform));
        }
    }

    private IEnumerator AttackPlayer(Transform other)
    {    
        _currentState = EnemyState.Attack;
        while (_currentState == EnemyState.Attack)
        {
            IDamageable damage = other.GetComponent<IDamageable>();
            if (damage != null)
            {
                damage.Damage(10);
            }
            yield return new WaitForSeconds(1.5f);
        }
        
    }


    public void PlayerRunning()
    {      
        _currentState = EnemyState.Follow;
        _attacking = false;        
    }

}


