using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    private Collider2D playerCollider;
    public string pickupInput = "Jump";
    public string dashInput = "Fire1";

    [SerializeField] private List<GameObject> plants = new List<GameObject>();


    [Header("Charge Values")]
    public float chargeAmmount = 10;

    //private float chargeInputammount;
    private float chargeCurrent;
    private float chargeTimer;
    public Image chargeImage;


    private void Start()
    {
        chargeImage.fillAmount = 0;
        chargeImage.gameObject.SetActive(false);

    }

    private void Update()
    {
        
        if (currentHoldState == PickupState.Idle) //If I'm idle
        {
            chargeImage.gameObject.SetActive(false);

            if (plants.Count > 0)                   // and have plants below me
            {
                currentHoldState = PickupState.AbovePlant;
            }
        }
        else if(currentHoldState == PickupState.AbovePlant && plants.Count < 1) // or if I'm above plant but there are no plants
        {
            currentHoldState = PickupState.Idle;
        }

        if (Input.GetButtonDown(dashInput) && currentHoldState == PickupState.UpprootingPlant) //If I dash cancel the rooting
        {
            currentHoldState = PickupState.Idle;
        }



        if (chargeTimer > 0)
        {
            float currentFill = chargeTimer / chargeAmmount;
            chargeImage.fillAmount = currentFill;
            chargeTimer -= Time.deltaTime;
        }
        else if(chargeTimer < 0)
        {
            chargeTimer = 0;
            chargeImage.fillAmount = 0;
        }


        if (Input.GetButtonDown(pickupInput))
        {
            if (currentHoldState == PickupState.AbovePlant)
            {
                //Disable move / Enable Uprooting on move script

                //Start Pickup anim

                // Go to rooted state
                // Start Spam sequence

                chargeImage.gameObject.SetActive(true);

                currentHoldState = PickupState.UpprootingPlant;


            }
            else if (currentHoldState == PickupState.UpprootingPlant)
            {


                if (chargeTimer < chargeAmmount)
                {
                    chargeTimer++;
                }
                else
                {
                    Destroy(plants[0]);

                    chargeTimer = 0;
                    chargeImage.fillAmount = 0;
                    currentHoldState = PickupState.Idle;
                }



                //Start Spamm sequance
                //When Sequance done roll dice if it's picked up or discarded               

                //if picked up go to holding state

            }
            else if (currentHoldState == PickupState.HoldingPlant)
            {
                //Hold to charge throw

                //Release to throw

                //Go back to Idle state

            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Plant")
        {
            plants.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Plant")
        {

            plants.Remove(collision.gameObject);

        }
    }





}
