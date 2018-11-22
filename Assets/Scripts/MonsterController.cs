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
        detectPlayer();

        _timer += Time.deltaTime;

        //Debug.Log(_transparencyBool);
        //Debug.Log(Shader.GetGlobalFloat("_Transparency"));

        if (_transparencyBool)
        {
            if (Shader.GetGlobalFloat("_Transparency") < 1)
            {
                Shader.SetGlobalFloat("_Transparency", (Mathf.Lerp(Shader.GetGlobalFloat("_Transparency"), 1, Time.deltaTime / 3)));
                //Debug.Log(Shader.GetGlobalFloat("_Transparency"));

                if (Shader.GetGlobalFloat("_Transparency") > 0.9)
                {
                    Shader.SetGlobalFloat("_Transparency", 1);
                }
            }

            if (Shader.GetGlobalFloat("_TransparencyMaterial") < 1)
            {
                Shader.SetGlobalFloat("_TransparencyMaterial", (Mathf.Lerp(Shader.GetGlobalFloat("_TransparencyMaterial"), 1, Time.deltaTime / 3)));

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
                Shader.SetGlobalFloat("_Transparency", (Mathf.Lerp(Shader.GetGlobalFloat("_Transparency"), 0, Time.deltaTime / 3)));
                //Debug.Log(Shader.GetGlobalFloat("_Transparency"));

                if (Shader.GetGlobalFloat("_Transparency") < 0.1)
                {
                    Shader.SetGlobalFloat("_Transparency", 0);
                }
            }

            if (Shader.GetGlobalFloat("_TransparencyMaterial") > 0)
            {
                Shader.SetGlobalFloat("_TransparencyMaterial", (Mathf.Lerp(Shader.GetGlobalFloat("_TransparencyMaterial"), 0, Time.deltaTime / 3)));

                if (Shader.GetGlobalFloat("_TransparencyMaterial") < 0.1)
                {
                    Shader.SetGlobalFloat("_TransparencyMaterial", 0);
                }
            }
        }

        //walking speed
        if (isFollowing == false)
        {
            _agent.speed = 1.5f;
            GetComponent<Animator>().SetTrigger("MonsterWalk");
        }

        //running speed
        if (isFollowing == true)
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

        ////if monster is following something then reaches the desired destination then stop following and set animation to walking
        //if (isFollowing && Vector3.Distance(transform.position, tempDestination.position) < 2)
        //{
        //    isFollowing = false;
        //    GetComponent<Animator>().SetTrigger("MonsterWalk");
        //}

        //if monster is very close to player, attack the player and follow until player is out of reach
        if (Vector3.Distance(transform.position, _player.position) < 2)
        {
            
            _agent.SetDestination(_player.position);
            if (!isTimeOut)
            {
                GetComponent<Animator>().SetTrigger("MonsterAttack");
                isTimeOut = true;
                StartCoroutine(AttackPlayer(2));
            }
            else
            {
                GetComponent<Animator>().SetTrigger("MonsterWalk");
            }
            
        }

        //check if monster is standing still and if so then give it new destination
        if (_agent.velocity.x == 0.0 && _agent.velocity.y == 0.0 && _agent.velocity.z == 0.0)
        {
            isFollowing = false;
            GetComponent<Animator>().SetTrigger("MonsterWalk");
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            _agent.SetDestination(newPos);
            _timer = 0;
        }

        Debug.Log(isFollowing);
        Debug.Log(_agent.velocity);
    }

    //function to attack player, deal damage and wait x second between attacks
    private IEnumerator AttackPlayer(float time)
    {
        _player.GetComponent<Player>().TakeDamage(damageDeal);
        yield return new WaitForSeconds(time);
        isTimeOut = false;
        GetComponent<Animator>().SetTrigger("MonsterWalk");
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
        if (Vector3.Distance(transform.position, _player.position) < detectionRange)
        {
            if (Physics.Raycast(transform.position, (_player.position - transform.position), out _raycastHit, detectionRange))
            {
                if (_raycastHit.transform == _player)
                {
                    echolocation.isEcholocationActive = true;
                    //Debug.Log("Echolocation on.");
                    if (!_transparencyBool)
                    {
                        _transparencyBool = true;
                    }
                    
                }
            }
        }
        else
        {
            //Debug.Log("Echolocation off.");
            echolocation.isEcholocationActive = false;
            if(_transparencyBool)
            {
                _transparencyBool = false;
            }
        }
    }

    //Use this from other gameobject to set the destination to it: GameObject.FindGameObjectWithTag("Enemy").GetComponent<MonsterController>().SetNewDestination(GetComponent<Transform>());
    public void SetNewDestination(Transform newDest)
    {
        isFollowing = true;
        tempDestination = newDest;
        _agent.SetDestination(tempDestination.position);
        GetComponent<Animator>().SetTrigger("MonsterRun");
    }

}
