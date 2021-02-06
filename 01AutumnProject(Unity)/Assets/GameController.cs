using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject[] notes;
    private float[] _timing;
    private int[] _lineNum;

    public string filePass;
    private int _notesCount = 0;

    private AudioSource _audioSource;
    private float _startTime = 0;

    public float timeOffset = -1;

    private bool _isPlaying = false;
    public GameObject startButton;

    public Text scoreText;
    private int _score = 0;

    [SerializeField]
    private GameObject good;
    

    void Start(){
        _audioSource = GameObject.Find ("GameMusic").GetComponent<AudioSource> ();
        _timing = new float[1024];
        _lineNum = new int[1024];
        good.SetActive(false);
        LoadCSV ();
    }

    void Update () {
        if (_isPlaying) {
            CheckNextNotes ();
            scoreText.text = _score.ToString ();
        }

    }

    public void StartGame(){
        startButton.SetActive (false);
        good.SetActive(false);
        _startTime = Time.time;
        _audioSource.Play ();
        _isPlaying = true;
    }

    void CheckNextNotes(){
        while (_timing [_notesCount] + timeOffset < GetMusicTime () && _timing [_notesCount] != 0) {
            SpawnNotes (_lineNum[_notesCount]);
            good.SetActive(false);
            _notesCount++;
        }
    }

    void SpawnNotes(int num){
         
        if (num == 0){
            Instantiate (notes[num],
            new Vector3 (-1.0f , 10.0f, 3.5f),
            Quaternion.identity);
            }
        else if(num==1){
            Instantiate (notes[num],
            new Vector3 (-4.0f , 10.0f, 0.5f),
            Quaternion.identity);
        }
        else if(num==2){
            Instantiate (notes[num],
            new Vector3 (-1.0f , 10.0f, -3.25f),
            Quaternion.identity);
        }
        else{
            Instantiate (notes[num],
            new Vector3 (2.0f , 10.0f, 0.5f),
            Quaternion.identity);
        }
    }

    void LoadCSV(){
        int i = 0, j;
        TextAsset csv = Resources.Load (filePass) as TextAsset;
        StringReader reader = new StringReader (csv.text);
        while (reader.Peek () > -1) {

            string line = reader.ReadLine ();
            string[] values = line.Split (',');
            for (j = 0; j < values.Length; j++) {
                _timing [i] = float.Parse( values [0] );
                _lineNum [i] = int.Parse( values [1] );
            }
            i++;
        }
    }

    float GetMusicTime(){
        return Time.time - _startTime;
    }

    public void GoodTimingFunc(int num){
        Debug.Log ("Line:" + num + " good!");
        Debug.Log (GetMusicTime());
        _score++;
        good.SetActive(true);
    }

}
