using System.Collections;
using UnityEngine;

public class NotesScript : MonoBehaviour {

    public int lineNum;
    private GameController _gameController;
    private bool isInLine = false;
    private KeyCode _lineKey;

    //　出現させるエフェクト
	[SerializeField]
    private GameObject effectObject;
	//　エフェクトを消す秒数
	[SerializeField]
    private float deleteTime;
	//　エフェクトの出現位置のオフセット値
	[SerializeField]
    private float offset;

    void Start () {
        _gameController = GameObject.Find ("GameController").GetComponent<GameController> ();
        _lineKey = GameUtil.GetKeyCodeByLineNum(lineNum);
    }

    void Update () {
        this.transform.position += Vector3.down * 10 * Time.deltaTime;

        if (this.transform.position.y < -5.0f) {
            Debug.Log ("false");
            Destroy (this.gameObject);
        }

        if(isInLine){
            CheckInput(_lineKey);
        }
    }

    void OnTriggerEnter (Collider other) {
        isInLine = true;
    }

    void OnTriggerExit (Collider other) {
        isInLine = false;
    }
 
	// Use this for initialization
	void MakeEffect () {
		//　ゲームオブジェクト登場時にエフェクトをインスタンス化
		var instantiateEffect = GameObject.Instantiate (effectObject, transform.position + new Vector3(0f, offset, 0f), Quaternion.identity) as GameObject;
		Destroy (instantiateEffect, deleteTime);
	}

    void CheckInput (KeyCode key) {

        if (Input.GetKeyDown (key)) {
            _gameController.GoodTimingFunc (lineNum);
            MakeEffect();
            Destroy (this.gameObject);
        }
    }
}