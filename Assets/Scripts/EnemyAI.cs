using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {

#region public properties
	public Transform player;	
	public Transform patrolEnemy;
	public Transform rayTarget;
	public float distanceFront = 30;
	public float distanceBack = 10;
	public float followTime= 5;
	public float maxAngle= 15;
#endregion

#region private properties
	private Transform[] _positions;
	private NavMeshAgent _agent;
	private int _counter = 0;
	private bool _isRotating = false;
	private float _rotationSpeed = 300f;	
	private RaycastHit _hitFront;
	private RaycastHit _hitBack;
	private bool _isFollowPlayer = true;
	private float _speed = 5;
	private Vector3 _pos;
	private bool _isWaiting = false;
	private Rigidbody _playerRigi;
	private float _inc = 0.1f;
	private float _factor = 1;
	public float _angle = 0;

#endregion

	void Start () {
		_positions = patrolEnemy.GetComponentsInChildren<Transform>();
		_agent = GetComponent<NavMeshAgent>();			
		_agent.SetDestination(_positions[_counter].position);		
		_playerRigi = player.GetComponent<Rigidbody>();		
		//_agent.Stop();
	}
	private void moverPlayer(){
		float v = Input.GetAxisRaw("Vertical");
		float h = Input.GetAxisRaw("Horizontal");		
		_playerRigi.velocity = new Vector3(10 * h, 0, 10 * v);		
	}

	// Update is called once per frame
	
	void Update () {
		
		var x = transform.forward.x*Mathf.Cos(Mathf.Deg2Rad*_angle) - transform.forward.z*Mathf.Sin(Mathf.Deg2Rad*_angle);
		var z = transform.forward.x*Mathf.Sin(Mathf.Deg2Rad*_angle) + transform.forward.z*Mathf.Cos(Mathf.Deg2Rad*_angle);
		_angle += (0.8f * _factor);
		//Debug.Log("Incremento: "+_angle);
		if(_angle>maxAngle){
			_factor = -1;
		}else if(_angle<-maxAngle){
			_factor = 1;
		}

		moverPlayer();

		float dist=_agent.remainingDistance;
		
		//bool detectPlayerFront = Physics.Raycast(rayTarget.position, transform.forward, out _hitFront, distanceFront);
		bool detectPlayerFront = Physics.Raycast(rayTarget.position, new Vector3(x,0,z), out _hitFront, distanceFront);
		bool detectPlayerBack = Physics.Raycast(rayTarget.position, -transform.forward, out _hitBack, distanceBack);
		

		if(_isWaiting){
			_agent.SetDestination(player.position);		
		}
		
		if ((detectPlayerBack && _hitBack.transform.CompareTag("Player")) || (detectPlayerFront && _hitFront.transform.CompareTag("Player")))
		{
			if(!_isFollowPlayer){
				//_agent.speed += 10; 
				_agent.SetDestination(player.position);
				_isFollowPlayer = true;
				if(!_isWaiting){
					_isWaiting = true;
					Invoke("checkPlayer",followTime);
				}
				
			}
			              
			Debug.Log("Se encontro al Player");
			Debug.DrawRay(rayTarget.position, new Vector3(x,0,z) * Vector3.Distance(_hitFront.point,transform.position), Color.green);
			//Debug.DrawRay(rayTarget.position, transform.forward * Vector3.Distance(_hitFront.point,transform.position), Color.green);		
			
		}else{				
				
			//Debug.Log("esta colisionando con Muros");
			//_agent.speed = 3.5f; 
			if(_isFollowPlayer){				
				//_agent.SetDestination(_positions[_counter].position);
				_isFollowPlayer = false;
			}
						
			Debug.DrawRay(rayTarget.position, new Vector3(x,0,z) * Vector3.Distance(_hitFront.point,transform.position), Color.magenta);			
			//Debug.DrawRay(rayTarget.position, transform.forward * Vector3.Distance(_hitFront.point,transform.position), Color.yellow);			
			//Debug.DrawRay(rayTarget.position, -transform.forward * Vector3.Distance(_hitBack.point,transform.position), Color.yellow);			
        }

		if (dist!=Mathf.Infinity && _agent.pathStatus==NavMeshPathStatus.PathComplete && _agent.remainingDistance==0) {			
			_counter++;
			if(_counter>=_positions.Length){
				_counter=0;
			}
			_agent.SetDestination(_positions[_counter].position);
		}
	}

	public void checkPlayer(){
		Debug.Log("checkPlayer");
		if(!_isFollowPlayer){			
			_agent.SetDestination(_positions[_counter].position);			
		}
		_isWaiting = false;
	}
}
