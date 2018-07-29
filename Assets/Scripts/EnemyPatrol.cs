using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour {

    public Transform posPlayer;
    public NavMeshAgent monster;
    public GameObject [] puntosDePatrulla;

    Ray ray;
    RaycastHit hit;

    public float enemyDetectePlayer = 6;

    bool viewPlayer = false;
    bool playerDetecteRaycast = false;

	// Use this for initialization
	void Start () {
        posPlayer = GameObject.Find("Player").transform;
        SetDestination(puntosDePatrulla[0].GetComponent<Transform>());
    }
	
	// Update is called once per frame
	void Update () {
        // SetDestination(posPlayer);
        Debug.DrawRay(gameObject.transform.position, transform.forward * 30, Color.green);
        if (Physics.Raycast(gameObject.transform.position, transform.forward, out hit, enemyDetectePlayer))
        {
           
            if (hit.transform.CompareTag("Player"))
            {
                if (!playerDetecteRaycast)
                {
                    Debug.Log("player Raycast Detecte");
                    viewPlayer = true;
                    playerDetecteRaycast = true;
                }
            }
        }
        else
        {
            if (playerDetecteRaycast)
            {
                Debug.Log("player Raycast no Detecte");
                viewPlayer = false;
                SetDestination(puntosDePatrulla[Random.Range(0,puntosDePatrulla.Length)].GetComponent<Transform>());
                playerDetecteRaycast = false;
            }
            
        }
        //-----------------------------------------------------------------------------------------
        if (viewPlayer)
        {
            Vector3 lookVector = posPlayer.transform.position - transform.position;
            lookVector.y = transform.position.y;
            Quaternion rot = Quaternion.LookRotation(lookVector);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, 1);
            SetDestination(posPlayer);
        }
        
    }

    public void SetDestination(Transform newPath)
    {
        monster.SetDestination(newPath.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        return;
        if(viewPlayer == false)
        {
            if (other.gameObject.CompareTag("patrulla1"))
            {
                for (int i = 0; i < puntosDePatrulla.Length; i++)
                {
                    if (puntosDePatrulla[i] == other.gameObject)
                    {
                        if (i == 0)
                        {
                            for (int q = 0; q < puntosDePatrulla.Length; q++)
                            {
                                puntosDePatrulla[q].SetActive(true);
                            }
                        }
                        if (i >= puntosDePatrulla.Length - 1)
                        {
                            puntosDePatrulla[0].SetActive(true);
                            SetDestination(puntosDePatrulla[0].GetComponent<Transform>());
                        }
                        else
                        {
                            if (i + 1 > puntosDePatrulla.Length)
                            {
                                i = -1;
                            }
                            puntosDePatrulla[i].SetActive(false);
                            SetDestination(puntosDePatrulla[i + 1].GetComponent<Transform>());
                        }
                    }
                }
            }
        }
    }
}
