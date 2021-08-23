using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Vector3 _startPosition;
    private Vector3 _nowtouchPosition;
    private bool _touchFlag = false;
    private GameObject _enemyObj;
    private bool _check = false;
    private bool _next = true;
    private int _strength;
    private int _ePoint;
    [SerializeField] private GameObject _strengthObj;
    [SerializeField] private TextMesh _strengthText;
    [SerializeField] private GameObject _camera;
    private int _limit;
    [SerializeField] private Text _limitText;
    [SerializeField] private GameObject _gameEnd;
    [SerializeField] private Text _gameScore;
    private int _highScore;
    private string key = "HIGH SCORE";
    
    void Start()
    {
        _startPosition = GetComponent<Transform>().position;
        _strength = 3;
        _limit = 10;
        _highScore = PlayerPrefs.GetInt(key, 0);
        _gameScore.text = _highScore.ToString();
    }


    void Update()
    {
        Vector3 nowtouchPosition;
        Vector3 differencePosition;
        _strengthText.text = _strength.ToString();
        _limitText.text = _limit.ToString();

        if(_touchFlag)
        {
            nowtouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            differencePosition = nowtouchPosition - _nowtouchPosition;
            GetComponent<Transform>().position += differencePosition;
            _nowtouchPosition = nowtouchPosition;
        }
        if(!_touchFlag)
        {
            if(_check && _ePoint < _strength)
            {
                _strength += _ePoint;
                _enemyObj.SetActive(false);
                _check = false;
                if(_next)
                {
                    _startPosition.y += 2.0f;
                    _camera.transform.position += new Vector3(0,2,0);
                    _limit--;
                } else {
                    _limit--;
                }
            } else {
                GetComponent<Transform>().position = _startPosition;
            }
        }
        if(_limit == 0)
        {
            _gameEnd.SetActive(true);
        }
        if(_strength > _highScore)
        {
            _highScore = _strength;
            PlayerPrefs.SetInt(key, _highScore);
            _gameScore.text = _highScore.ToString("000");
        }
    }

    void GetPlayer()
    {
        _touchFlag = true;
        _nowtouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    }
    void SetPlayer()
    {
        _touchFlag = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "enemy")
        {
            _check = true;
            _enemyObj = other.gameObject;
            Enemy e = _enemyObj.GetComponent<Enemy>();
            _ePoint = e.ePoint;
            _next = true;
        }
        if(other.gameObject.tag == "item")
        {
            _check = true;
            _enemyObj = other.gameObject;
            Enemy e = _enemyObj.GetComponent<Enemy>();
            _ePoint = e.ePoint;
            _next = false;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "enemy")
        {
            _check = false;
        }
        if(other.gameObject.tag == "item")
        {
            _check = false;
        }
    }
}