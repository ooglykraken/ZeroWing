using UnityEngine;
using System.Collections;

public class Sniper : Enemy {
	
	public int timeToFire = 90;
	
	private float stopDistance;
	
	public float laserSpeed = 20f;
	
	public bool isSniping;
	
	// public int health = 1;
	
	public void Awake(){
		laserWeapon = transform.Find("LaserWeapon");
		
		thisRigidbody = gameObject.GetComponent<Rigidbody>();
		
		screenBorder = Gameplay.Instance().transform.Find("ScreenBorder");
		
		this.gameObject.name = this.gameObject.name.Split("("[0])[0];
		
		stopDistance = Random.Range(15f,20f);
		
		canFire = false;
		isSniping = false;
	}
	
	public void Update(){
		
		if(transform.parent != GameObject.Find("Enemies").transform){
			Destroy(this.gameObject);
		}
		
		if(Vector3.Distance(transform.position, Player.Instance().transform.position) < stopDistance){
			isSniping = true;
		}
		
		if(isSniping){
			if(timer > 0f) {
				timer--;
			} else if(timer <= 0f){
				FireLaser();
			}
		}
	}
	
	public void FixedUpdate () {
			Move();		
	}
	
	private void Move(){
		if(isSniping){
			canFire = true;
			thisRigidbody.velocity = Vector3.zero;
		} else{
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
