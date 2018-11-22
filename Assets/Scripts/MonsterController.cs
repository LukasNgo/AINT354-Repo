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
    private Transform _player;
    [SerializeField]
    private EcholocationManager echolocation;

    private Transform target;
    private NavMeshAgent _agent;
    private float _timer;
    private RaycastHit _raycastHit;

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

        if (_timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            _agent.SetDestination(newPos);
            _timer = 0;
        }

        //Debug.Log(_transparencyBool);
        //Debug.Log(Shader.GetGlobalFloat("_Transparency"));

        if (_transparencyBool)
        {
            if (Shader.GetGlobalFloat("_Transparency") < 1)
            {
                Shader.SetGlobalFloat("_Transparency", (Mathf.Lerp(Shader.GetGlobalFloat("_Transparency"), 1, Time.deltaTime / 3)));
                //Debug.Log(Shader.GetGlobalFloat("_Transparency"));

                if(Shader.GetGlobalFloat("_Transparency") > 0.9)
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
            if(Shader.GetGlobalFloat("_Transparency") > 0)
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
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

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

}
