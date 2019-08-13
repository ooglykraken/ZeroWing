using UnityEngine;
using System.Collections;

public class Pursuer : Enemy {

	public int timeToFire = 80;
	
	// public int health = 1;
	
	public float laserSpeed = 40f;
	
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
		
		CheckPos();
	}
	
	public void FixedUpdate () {		
		Move();	
	}
	
	private void Move(){
		float verticalModifier = Input.GetAxis("Vertical") * -1f;
		
		if(canFire){
			
			// Vector3 originalSpriteAngles = transform.Find("Sprite").eulerAngles;
			
			// transform.LookAt(Player.Instance().transform.position);
			
			// transform.Find("Sprite").eulerAngles = Vector3.zero;
			// transform.Find("Sprite").eulerAngles = new Vector3(0f, -270f, 0f);
			// transform.eulerAngles = Vector3.zero;
			
			// thisRigidbody.velocity = transform.forward * 7f;
			
			transform.position = Vector3.MoveTowards(transform.position, Player.Instance().transform.position, Time.deltaTime * 6f);
			
		} else if(stoppedFiring){
			thisRigidbody.velocity = Vector3.Lerp(thisRigidbody.velocity, 3f * standardVelocity, Time.deltaTime);
		} else {
			thisRigidbody.velocity = Vector3.Lerp(thisRigidbody.velocity, 2f * standardVelocity, Time.deltaTime * 4f);
		}
	}
	
	public override void CheckPos(){		
		if(transform.position.x < enemyRecycler.position.x){
			Gameplay.Instance().RemoveEnemy(this.gameObject);
		}
		
		if(transform.position.x < screenBorder.Find("RightEdge").position.x){
			canFire = true;
		}
		
		if(transform.position.x < posToStopFiring.position.x - 3f){
			stoppedFiring = true;
			canFire = false;
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
