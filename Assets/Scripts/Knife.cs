using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Knife : MonoBehaviour
{
    public GameManager gameManager;
    public float force = 5f;
    public float torque = 5f;
    public Transform knifeTip;
    public LineRenderer throwingLine;
    public float lineLength = 2f;
    public GameObject trailEffect = null;

    public int price = 1;

    private Rigidbody rb;
    private float flyStartTime = 0f;

    private bool isFlying = false;
    private bool _blockInput = false;
    private Vector2 startSwipe;
    private Vector2 endSwipe;

    private AudioManager _audioManager;

    private ParticleSystem _particleSystem;
    private bool isSwiping;

    private float lastTriggerExitTime = 0f;
    private float triggerEnterInterval = 0.01f;

    private bool isOnWheel = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        _audioManager = FindObjectOfType<AudioManager>();
        _particleSystem = FindObjectOfType<ParticleSystem>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!_blockInput)
        {
            if (isSwiping)
            {
                throwingLine.gameObject.SetActive(true);
                Vector2 currentPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                Vector2 throwVector = currentPos - startSwipe;
                if (throwVector.magnitude > 1)
                    throwVector = throwVector.normalized;
                throwVector = throwVector * lineLength;
                Vector2 inversed = throwingLine.transform.InverseTransformVector(throwVector);
                Vector2 offset = new Vector3(0f, 0.5f);
                offset = throwingLine.transform.InverseTransformVector(offset);
                throwingLine.SetPosition(0, offset);
                throwingLine.SetPosition(1, inversed + offset);
                //Debug.Log("Throw vector: " + inversed);
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (!isFlying)
                {
                    if (EventSystem.current.IsPointerOverGameObject())
                        return;
                    startSwipe = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                    isSwiping = true;
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (isSwiping)
                {
                    endSwipe = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                    isSwiping = false;
                    throwingLine.gameObject.SetActive(false);
                    Swipe();
                }
            }
        }
    }

    public void SetBlockInput(bool val)
    {
        _blockInput = val;
    }

    public void SetKinematic(bool state)
    {
        rb.isKinematic = state;
        rb.velocity = new Vector3(0f, 0f, 0f);
    }

    public void SetParticleSystem(ParticleSystem particleSystem)
    {
        _particleSystem = particleSystem;
        _particleSystem.gameObject.transform.SetParent(knifeTip);
        _particleSystem.gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
    }

    void Swipe()
    {

        rb.isKinematic = false;

        if (isOnWheel)
        {
            transform.SetParent(null);
            isOnWheel = false;
        }

        flyStartTime = Time.time;

        Vector2 swipe = endSwipe - startSwipe;
        swipe *= lineLength;
        if (swipe.magnitude > 1)
            swipe = swipe.normalized;

        rb.AddForce(swipe * force, ForceMode.Impulse);

        if (swipe.x < 0)
            rb.AddTorque(0f, 0f, torque);
        else
            rb.AddTorque(0f, 0f, -torque);

        gameManager.ThrowCountIncrease();

        isFlying = true;
    }

    public void SetAudioManager(AudioManager audioManager)
    {
        _audioManager = audioManager;
    }

    public void SetTrailEnabled(bool val)
    {
        if (trailEffect != null)
        {
            if (val)
            {
                trailEffect.SetActive(true);
            }
            else
                trailEffect.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //-Check time passed from last TriggerExit
        if (lastTriggerExitTime < Time.time - triggerEnterInterval)
        {
            switch (other.tag)
            {
                case "Wood":
                    SetKinematic(true);
                    Stick();
                    break;
                case "Wheel":
                    SetKinematic(true);
                    isOnWheel = true;
                    transform.SetParent(other.transform, true);
                    Stick();
                    break;
                case "Moving":
                    SetKinematic(true);
                    isOnWheel = true;
                    transform.SetParent(other.transform);
                    Stick();
                    break;
                case "Falling":
                    SetKinematic(true);
                    other.GetComponent<FalingPlatform>().Activate(this);
                    Stick();
                    break;
                case "Coin":
                    other.gameObject.GetComponent<Coin>().SetCollected(true);
                    gameManager.AddCoin();
                    break;
                case "Finish":
                    SetKinematic(true);
                    gameManager.Win();
                    break;
                //case "Changer":
                  //  StartCoroutine(ChangePlane(other));
                    //break;
                case "Transparent":
                    break;
                default:
                    Restart();
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        lastTriggerExitTime = Time.time;
    }

    private void OnCollisionEnter(Collision collision)
    {
        float timeInAir = Time.time - flyStartTime;

        if (!rb.isKinematic && timeInAir >= .05f)
        {
            Restart();
        }
    }

    private void Stick()
    {
        _audioManager.Play("KnifeHit1");
        _particleSystem.Play();
        isFlying = false;
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
