 using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	
	public bool canFire;
	public bool stoppedFiring;
	public bool passedPlayer;
	
	public int timer;
	
	public int health = 1;
	
	public static Vector3 standardVelocity = new Vector3(-5f, 0f, 0f);
	
	// public string enemyType;
	
	public Transform enemyRecycler;
	public Transform posToStopFiring;
	
	public Transform screenBorder;
	
	public Transform laserWeapon;
	
	public Rigidbody thisRigidbody;
	
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
	}
	
	public void Update(){
		CheckPos();
		// if(transform.parent != GameObject.Find("Enemies").transform){
			// Destroy(this.gameObject);
		// }
		
		// if(canFire){
			// if(timer > 0f) {
				// timer--;
			// } else if(timer <= 0f){
				// FireLaser();
			// }
		// }
		
		// if(passedPlayer){
			// SpeedAhead();
			// canFire = false;
		// }
	}


	// public void FixedUpdate () {
		// Debug.Log(enemyType);
		
		// Move();
		
	// }
	
	public void OnTriggerEnter(Collider c){
		
		if(c.transform.parent == null){
			return;
		} else if(c.transform.parent.tag == "Player"){

			if(c.tag == "Laser"){
				// Debug.Log(c.tag);
				Destroy(c.gameObject);
				
				health--;
				
				if(health <= 0){
					Gameplay.Instance().KillEnemy(this.gameObject);
				} else {
					// He Lives!
				}
				
				
			}else {
				// The collider belongs to player
				Gameplay gp = Gameplay.Instance();
				gp.RemoveEnemy(this.gameObject);
				Player.Instance().PlayerDamage();
			}
		}
	}
	
	// private void Move(){
		// switch(enemyType){
			// case "Creeper":
				// transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(-4f, 0f, 0f), Time.deltaTime);
				// break;
			// case "Pursuer":
				// transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(-10f, 0f, 0f), Time.deltaTime);
				// break;
				
			// case "Dogfighter":
				// transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(-5f, 0f, 0f), Time.deltaTime);
				// break;
				
			// case "Mimic":
				// transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(-5f, 0f, 0f), Time.deltaTime);
				// break;
				
			// case "Sniper":
				// transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(-5f, 0f, 0f), Time.deltaTime);
				// break;
				
			// case "Boss":
				// transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(-5f, 0f, 0f), Time.deltaTime);
				// break;
				
			// default:
			
				// break;
		// }
		
	// }
	
	
	
	public virtual void CheckPos(){		
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
		
	}
	
	public void CheckSFX(){
		
	}
	
	// public void FireLaser(){
		// EnemyLaser laser = Instantiate(Resources.Load("Projectiles/EnemyLaser", typeof (EnemyLaser)) as EnemyLaser) as EnemyLaser; 

		// laser.transform.position = laserWeapon.position;
		
		// laser.transform.LookAt(Player.Instance().transform.position);
		// laser.transform.Find("Sprite").eulerAngles = Vector3.zero;
		// laser.GetComponent<Rigidbody>().velocity = laser.transform.forward * laserSpeed;
		// laser.transform.parent = transform;
		// timer = timeToFire;
	// }
	
}
