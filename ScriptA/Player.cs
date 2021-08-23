using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //�L�����N�^�[�̈ړ�
    [SerializeField] FixedJoystick _FixedJoystick;
    private float _MoveSpeed = 4.0f;
    private CharacterController _Controller;
    private Vector3 _Direction;
    private Animator _Animator;
    //�A�N�V����
    public bool action;
    public GameObject actionCube;
    //�v���C���[��Y���W���m�F���ĕ����Ȃ��悤�ɂ���
    private Vector3 _Gravity;
    private Transform _Transform;
    //�l���|�C���g
    public int point;
    public Text pointText;
    //�����^�C�}�[
    public float spUpTimer = 30;
    public Text spUpTimerText;
    public GameObject spUp;
    //�X�R�A�X�V
    private int _highScore;
    public Text highScoreText;
    private string key = "HIGH SCORE";
    //�Q�[���I�[�o�[
    public GameObject gameOver;
    //�ʔ�΂�
    public bool ball;
    public int ballStack;
    public GameObject greenball;
    public GameObject ballPosition;
    public Text ballCount;
    //�y��
    public ParticleSystem footSmoke;
    //�A�C�e���擾
    public ParticleSystem getEffect;
    //�S�[���c��Ǘ�
    public Text goalCount;
    //�T�E���h
    AudioSource audioSource;
    [SerializeField] AudioClip itemGetA , itemGetB , itemGetC , itemGetS , itemGetG;
    [SerializeField] AudioClip actionSound , missSound , ballSound;
    private bool misscheck = true;

    void Start()
    {
        _Controller = GetComponent<CharacterController>();
        _Animator = GetComponent<Animator>();
        BallCheck();
        _highScore = PlayerPrefs.GetInt(key, 0);
        highScoreText.text = _highScore.ToString("�N���A�{�[�i�X\n"+ "+1000\n" +"�n�C�X�R�A\n" + "0000");
        //PlayerPrefs.DeleteAll();
        //�X�R�A�����p
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        _Controller.Move(_Direction * _MoveSpeed * Time.deltaTime);
        _Direction = Vector3.forward * _FixedJoystick.Vertical + Vector3.right * _FixedJoystick.Horizontal;

        _Transform = this.transform;
        _Gravity = _Transform.position;

        int count = GameObject.FindGameObjectsWithTag("Goal").Length;
        goalCount.text = count.ToString("�~"+"0");

        if(_Gravity.y > 0.1)
        {
            _Gravity.y -= 0.1f;
            _Transform.position = _Gravity;
        }

        if(_Direction != new Vector3(0, 0, 0))
        {
            transform.localRotation = Quaternion.LookRotation(_Direction);

            if(_MoveSpeed < 8.0f)
            {
                PlayerWalk();

                if(footSmoke.isEmitting)
                {
                    footSmoke.Stop();
                }
            } 
            if(_MoveSpeed > 8.0f)
            {
                PlayerRun();

                if(!footSmoke.isEmitting)
                {
                    footSmoke.Play();
                }
            }
        } else {
            PlayerStop();

            if(footSmoke.isEmitting)
            {
                footSmoke.Stop();
            }
        }

        if(_MoveSpeed > 8.0f)
        {
            spUpTimer -= Time.deltaTime;
            SetSpUpTimerText();
        }

        if(action)
        {
            _Animator.SetTrigger("action");
            action = false;
            actionCube.SetActive(true);
            Invoke("ActionTimer",0.3f);
            audioSource.PlayOneShot(actionSound , 0.2f);
        }

        if(point > _highScore)
        {
            _highScore = point;
            PlayerPrefs.SetInt(key, _highScore);
            highScoreText.text = _highScore.ToString("�N���A�{�[�i�X\n"+ "+1000\n" +"�n�C�X�R�A�X�V!!\n" + "0000");
        }
        
        if(ball)
        {
            if(ballStack == 0)
            {
                ball = false;
            }
            if(ballStack > 0)
            {
                _Animator.SetTrigger("action");
                ballStack --;
                ball = false;
                BallAdd();
                BallCheck();
                audioSource.PlayOneShot(ballSound);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "SpeedUp")
        {
            Destroy(other.gameObject);
            _Animator.SetBool("walk",false);
            PlayerRun();
            _MoveSpeed  =  8.5f;
            spUp.SetActive(true);
            Invoke("SpeedDown",30.0f);
            getEffect.Play();
            audioSource.PlayOneShot(itemGetS);
        }
        if(other.gameObject.tag == "Point100")
        {
            Destroy(other.gameObject);
            point += 100;
            SetPointText();
            getEffect.Play();
            audioSource.PlayOneShot(itemGetA);
        }
        if(other.gameObject.tag == "Point300")
        {
            Destroy(other.gameObject);
            point += 300;
            SetPointText();
            getEffect.Play();
            audioSource.PlayOneShot(itemGetB);
        }
        if(other.gameObject.tag == "Point500")
        {
            Destroy(other.gameObject);
            point += 500;
            SetPointText();
            getEffect.Play();
            audioSource.PlayOneShot(itemGetC);
        }
        if(other.gameObject.tag == "Enemy")
        {
            _Animator.SetBool("miss",true);
            _MoveSpeed = 0;
            Invoke("GameOver",2.0f);
            if(misscheck)
            {
                audioSource.PlayOneShot(missSound);
                misscheck = false;
            }
        }
        if(other.gameObject.tag == "Ball")
        {
            Destroy(other.gameObject);
            ballStack ++;
            BallCheck();
            audioSource.PlayOneShot(itemGetG);
        }
    }

    void PlayerWalk()
    {
        _Animator.SetBool("run",false);
        _Animator.SetBool("walk",true);
    }
    void PlayerRun()
    {
        _Animator.SetBool("run",true);
    }
    void PlayerStop()
    {
        _Animator.SetBool("walk",false);
        _Animator.SetBool("run",false);
    }
    void ActionTimer()
    {
        actionCube.SetActive(false);
    }
    void SpeedDown()
    {
        _MoveSpeed = 4.0f;
        spUp.SetActive(false);

    }
    void SetPointText()
    {
        pointText.text = "Score\n" + point.ToString("0000");
    }
    void SetSpUpTimerText()
    {
        spUpTimerText.text = "SpeedUp\n" + spUpTimer.ToString("00.00");
    }
    void GameOver()
    {
        gameOver.gameObject.SetActive(true);
    }
    void BallAdd()
    {
        Vector3 launcher = GameObject.Find ("ballPosition").transform.position;
        Instantiate (greenball, launcher, Quaternion.Euler(0f, gameObject.transform.localEulerAngles.y,0f));
    }
    void BallCheck()
    {
        ballCount.text = ballStack.ToString("�~" + "0");
    }
}