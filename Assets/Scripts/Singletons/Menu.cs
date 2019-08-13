using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {
	
	public bool musicOn;
	public bool sfxOn;
	
	public GameObject optionsMenu;
	public GameObject startBtn;
	public GameObject optionsBtn;
	
	public bool platformIsMobile;
	
	public void Awake(){
		DontDestroyOnLoad(this.gameObject);
		
		musicOn = true;
		sfxOn = true;
		
		if(Application.isMobilePlatform){
			platformIsMobile = true;
		} else {
			platformIsMobile = false;
		}
	}
	
	public void StartGame(){
		Application.LoadLevel("Gameplay");
	}
	
	
	public void ToggleOptions(){
		bool optionsActive = optionsMenu.active;
		
		optionsMenu.SetActive(!optionsActive);
		startBtn.SetActive(optionsActive);
		optionsBtn.SetActive(optionsActive);
	}
	
	public void ToggleMusic(){
		
		if(musicOn){
			transform.Find("MenuPause/MusicBtn/musicOn").gameObject.GetComponent<SpriteRenderer>().enabled = true;
			transform.Find("MenuPause/MusicBtn/musicOff").gameObject.GetComponent<SpriteRenderer>().enabled = false;	
			musicOn = false;
		} else {
			transform.Find("MenuPause/MusicBtn/musicOn").gameObject.GetComponent<SpriteRenderer>().enabled = false;
			transform.Find("MenuPause/MusicBtn/musicOff").gameObject.GetComponent<SpriteRenderer>().enabled = true;			
			musicOn = true;
		}
	}
	
	public void ToggleSfx(){
		if(sfxOn){
			transform.Find("MenuPause/SfxBtn/sfxOn").gameObject.GetComponent<SpriteRenderer>().enabled = true;	
			transform.Find("MenuPause/SfxBtn/sfxOff").gameObject.GetComponent<SpriteRenderer>().enabled = false;	
			sfxOn = false;
		} else {
			transform.Find("MenuPause/SfxBtn/sfxOn").gameObject.GetComponent<SpriteRenderer>().enabled = false;	
			transform.Find("MenuPause/SfxBtn/sfxOff").gameObject.GetComponent<SpriteRenderer>().enabled = true;		
			sfxOn = true;
		}
	}
	
	private static Menu instance = null;
	
	public static Menu Instance(){
		if(instance == null){
			instance = GameObject.FindObjectOfType<Menu>();
		}
		
		return instance;
	}
}
