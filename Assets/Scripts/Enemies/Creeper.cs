using UnityEngine;
using System.Collections;

public class Creeper : Enemy {

	public int timeToFire = 80;
	
	// public int health = 1;
	
	public float laserSpeed = 9f;
	
	// public void Awake(){
		// laserWeapon = transform.Find("LaserWeapon");
		
		// thisRigidbody = gameObject.GetComponent<Rigidbody>();
		
		// this.gameObject.name = this.gameObject.name.Split("("[0])[0];
		
		// canFire = false;
	// }
	
	public void Update(){
		
		if(transform.parent != GameObject.Find("Enemies").transform){
			Destroy(this.gameObject);
		}
		
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
		// if(canFire){
			// transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(-4f, 0f, 0f), Time.deltaTime);
		// } else {
			// thisRigidbody.velocity = Vector3.Lerp(thisRigidbody.velocity, standardVelocity, Time.deltaTime * 4f);
		// }
		if(stoppedFiring){
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
