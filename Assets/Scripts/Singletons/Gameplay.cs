using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gameplay : MonoBehaviour {
	
	// public float gameTimer;
	private int shifts;
	public int kills;
	
	private static int creeperValue = 5;
	private static int pursuerValue = 5;
	private static int sniperValue = 10;
	private static int dogfighterValue = 20;
	private static int mimicValue = 25;
	private static int bossValue = 100;
	
	private float distanceAdjustment = 30f;
	private float distancePerTick = 2f;
	private float playerModifier = 1f;
	
	private float enemyZLayer = 6f;
	
	private static float timeLimit = 240f; //Roughly 4 minutes. 
	
	public float gameTime;
	
	private Transform background;
	public Transform enemies;
	private Transform rightEdge;

	public Transform enemyRecycler;
	public Transform posToStopFiring;
	
	public GameObject pauseMenu;
	public GameObject deathMenu;
	public GameObject pauseButton;
	
	public TextMesh scoreTextDeath;
	
	private Vector3 spawnStart;
	
	private bool secondWaveSpawned;
	
	private bool canHearEnemy;
	public bool paused;
	
	public bool platformIsMobile;
	
	private List<Transform> backgroundImages = new List<Transform>(); 
	
	public void Awake(){
		// gameTimer = 0;
		background = transform.Find("Background");
		enemies = transform.Find("Enemies");
		paused = false;
		shifts = 1;
		kills = 0;
		backgroundImages.Add(background.Find("Image").transform); 
		backgroundImages.Add(background.Find("Image2").transform);
		backgroundImages.Add(background.Find("Image3").transform);
		backgroundImages.Add(background.Find("Image4").transform);
				
		spawnStart = transform.Find("ScreenBorder/SpawnPoint").position;
		canHearEnemy = false;

		secondWaveSpawned = false;
		
		if(Application.isMobilePlatform){
			platformIsMobile = true;
		} else {
			platformIsMobile = false;
		}
	}
	
	public void Update(){
		if(paused){
			return;
		}
		
		Carousel();
		// gameTimer++;
		
		if(enemies.childCount == 0){
			enemies.gameObject.GetComponent<AudioSource>().Stop();
			canHearEnemy = false;
		} else {
			// Debug.Log("Enemy engines");
			enemies.gameObject.GetComponent<AudioSource>().Play();
			canHearEnemy = true;
		}
	}
	
	private void Carousel(){
		
		float scrollAdjustment = Player.Instance().horizontalInput;
		
		background.position = Vector3.Lerp(background.position, new Vector3(background.position.x - distancePerTick, background.position.y, background.position.z), Time.deltaTime * 1f);
		
		if(background.position.x <= (shifts * -distanceAdjustment) - 12f + Random.Range(-2,2)){
			Shift(); 
			secondWaveSpawned = false;
		} else if(background.position.x <= (shifts * -distanceAdjustment) + Random.Range(-2,2)){
			if(!secondWaveSpawned){
				SpawnWave();
				secondWaveSpawned = true;
			}
		}
	}
	
	private void Shift(){
		backgroundImages[0].position = backgroundImages[3].position + new Vector3(distanceAdjustment, 0f, 10f);
		Transform temp = backgroundImages[0];
		backgroundImages[0] = backgroundImages[1];
		backgroundImages[1] = backgroundImages[2];
		backgroundImages[2] = backgroundImages[3];
		backgroundImages[3] = temp;
		backgroundImages[3].position = backgroundImages[3].position - new Vector3(0f, 0f, 10f);
		SpawnWave();
		
		shifts++;
	}
	
	private void SpawnWave(){
		// Create a function to spawn a wave of enemies of difficuly level proportional to gameTimer
		gameTime =  Time.timeSinceLevelLoad;
		float difficultyRatio = Mathf.RoundToInt((Time.timeSinceLevelLoad/timeLimit) * 100); 
		int difficultyModifier = 0;
		List<Enemy> listOfSpawnEnemies = new List<Enemy>();
		if(difficultyRatio < 50){
			difficultyModifier = Random.Range(Mathf.RoundToInt(difficultyRatio), 51);
		} else if(difficultyRatio < 100){
			difficultyModifier = Random.Range(Mathf.RoundToInt(difficultyRatio), 101);
		} else {
			difficultyModifier = Random.Range(90, 100 + Mathf.RoundToInt(difficultyRatio));
		}
		int numberOfEnemiesInWave = 0;
		while(difficultyModifier > 0){
			Enemy enemy = null;
			if(difficultyModifier >= bossValue){
						enemy = Instantiate(Resources.Load("Enemies/Boss", typeof (Enemy)) as Enemy) as Enemy;
						difficultyModifier -= bossValue;
						float xPos = 0f;
						float yPos = 10f;
						Transform parent = GameObject.Find("Enemies").transform;
						enemy.transform.parent = parent;
						enemy.transform.localPosition = new Vector3(xPos, yPos, 0f);
			} else if(difficultyModifier >= mimicValue){
				int coinToss = Random.Range(0,5);
				switch(coinToss){
					case 0:
						enemy = Instantiate(Resources.Load("Enemies/Creeper", typeof (Enemy)) as Enemy) as Enemy;
						difficultyModifier -= pursuerValue;
						break;
					case 1:
						enemy = Instantiate(Resources.Load("Enemies/Pursuer", typeof (Enemy)) as Enemy) as Enemy;
						difficultyModifier -= pursuerValue;
						break;
					case 2:
						enemy = Instantiate(Resources.Load("Enemies/Sniper", typeof (Enemy)) as Enemy) as Enemy;
						difficultyModifier -= sniperValue;
						break;
					case 3:
						enemy = Instantiate(Resources.Load("Enemies/Dogfighter", typeof (Enemy)) as Enemy) as Enemy;
						difficultyModifier -= dogfighterValue;
						break;
					default:
						enemy = Instantiate(Resources.Load("Enemies/Mimic", typeof (Enemy)) as Enemy) as Enemy;
						difficultyModifier -= mimicValue;
						break;
				}				
				listOfSpawnEnemies.Add(enemy);
			} else if(difficultyModifier >= dogfighterValue){
				int coinToss = Random.Range(0,4);
				switch(coinToss){
					case 0:
						enemy = Instantiate(Resources.Load("Enemies/Creeper", typeof (Enemy)) as Enemy) as Enemy;
						difficultyModifier -= pursuerValue;
						break;
					case 1:
						enemy = Instantiate(Resources.Load("Enemies/Pursuer", typeof (Enemy)) as Enemy) as Enemy;
						difficultyModifier -= pursuerValue;
						break;
					case 2:
						enemy = Instantiate(Resources.Load("Enemies/Sniper", typeof (Enemy)) as Enemy) as Enemy;
						difficultyModifier -= sniperValue;
						break;
					default:
						enemy = Instantiate(Resources.Load("Enemies/Dogfighter", typeof (Enemy)) as Enemy) as Enemy;
						difficultyModifier -= dogfighterValue;
						break;
				}
				listOfSpawnEnemies.Add(enemy);
			} else if(difficultyModifier >= pursuerValue){
				int coinToss = Random.Range(0,3);
				switch(coinToss){
					case 0:
						enemy = Instantiate(Resources.Load("Enemies/Creeper", typeof (Enemy)) as Enemy) as Enemy;
						difficultyModifier -= creeperValue;
						break;
					case 1:
						enemy = Instantiate(Resources.Load("Enemies/Pursuer", typeof (Enemy)) as Enemy) as Enemy; 
						difficultyModifier -= pursuerValue;
						break;
					default:
						enemy = Instantiate(Resources.Load("Enemies/Sniper", typeof (Enemy)) as Enemy) as Enemy; 
						difficultyModifier -= sniperValue;
						break;
				}
				listOfSpawnEnemies.Add(enemy);
			} else if(difficultyModifier >= pursuerValue){
				int coinToss = Random.Range(0,2);
				if(coinToss == 0){
					enemy = Instantiate(Resources.Load("Enemies/Creeper", typeof (Enemy)) as Enemy) as Enemy; 
					difficultyModifier -= creeperValue;
				} else {
					enemy = Instantiate(Resources.Load("Enemies/Pursuer", typeof (Enemy)) as Enemy) as Enemy; 
					difficultyModifier -= pursuerValue;
				}
				listOfSpawnEnemies.Add(enemy);
			} else if(difficultyModifier >= creeperValue){
				enemy = Instantiate(Resources.Load("Enemies/Creeper", typeof (Enemy)) as Enemy) as Enemy; 
				difficultyModifier -= creeperValue;
				listOfSpawnEnemies.Add(enemy);
			} else { // if(difficultyModifier < creeperValue){
				enemy = Instantiate(Resources.Load("Enemies/Creeper", typeof (Enemy)) as Enemy) as Enemy; 
				difficultyModifier = 0;
				listOfSpawnEnemies.Add(enemy);
			}
		}
		numberOfEnemiesInWave = (listOfSpawnEnemies.Count - 1);
		int numberOfEnemiesInRow = 4;
		for(int j = 0; j < 10f; j++){
			for(int i = 0; i < numberOfEnemiesInRow; i++){
				float xPos = (1 + j) * 7f;
				float yPos = (1 + i) * 4f;				
				if(numberOfEnemiesInWave < 0){
					return;
				}
				Enemy enemy = listOfSpawnEnemies[numberOfEnemiesInWave];
				Transform parent = GameObject.Find("Enemies").transform;
				enemy.transform.parent = parent;
				enemy.transform.localPosition = new Vector3(xPos, yPos, 0f);
				numberOfEnemiesInWave--;
			}
		}
		
	}
	
	public void KillEnemy(GameObject enemy){
		kills++;		
		UI.Instance().UpdateScore();
		Destroy(enemy);
	}
	
	public void RemoveEnemy(GameObject enemy){
		Destroy(enemy);
	}
	
	public void PlayerDeath(){
		paused = true;
		UI.Instance().scoreTextGame.gameObject.SetActive(false);
		enemies.gameObject.SetActive(false);
		UI.Instance().pauseButton.SetActive(false);
		deathMenu.SetActive(true);
		Player.Instance().gameObject.SetActive(false);
		scoreTextDeath.text = "Score: " + kills;
	}
	
	public void Restart(){
		Application.LoadLevel(Application.loadedLevel);
	}
	
	private static Gameplay instance = null;
	
	public static Gameplay Instance(){
		if(instance == null){
			instance = GameObject.FindObjectOfType<Gameplay>();
		}
		
		return instance;
	}
}
