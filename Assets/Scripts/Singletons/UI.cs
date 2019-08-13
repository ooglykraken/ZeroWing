using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UI : MonoBehaviour {
	
	public GameObject pauseMenu;
	public GameObject pauseButton;
	
	public bool musicOn;
	public bool sfxOn;
	
	public List<AudioSource> allSFX = new List<AudioSource>();
	
	public TextMesh scoreTextGame;
	
	public void Awake(){
		pauseButton = transform.Find("PauseBtn").gameObject;
		pauseMenu = transform.Find("PauseMenu").gameObject;
	}
	
	public void Start(){
		if(Menu.Instance() != null){
			PauseGame();
			
			if(!Menu.Instance().sfxOn){
				TurnSFXOff();
			}
			 
			if(!Menu.Instance().musicOn){
				TurnMusicOff();
				
			}
			
			Destroy(Menu.Instance().gameObject);
			
			Resume();
		}
		
		// SpawnWave();
		UpdateScore();
	}
	
	public void PauseGame(){
		Gameplay.Instance().enemies.gameObject.SetActive(false);
		Player.Instance().gameObject.SetActive(false);
		pauseButton.SetActive(false);
		pauseMenu.SetActive(true);
		UI.Instance().scoreTextGame.gameObject.SetActive(false);
		Gameplay.Instance().paused = true;
	}
	
	public void Resume(){
		Gameplay.Instance().enemies.gameObject.SetActive(true);
		Player.Instance().gameObject.SetActive(true);
		pauseButton.SetActive(true);
		pauseMenu.SetActive(false);
		UI.Instance().scoreTextGame.gameObject.SetActive(true);
		Gameplay.Instance().paused = false;
	}
	
	public void UpdateScore(){
		scoreTextGame.text = "Score: " + Gameplay.Instance().kills;
	}
	
	public void OpenMenu(){
		Application.LoadLevel("Menu");
	}
	
	public void ToggleMusic(){
		
		if(this.musicOn){
			TurnMusicOff();
		} else {
			TurnMusicOn();
		}
	}
	
	public void ToggleSfx(){
		if(this.sfxOn){
			TurnSFXOff();
		} else {
			TurnSFXOn();
		}
	}
	
	public void TurnMusicOn(){
		transform.Find("PauseMenu/MusicBtn/musicOn").gameObject.GetComponent<SpriteRenderer>().enabled = false;
		transform.Find("PauseMenu/MusicBtn/musicOff").gameObject.GetComponent<SpriteRenderer>().enabled = true;			
		Gameplay.Instance().gameObject.GetComponent<AudioSource>().Play();
		musicOn = true;
	}
	
	public void TurnMusicOff(){
		transform.Find("PauseMenu/MusicBtn/musicOn").gameObject.GetComponent<SpriteRenderer>().enabled = true;
		transform.Find("PauseMenu/MusicBtn/musicOff").gameObject.GetComponent<SpriteRenderer>().enabled = false;	
		Gameplay.Instance().gameObject.GetComponent<AudioSource>().Pause();
		musicOn = false;
	}
	
	public void TurnSFXOn(){
		transform.Find("PauseMenu/SfxBtn/sfxOn").gameObject.GetComponent<SpriteRenderer>().enabled = false;	
		transform.Find("PauseMenu/SfxBtn/sfxOff").gameObject.GetComponent<SpriteRenderer>().enabled = true;		
		foreach(AudioSource a in allSFX){
			a.enabled = true;
		}
		sfxOn = true;
	}
	
	public void TurnSFXOff(){
		transform.Find("PauseMenu/SfxBtn/sfxOn").gameObject.GetComponent<SpriteRenderer>().enabled = true;	
		transform.Find("PauseMenu/SfxBtn/sfxOff").gameObject.GetComponent<SpriteRenderer>().enabled = false;	
		foreach(AudioSource a in allSFX){
			a.enabled = false;
		}
		sfxOn = false;
	}
	
	private static UI instance = null;
	
	public static UI Instance(){
		if(instance == null){
			instance = GameObject.Find("UI").GetComponent<UI>();
		}
		
		return instance;
	}
}
