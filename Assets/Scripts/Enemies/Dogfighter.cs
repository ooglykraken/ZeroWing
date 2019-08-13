using UnityEngine;
using System.Collections;

public class Dogfighter : Enemy {
	
	public int timeToFire = 40;
	
	public int timeToManeuver;
	
	private int maneuverTimer;
	
	public float laserSpeed = 30f;
	
	// public int health = 1;
	
	private Vector3 randomVelocity;
	
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
		
		if(timeToManeuver > 0f) {
			timeToManeuver--;
		} else if(timeToManeuver <= 0f){
			Maneuver();
		}
		
		CheckPos();
	}
	
	public void FixedUpdate () {		
		Move();
	}
	
	private void Maneuver(){
		randomVelocity = new Vector3(Random.Range(5f, 10f) * -1f, Random.Range(-8f, 8f), 0f);
		
		timeToManeuver = Random.Range(150, 200);
	}
	
	private void Move(){
		
		if(canFire){
			thisRigidbody.velocity = Vector3.Lerp(thisRigidbody.velocity, randomVelocity, Time.deltaTime * 4f);
		} else if(stoppedFiring){
			thisRigidbody.velocity = Vector3.Lerp(thisRigidbody.velocity, 2f * standardVelocity, Time.deltaTime * 4f);
		} else {
			thisRigidbody.velocity = Vector3.Lerp(thisRigidbody.velocity, standardVelocity, Time.deltaTime * 4f);
		}	
	}
	
	public void MoveUp(){
		randomVelocity = new Vector3(Random.Range(5f, 10f) * -1f, Random.Range(0f, 8f), 0f);
	}
	
	public void MoveDown(){
		randomVelocity = new Vector3(Random.Range(5f, 10f) * -1f, Random.Range(-8f, 0f), 0f);
	}
	
	public override void CheckPos(){		
		if(transform.position.x < enemyRecycler.position.x){
			Gameplay.Instance().RemoveEnemy(this.gameObject);
		}
		
		if(transform.position.x < screenBorder.Find("RightEdge").position.x){
			canFire = true;
		}
		
		if(transform.position.x < posToStopFiring.position.x){
			stoppedFiring = true;
			canFire = false;
		}
		
		
		if(transform.position.y > screenBorder.Find("TopEdge").position.y - 6f){
			MoveDown();
		} else if(transform.position.y < screenBorder.Find("BotEdge").position.y + 3f){
			MoveUp();
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
