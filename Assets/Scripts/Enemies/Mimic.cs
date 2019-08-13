using UnityEngine;
using System.Collections;

public class Mimic : Enemy {

	public int timeToFire = 40;
	
	public float verticalSpeed;
	public float horizontalSpeed;
	
	public float laserSpeed = 35f;
	
	public float horizontalModifier;
	public float verticalModifier;
	
	// public int health = 1;
	
	public void Awake(){
		laserWeapon = transform.Find("LaserWeapon");
		
		thisRigidbody = gameObject.GetComponent<Rigidbody>();
		
		this.gameObject.name = this.gameObject.name.Split("("[0])[0];
		
		this.enemyRecycler = Gameplay.Instance().enemyRecycler;
		this.posToStopFiring = Gameplay.Instance().posToStopFiring;
		
		screenBorder = Gameplay.Instance().transform.Find("ScreenBorder");
		
		canFire = false;
		stoppedFiring = false;
		passedPlayer = false;
		
		
		this.verticalSpeed = Player.Instance().verticalSpeed * .4f;
		this.horizontalSpeed = Player.Instance().horizontalSpeed * .4f;
		
	}
	
	public void Update(){
		
		if(transform.parent != GameObject.Find("Enemies").transform){
			Destroy(this.gameObject);
		}
		
		horizontalModifier = -1f;
		verticalModifier = Input.GetAxis("Vertical") * 1f;
		
		if(canFire){
			
			if(timer > 0f) {
				timer--;
			} else if(timer <= 0f){
				FireLaser();
			}
		}
		
		CheckPos();
	}
	
	public void FixedUpdate () {
		// Debug.Log(enemyType);
		
		Move();
		
	}
	
	private void Move(){
		if(canFire){
			 
			
			Vector3 newVelocity = new Vector3(horizontalModifier * horizontalSpeed, verticalModifier * verticalSpeed, 0f);
		
			thisRigidbody.velocity = Vector3.Lerp(thisRigidbody.velocity, newVelocity, Time.deltaTime * 4f);
		} else if(stoppedFiring){
			thisRigidbody.velocity = Vector3.Lerp(thisRigidbody.velocity, 2f * standardVelocity, Time.deltaTime * 4f);
		} else {
			thisRigidbody.velocity = Vector3.Lerp(thisRigidbody.velocity, standardVelocity, Time.deltaTime * 4f);
		}
		
	}
	
	public void FireLaser(){
		EnemyLaser laser = Instantiate(Resources.Load("Projectiles/EnemyLaser", typeof (EnemyLaser)) as EnemyLaser) as EnemyLaser; 

		laser.transform.position = laserWeapon.position;
		
		laser.transform.LookAt(Player.Instance().transform.position);
		laser.transform.Find("Sprite").eulerAngles = Vector3.zero;
		laser.GetComponent<Rigidbody>().velocity = laser.transform.forward * laserSpeed;
		// laser.transform.parent = transform;
		timer = timeToFire;
	}
}
