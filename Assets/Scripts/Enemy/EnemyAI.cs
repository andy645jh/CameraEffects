using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
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
	private float _angle = 0;
	private Transform _pathForSound;
	private Transform _newTransformPath;
	private SoundDetector _soundDetector;
	private States _currentState = States.IDLE;
#endregion

	public enum States{
		PATROL, CHASE, SOUND, IDLE, DOOR
	}
	void Start () {
		//valores iniciales
		_positions = patrolEnemy.GetComponentsInChildren<Transform>();
		_agent = GetComponent<NavMeshAgent>();	
		_playerRigi = player.GetComponent<Rigidbody>();		

		//iniciar estado
		_newTransformPath = _positions[_counter];
		_agent.SetDestination(_newTransformPath.position);	
		proposeNextState(States.PATROL);		
	}
	private void moverPlayer(){
		float v = Input.GetAxisRaw("Vertical");
		float h = Input.GetAxisRaw("Horizontal");		
		//_playerRigi.velocity = new Vector3(10 * h, 0, 10 * v);		
	}

	public void startPatrol(){
		_agent.isStopped = false;
		_newTransformPath = _positions[_counter];
		_agent.SetDestination(_newTransformPath.position);	
	}	

	public void changePathBySound(SoundDetector soundDetector, Transform newObj){
		_soundDetector = soundDetector;
		_pathForSound = newObj;	
		proposeNextState(States.SOUND);		
	}
	
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
		
		//test de puertas
		if (detectPlayerFront && _hitFront.transform.CompareTag("doorAction")){
			if(Vector3.Distance(_hitFront.point,rayTarget.position)<2){
				//proposeNextState(States.DOOR);
				//_agent.Stop();
				_hitFront.transform.GetComponent<Animator>().Play("Door");
				//Invoke("finishOpenDoor",1);
				//startDoor();
			}			
		}

		/*if(detectPlayerFront && !_hitFront.transform.CompareTag("Player")){
			Debug.Log("Hit contra: "+ _hitFront.transform.name);
		}*/

		if ((detectPlayerBack && _hitBack.transform.CompareTag("Player")) || (detectPlayerFront && _hitFront.transform.CompareTag("Player")))
		{
			//if(!_isFollowPlayer){
				proposeNextState(States.CHASE);
				/*_agent.SetDestination(player.position);
				_isFollowPlayer = true;
				if(!_isWaiting){
					_isWaiting = true;
					Invoke("checkPlayer",followTime);
				}*/
				
			//}
			              
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
			Debug.DrawRay(rayTarget.position, -transform.forward * Vector3.Distance(_hitBack.point,transform.position), Color.yellow);			
        }

		if (dist!=Mathf.Infinity && _agent.pathStatus==NavMeshPathStatus.PathComplete && _agent.remainingDistance==0) {	
			if(_currentState==States.SOUND){
				proposeNextState(States.PATROL);
			}else{
				_counter++;
				if(_counter>=_positions.Length){
					_counter=0;
				}
				_newTransformPath = _positions[_counter];
			}							
		}

		//actualizar constantemente la posicion a seguir
		_agent.SetDestination(_newTransformPath.position);
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            SceneManager.LoadScene(0);
        }
    }
    public void startDoor(){
		_agent.isStopped = false;				
		checkPlayer();		
	}
	
	public bool checkNextState(EnemyAI.States next){
		Debug.Log("Current: "+_currentState.ToString());
		Debug.Log("Next: "+next.ToString());
		if(_currentState==States.PATROL){
			return next==States.CHASE || next==States.SOUND;			
		}
		else if(_currentState==States.CHASE){
			return next==States.PATROL || next==States.SOUND || next==States.DOOR;			
		}
		else if(_currentState==States.SOUND){
			return next==States.CHASE || next==States.PATROL;			
		}
		else if(_currentState==States.IDLE){
			return next==States.PATROL;			
		}else{
			return false;
		}	
	}

	public void proposeNextState(States next){
		if(checkNextState(next)){
			Debug.Log("<color=green>Cambiar Estado de "+_currentState.ToString()+" a: "+next.ToString()+"</color>");
			outOfState();
			_currentState = next;
			changeState();
		}else{
			//Debug.Log("<color=red>No pudo cambiar Estado de "+_currentState.ToString()+" a: "+next.ToString()+"</color>");
		}
	}
	public void starChase(){
		_agent.isStopped = false;
		_newTransformPath = player;
		_agent.SetDestination(_newTransformPath.position);
		//_isFollowPlayer = true;
		/**if(!_isWaiting){
			_isWaiting = true;
			Invoke("checkPlayer",followTime);
		}*/
		Invoke("checkPlayer",followTime);
	}

	public void checkPlayer(){
		Debug.Log("EnemyAI.checkPlayer");
		/*if(!_isFollowPlayer){			
			_agent.SetDestination(_positions[_counter].position);			
		}
		_isWaiting = false;*/
		proposeNextState(States.PATROL);
	}

	public void changeState(){
		
		switch(_currentState){

			//persiguiendo
			case States.CHASE:
			Debug.Log("Chase");
			starChase();
			break;

			//siguiendo el patron
			case States.PATROL:
			Debug.Log("Patrol");
			startPatrol();
			break;

			case States.SOUND:
			_newTransformPath = _pathForSound;
			_agent.SetDestination(_newTransformPath.position);
			break;

			case States.IDLE:
			_agent.isStopped = true;
			break;

			case States.DOOR:
			startDoor();
			break;
		}
	}

	public void outOfState(){
		
		switch(_currentState){

			//persiguiendo
			case States.CHASE:
			
			break;

			//siguiendo el patron
			case States.PATROL:
		
			break;

			case States.SOUND:
			_soundDetector.startDetecting();
			break;

			case States.IDLE:
			
			break;

			case States.DOOR:
			
			break;
		}
	}
}
