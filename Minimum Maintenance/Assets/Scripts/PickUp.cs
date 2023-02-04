using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class PickUp : MonoBehaviour
{
    private enum PickupState
    {
        Idle,
        AbovePlant,
        UpprootingPlant,
        HoldingPlant
    }

    [SerializeField] private PickupState currentHoldState;

    [Range(1f, 2f)]
    public int playerNum = 1;
    public string pickupInput = "Jump";
    public string dashInput = "Fire1";
    [SerializeField] private List<GameObject> plants = new();

    [Header("Throw Components")]
    public float throwChargeSpeed = 1;
    [SerializeField] private float currentThrowCharge = 0;
    public Vector2 minMaxDistance = new Vector2(2, 8);
    public Vector2 minMaxHeight = new Vector2(1, 3);
    public Vector2 minMaxDuration = new Vector2(0.3f, 0.8f);
    private float arcHeight;
    private float throwDuration;



    bool chargeThrow = false;
    public AnimationCurve throwCurve;
    public GameObject throwPosition;


    [Header("Holding Components")]
    public List<GameObject> rootedPlants = new();
    private GameObject holdingPlant;
    public GameObject holdPosition;
    bool isHolding = false;

    [Header("Charge Values")]
    public float chargeAmmount = 10;

    //private float chargeInputammount;
    private float chargeCurrent;
    private float chargeTimer;
    public Image chargeImage;

    private Movement moveScript;



    private void Start()
    {
        pickupInput = pickupInput + playerNum;
        dashInput = dashInput + playerNum;
        arcHeight = minMaxHeight.x;
        throwPosition.transform.localPosition = new Vector3(minMaxDistance.x, 0);
        throwPosition.SetActive(false);
        currentThrowCharge = 0;


        moveScript = GetComponent<Movement>();
        chargeImage.fillAmount = 0;
        chargeImage.gameObject.SetActive(false);

    }

    private void Update()
    {
        PickupManager();

        if (chargeTimer > 0)
        {
            float currentFill = chargeTimer / chargeAmmount;
            chargeImage.fillAmount = currentFill;
            chargeTimer -= Time.deltaTime;
        }
        else if (chargeTimer < 0)
        {
            chargeTimer = 0;
            chargeImage.fillAmount = 0;
        }

        if (Input.GetButtonDown(pickupInput))
        {
            OnPickupPress();
        }


    }

    void PickupManager()
    {
        switch (currentHoldState)
        {
            case PickupState.Idle://If I'm idle
                {
                    moveScript.isUpRooting = false;
                    isHolding = false;

                    RemoveChargebar();

                    if (plants.Count > 0)                   // and have plants below me
                    {
                        currentHoldState = PickupState.AbovePlant;
                    }
                }
                break;
            case PickupState.AbovePlant:// or if I'm above plant but there are no plants
                {
                    if (plants.Count < 1)
                    {
                        currentHoldState = PickupState.Idle;
                    }
                }
                break;
            case PickupState.UpprootingPlant:
                {
                    if (Input.GetButtonDown(dashInput)) //If I dash cancel the uprooting
                    {
                        currentHoldState = PickupState.Idle;
                    }
                }
                break;
            case PickupState.HoldingPlant:
                {
                    if (isHolding)
                    {
                        holdingPlant.transform.position = holdPosition.transform.position;


                        if (chargeThrow)
                        {
                            currentThrowCharge += Time.deltaTime * throwChargeSpeed;

                            throwPosition.transform.localPosition = new Vector3(Mathf.Lerp(minMaxDistance.x, minMaxDistance.y, currentThrowCharge), 0);
                            arcHeight = Mathf.Lerp(minMaxHeight.x, minMaxHeight.y, currentThrowCharge);
                            throwDuration = Mathf.Lerp(minMaxDuration.x, minMaxDuration.y, currentThrowCharge);
                        }

                        if (Input.GetButtonUp(pickupInput))
                        {
                            chargeThrow = false;
                            currentHoldState = PickupState.Idle;

                            holdingPlant.transform.DOJump(throwPosition.transform.position, arcHeight, 1, throwDuration).SetEase(throwCurve);

                            currentThrowCharge = 0;
                            throwPosition.SetActive(false);
                            throwPosition.transform.localPosition = new Vector3(minMaxDistance.x, 0);
                            arcHeight = minMaxHeight.x;
                            throwDuration = minMaxDuration.x;

                        }

                    }
                    chargeImage.gameObject.SetActive(false);
                    RemoveChargebar();
                }
                break;
            default:
                break;
        }
    }
    void OnPickupPress()
    {
        switch (currentHoldState)
        {
            case PickupState.AbovePlant:
                {
                    //Start Pickup anim

                    chargeImage.gameObject.SetActive(true);
                    moveScript.isUpRooting = true;
                    transform.DOMove(plants[0].transform.position + new Vector3(0, 0.5f), 0.3f);

                    currentHoldState = PickupState.UpprootingPlant;
                }
                break;
            case PickupState.UpprootingPlant:
                {
                    if (chargeTimer < chargeAmmount)
                    {
                        chargeTimer++;
                    }
                    else
                    {
                        RootingPlant();
                    }
                }
                break;
            case PickupState.HoldingPlant:
                {
                    chargeThrow = true;
                    throwPosition.SetActive(true);
                }
                break;
            default:
                break;
        }

    }

    void RemoveChargebar()
    {
        chargeImage.gameObject.SetActive(false);

        chargeTimer = 0;
    }

    private void RootingPlant()
    {
        if (plants[0].CompareTag("Plant"))
        {
            int randNum = Random.Range(0, 3);

            if (randNum == 0)
                DestroyPlant();
            else
                PickupPlant();

        }
        else if (plants[0].CompareTag("PlantGrown"))
        {
            int randNum = Random.Range(0, 5);

            if (randNum == 0)
                DestroyPlant();
            else
                PickupPlant();

        }
        else if (plants[0].CompareTag("PlantThrown"))
        {
            DestroyPlant();
        }

        chargeTimer = 0;
        chargeImage.fillAmount = 0;

    }

    void PickupPlant()
    {
        int randomPlant = Random.Range(0, rootedPlants.Count);

        holdingPlant = Instantiate(rootedPlants[randomPlant], plants[0].transform.position, rootedPlants[randomPlant].transform.rotation);
        Destroy(plants[0]);
        currentHoldState = PickupState.HoldingPlant;

        float pickupSpeed = 0.5f;

        holdingPlant.transform.DOMove(holdPosition.transform.position, 0.5f);
        Invoke(nameof(WaitForPickup), pickupSpeed);
    }

    void WaitForPickup()
    {
        moveScript.isUpRooting = false;
        isHolding = true;
    }

    void DestroyPlant()
    {
        //Add throwaway anim here
        Destroy(plants[0]);
        currentHoldState = PickupState.Idle;
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
<<<<<<< HEAD
        if (collision.tag == "PlantGrown")
=======
        if (collision.CompareTag("Plant") || collision.CompareTag("PlantGrown") || collision.CompareTag("PlantThrown"))
>>>>>>> feature/Hold_and_Throw
        {
            plants.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
<<<<<<< HEAD
        if (collision.tag == "PlantGrown")
=======
        if (collision.CompareTag("Plant") || collision.CompareTag("PlantGrown") || collision.CompareTag("PlantThrown"))
>>>>>>> feature/Hold_and_Throw
        {

            plants.Remove(collision.gameObject);

        }
    }





}
