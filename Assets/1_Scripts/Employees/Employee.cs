using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Employee : MonoBehaviour
{
    [HideInInspector] public Transform ServedCustomer;

    private bool customerBeingServed;
    public Transform CustomerWaitPosition;

    [Space(10f), SerializeField] private GameObject progressBar;
    [SerializeField] private Image progressBarFill;

    private float prepTime;

    private Animator animator;

    [Header("MODEL PARENTS: ")]
    [SerializeField] private Transform hats;
    [SerializeField] private Transform heads;
    [SerializeField] private Transform bodies;

    private void OnEnable()
    {
        CharacterAppearance.ShowRandomModel(hats);
        CharacterAppearance.ShowRandomModel(heads);
        CharacterAppearance.ShowRandomModel(bodies);

        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (customerBeingServed && ServedCustomer != null)
        {
            ServedCustomer.position = Vector3.Lerp(ServedCustomer.position, CustomerWaitPosition.position, Settings.CustomerSettings.CustomerMovementSpeed * Time.deltaTime);

            progressBarFill.fillAmount = Mathf.MoveTowards(progressBarFill.fillAmount, 1f, Time.deltaTime * 1f / prepTime); //fill the progress to the end gradually to full while the order finishes
        }
    }

    public void PrepareOrder() //called by: OrderAccepted
    {
        if(!customerBeingServed && ServedCustomer != null) StartCoroutine(OrderPrep(GameManager.OrderPrepTime)); //start preparing the order for the assigned customer
    }

    IEnumerator OrderPrep(float delay)
    {
        if(CustomerManager.WaitingCustomers.Count > 0)
        {
            animator.SetTrigger("OrderStarted"); //start the order prep animation

            CustomerManager.WaitingCustomers.RemoveAt(0);

            prepTime = delay;

            progressBar.SetActive(true); //show the preparation progress bar above the employee
            customerBeingServed = true;

            yield return new WaitForSeconds(delay);

            if (ServedCustomer != null) Destroy(ServedCustomer.gameObject); //remove the completely served customer from the game
            ServedCustomer = null;

            customerBeingServed = false;
            progressBarFill.fillAmount = 0f; //reset the employee's progress bar
            progressBar.SetActive(false); //hide the preparation progress bar above the employee

            RuntimeEvents.NewCustomerAtCounter.Raise();
            RuntimeEvents.OrderFinished.Raise();

            animator.SetTrigger("OrderFinished"); //stop the order prep animation and play the money gain animation
        }
    }
}
