﻿using UnityEngine;
using System.Collections;


[System.Serializable]
public class Sound{

	public string name;
	public AudioClip clip;


	[Range(0f,1f)]
	public float volume = 0.7f;
	[Range(0.5f, 1.5f)]
	public float pitch = 1f;
	[Range(0f,0.5f)]
	public float volumeVariance = 0.1f;
	[Range(0f,0.5f)]
	public float pitchVariance = 0.1f;

	public bool loop = false;

	private AudioSource source;

	public void SetSource (AudioSource _source){
		source = _source;
		source.clip = clip;
		source.loop = loop;
	}

	public void Play(){
		source.volume = volume * (1 + Random.Range(-volumeVariance/2f, volumeVariance/2f));
		source.pitch = pitch* (1 + Random.Range(-pitchVariance/2f,pitchVariance/2f));
		source.Play ();
	}

	public void Stop(){
		source.Stop ();
	}
}

public class AudioManager : MonoBehaviour {

	public static AudioManager instance;

	void Awake(){
		if(instance != null){
			if (instance != this) {
				Destroy (this.gameObject);
			}
		}else{
			instance = this;
			DontDestroyOnLoad (this);
		}

	}

	[SerializeField]
	Sound[] sounds;

	void Start(){
		for (int i = 0; i < sounds.Length; i++) {
			GameObject _go = new GameObject ("Sound_" + i + "_" + sounds [i].name);
			_go.transform.SetParent (this.transform);
			sounds [i].SetSource (_go.AddComponent<AudioSource> ());
			//Debug.Log(sounds[i].name + " source set");
		}
		PlaySound ("BackgroundMusic");
	}

	void Update(){
		
	}

	public void PlaySound (string _name){
		for(int i=0; i<sounds.Length;i++){
			if(sounds[i].name == _name){
				//Debug.Log ("Achou " + _name);
				sounds[i].Play();
				return;
			}
		}
		Debug.LogWarning("Sound not found:" + _name);
	}

	public void StopSound (string _name){
		for(int i=0; i<sounds.Length;i++){
			if(sounds[i].name == _name){
				Debug.Log ("Achou " + _name);
				sounds[i].Stop();
				return;
			}
		}
		Debug.LogWarning("Sound not found:" + _name);
	}
}
