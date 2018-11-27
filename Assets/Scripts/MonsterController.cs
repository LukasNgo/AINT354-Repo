using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour {

    [SerializeField]
    private float wanderRadius = 1000;
    [SerializeField]
    private float wanderTimer = 10;
    [SerializeField]
    private float detectionRange = 200;
    [SerializeField]
    private int damageDeal = 50;
    [SerializeField]
    private Transform _player;
    [SerializeField]
    private EcholocationManager echolocation;
    [SerializeField]
    private float attackRange = 5f;

    private Transform target;
    private NavMeshAgent _agent;
    private float _timer;
    private RaycastHit _raycastHit;
    private Transform tempDestination;
    private bool isTimeOut = false;

    public bool isFollowing = false;

    private bool _transparencyBool = false;

    void OnEnable()
    {
        _agent = GetComponent<NavMeshAgent>();
        _timer = wanderTimer;
    }

    void Update()
    {

        //Debug.Log("monster destination " + _agent.destination);
        //Debug.Log("monster position " + _agent.transform.position);
        //Debug.Log("player position " + _player.position);

        detectPlayer();

        _timer += Time.deltaTime;

        if (_transparencyBool)
        {
            if (Shader.GetGlobalFloat("_Transparency") < 1)
            {
                Shader.SetGlobalFloat("_Transparency", (Mathf.Lerp(Shader.GetGlobalFloat("_Transparency"), 1, Time.deltaTime * 4)));

                if (Shader.GetGlobalFloat("_Transparency") > 0.9)
                {
                    Shader.SetGlobalFloat("_Transparency", 1);
                }
            }

            if (Shader.GetGlobalFloat("_TransparencyMaterial") < 1)
            {
                Shader.SetGlobalFloat("_TransparencyMaterial", (Mathf.Lerp(Shader.GetGlobalFloat("_TransparencyMaterial"), 1, Time.deltaTime * 4)));

                if (Shader.GetGlobalFloat("_TransparencyMaterial") > 0.9)
                {
                    Shader.SetGlobalFloat("_TransparencyMaterial", 1);
                }
            }
        }
        else
        {
            if (Shader.GetGlobalFloat("_Transparency") > 0)
            {
                Shader.SetGlobalFloat("_Transparency", (Mathf.Lerp(Shader.GetGlobalFloat("_Transparency"), 0, Time.deltaTime * 2)));

                if (Shader.GetGlobalFloat("_Transparency") < 0.02)
                {
                    Shader.SetGlobalFloat("_Transparency", 0);
                }
            }

            if (Shader.GetGlobalFloat("_TransparencyMaterial") > 0)
            {
                Shader.SetGlobalFloat("_TransparencyMaterial", (Mathf.Lerp(Shader.GetGlobalFloat("_TransparencyMaterial"), 0, Time.deltaTime * 2)));

                if (Shader.GetGlobalFloat("_TransparencyMaterial") < 0.02)
                {
                    Shader.SetGlobalFloat("_TransparencyMaterial", 0);
                }
            }
        }

        //walking speed
        if (isFollowing == false && isTimeOut == false)
        {
            _agent.speed = 1.5f;
            GetComponent<Animator>().SetTrigger("MonsterWalk");
        }

        //running speed
        if (isFollowing == true && isTimeOut == false)
        {
            _agent.speed = 4f;
            GetComponent<Animator>().SetTrigger("MonsterRun");
        }

        //if monster is not following sound or player then roam randomly
        if (_timer >= wanderTimer && !isFollowing)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            _agent.SetDestination(newPos);
            _timer = 0;
        }

        //if monster is very close to player, attack the player and follow until player is out of reach
        if (Vector3.Distance(transform.position, _player.position) < attackRange)
        {
            
            _agent.SetDestination(_player.position);
            if (!isTimeOut)
            {
                
                if (Vector3.Distance(transform.position, _player.position) < 3)
                {
                    isTimeOut = true;
                    GetComponent<Animator>().SetTrigger("MonsterAttack");
                    StartCoroutine(AttackPlayer(2));
                }
            }
        }

        //check if monster has a destination and give it a new one if it doesn't
        if(_agent.destination == _agent.transform.position)
        {
            isFollowing = false;
            //GetComponent<Animator>().SetTrigger("MonsterWalk");
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            _agent.SetDestination(newPos);
            _timer = 0;
        }
    }

    //function to attack player, deal damage and wait x second between attacks
    private IEnumerator AttackPlayer(float time)
    {
        _player.GetComponent<Player>().TakeDamage(damageDeal);
        yield return new WaitForSeconds(time);
        isTimeOut = false;
    }

    //calculate random navmesh destination
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    //detect range from player to trigger echolocation
    private void detectPlayer()
    {
        
        if (Physics.Raycast(transform.position, (_player.position - transform.position), out _raycastHit, detectionRange))
        {
            if (_raycastHit.transform == _player || Vector3.Distance(transform.position, _player.position) < 10f)
            {
                echolocation.isEcholocationActive = true;
                if (!_transparencyBool)
                {
                    _transparencyBool = true;
                }
                    
            }
            else
            {
                echolocation.isEcholocationActive = false;
                if (_transparencyBool)
                {
                    _transparencyBool = false;
                }
            }
        }      
    }

    //Use this from other gameobject to set the destination to it: GameObject.FindGameObjectWithTag("Enemy").GetComponent<MonsterController>().SetNewDestination(GetComponent<Transform>());
    public void SetNewDestination(Transform newDest)
    {
        isFollowing = true;
        tempDestination = newDest;
        _agent.SetDestination(tempDestination.position);
    }

}
