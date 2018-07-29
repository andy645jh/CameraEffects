using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {

#region public properties
	public Transform player;	
	public Transform patrolEnemy;
	public Transform rayTarget;
	public float distanceToDetectplayer = 100;
#endregion

#region private properties
	private Transform[] _positions;
	private NavMeshAgent _agent;
	private int _counter = 0;
	private bool _isRotating = false;
	private float _rotationSpeed = 300f;	
	private RaycastHit _hit;
	private bool _isFollowPlayer = true;
#endregion

	void Start () {
		_positions = patrolEnemy.GetComponentsInChildren<Transform>();
		_agent = GetComponent<NavMeshAgent>();			
		_agent.SetDestination(_positions[_counter].position);		
	}
	
	IEnumerator RotateAgent(Quaternion currentRotation, Quaternion targetRotation) {
		_isRotating = true;
		while(currentRotation != targetRotation) {
			transform.rotation = Quaternion.RotateTowards(currentRotation, targetRotation, _rotationSpeed * Time.deltaTime);
			yield return 1;
		}
		_isRotating = false;
	}

	private void moverPlayer(){
		float v = Input.GetAxisRaw("Vertical");
		float h = Input.GetAxisRaw("Horizontal");
		
		Vector3 pos = player.transform.position;
		pos.z+= (10 * v * Time.deltaTime);
		pos.x+= (10 * h * Time.deltaTime);
		player.transform.position = pos;
	}

	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.F)){
			player.position = new Vector3(-25,player.position.y, 20);
		}	
		moverPlayer();
		float dist=_agent.remainingDistance;
		
        if (Physics.Raycast(rayTarget.position, transform.forward, out _hit, distanceToDetectplayer))
        {
			Debug.Log("Hit: "+_hit.transform.name);
            if (_hit.transform.CompareTag("Player"))
            {
				if(!_isFollowPlayer){
					_agent.speed += 10; 
					_agent.SetDestination(player.position);
					_isFollowPlayer = true;
				}
			              
				Debug.Log("Se encontro al Player");
				Debug.DrawRay(rayTarget.position, transform.forward * Vector3.Distance(_hit.point,transform.position), Color.green);
			
            }else{
				Debug.Log("esta colisionando con Muros");
				if(_isFollowPlayer){
					_agent.speed = 3.5f; 
					_agent.SetDestination(_positions[_counter].position);
					_isFollowPlayer = false;
				}
				Debug.DrawRay(rayTarget.position, transform.forward * Vector3.Distance(_hit.point,transform.position), Color.yellow);
			}
        }
        else
        {
			Debug.Log("No esta colisionando");
			Debug.DrawRay(rayTarget.position, transform.forward * distanceToDetectplayer, Color.red);        
        }

		if (dist!=Mathf.Infinity && _agent.pathStatus==NavMeshPathStatus.PathComplete && _agent.remainingDistance==0) {
			Debug.Log("Llego");
			_counter++;
			if(_counter>=_positions.Length){
				_counter=0;
			}			
			
			/*Vector3 relativePos = (positions[_counter].position - transform.position).normalized;
			Quaternion _lookRotation = Quaternion.LookRotation(relativePos);
			StartCoroutine(RotateAgent(transform.rotation, _lookRotation));
			if (!_isRotating) //check if unit is in line with direction of hit.point
    		{		
				Debug.Log("Iniciar rutina");					
				_agent.SetDestination(positions[_counter].position);	
			}	*/

			_agent.SetDestination(_positions[_counter].position);
		}
	}
}
