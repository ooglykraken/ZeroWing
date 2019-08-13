using UnityEngine;
using System.Collections;

public class Boss  : Enemy {
	
	public int timeToFire = 30;
	
	private float stopDistance;
	
	public float laserSpeed = 35f;
	
	public bool isSniping;
	
	public bool movingDown;
	public bool movingUp;
	
	private static int bossHealth = 10;
	
	public void Awake(){
		laserWeapon = transform.Find("LaserWeapon");
		
		thisRigidbody = gameObject.GetComponent<Rigidbody>();
		
		this.gameObject.name = this.gameObject.name.Split("("[0])[0];
		
		stopDistance = Random.Range(15f,20f);
		
		this.enemyRecycler = Gameplay.Instance().enemyRecycler;
		this.posToStopFiring = Gameplay.Instance().posToStopFiring;
		
		screenBorder = Gameplay.Instance().transform.Find("ScreenBorder");

		movingDown = false;
		movingUp = false;
		
		canFire = false;
		isSniping = false;
		
		health = bossHealth;
	}
	
	public void Update(){
		
		if(transform.parent != GameObject.Find("Enemies").transform){
			Destroy(this.gameObject);
		}
		
		if(Vector3.Distance(transform.position, Player.Instance().transform.position) < stopDistance && !isSniping){
			isSniping = true;
			
			if(Random.Range(0, 1) == 1){
				MoveUp();
			} else {
				MoveDown();
			}
		}
		
		if(canFire){
			if(timer > 0f) {
				timer--;
			} else if(timer <= 0f){
				FireLaser();
			}
		}
	}
	
	public void FixedUpdate () {
		// Debug.Log(enemyType);
		
		Move();
		
	}
	
	private void Move(){
		
		CheckPos();
		
		if(isSniping){
			canFire = true;
			
			float verticalVelocity = 3f;
			
			if(movingDown){
				verticalVelocity = -3f;
			} else if(movingUp){
				verticalVelocity = 3f;
			} else {
				Debug.Log("Something is WRONG!");
			}
			
			thisRigidbody.velocity = Vector3.Lerp(thisRigidbody.velocity, new Vector3(0f, verticalVelocity, 0f), Time.deltaTime * 4f);
		} else{
			thisRigidbody.velocity = Vector3.Lerp(thisRigidbody.velocity, standardVelocity, Time.deltaTime * 4f);
		}
	
	}
	
	public void MoveUp(){
		movingUp = true;
		movingDown = false;
	}
	
	public void MoveDown(){
		movingDown = true;
		movingUp = false;
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
