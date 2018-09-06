using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class SoundDetector : MonoBehaviour {

	public Transform enemy;
	public Transform player;
	public int maxAmountWalls = 3;
	// Movement speed in units/sec.
    public float speed = 1.0F;

    // Time when the movement started.
    private float _startTime;

    // Total distance between the markers.
    private float _journeyLength;
	private RigidbodyFirstPersonController _playerControl;
	private int _amountWalls = 0;
	private Transform _pathObj;
	private bool _isDetecting = false;

	void Start () {
		
        // Calculate the journey length.
        _journeyLength = Vector3.Distance(transform.position, player.position);	
		_playerControl = player.GetComponent<RigidbodyFirstPersonController>();
		_pathObj = transform.GetChild(0);
		startDetecting();
	}
	
	// Update is called once per frame
	void Update () {

		if(!_isDetecting) return;

		// Distance moved = time * speed.
        float distCovered = (Time.time - _startTime) * speed;

        // Fraction of journey completed = current distance divided by total distance.
        float fracJourney = distCovered / _journeyLength;

        // Set our position as a fraction of the distance between the markers.
        transform.position = Vector3.Lerp(transform.position, player.position, fracJourney);
		var dist = Vector3.Distance(transform.position, player.position);
		//Debug.Log("Distance: "+dist);

		if(dist<0.1f){
			//Debug.Log("Start Again");
			transform.position = enemy.position;			
        	_journeyLength = Vector3.Distance(transform.position, player.position);
			_startTime = Time.time;	

			if(_playerControl.isRun() && _amountWalls<maxAmountWalls){
				Debug.Log("Escucho Algo: "+_pathObj.name);
				var enemyAI = enemy.GetComponent<EnemyAI>();
				_pathObj.position = player.position;
				enemyAI.changePathBySound(this,_pathObj.transform);
				_isDetecting = false;
			}

			_amountWalls = 0;
		}
	}

	/// <summary>
	/// OnTriggerEnter is called when the Collider other enters the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerEnter(Collider other)
	{
		_amountWalls++;
		//Debug.Log("OnTriggerEnter: "+other.name);		
	}

	public void startDetecting(){
		Debug.Log("startDetecting");
        _startTime = Time.time;		
		transform.position = enemy.position;	
		_journeyLength = Vector3.Distance(transform.position, player.position);
		_amountWalls=0;				
		_isDetecting = true;		
	}
}
