using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Employee : MonoBehaviour
{
    public Transform ServedCustomer;

    private bool customerBeingServed;
    public Transform CustomerWaitPosition;

    [Space(10f), SerializeField] private GameObject progressBar;
    [SerializeField] private List<Sprite> progressBarSprites;
    [SerializeField] private Image progressBarFill;
    [SerializeField] private Image progressBarBackground;

    private float prepTime;
    private Animator animator;
    public Transform CameraTarget;

    private EmployeeMerge employeeMerge;

    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        employeeMerge = GetComponent<EmployeeMerge>();
    }

    private void Update()
    {
        if (customerBeingServed && ServedCustomer != null)
        {
            progressBarFill.fillAmount = Mathf.MoveTowards(progressBarFill.fillAmount, 1f, Time.deltaTime * 1f / prepTime); //fill the progress to the end gradually to full while the order finishes
            
            if(!employeeMerge.IsDragged)
            {
                ServedCustomer.position = Vector3.Lerp(ServedCustomer.position, CustomerWaitPosition.position, Settings.CustomerSettings.CustomerMovementSpeed * Time.deltaTime);
            }
        }
    }

    public void PrepareOrder() //called by: OrderAccepted
    {
        if(!customerBeingServed && ServedCustomer != null)
        {
            StartCoroutine(OrderPrep()); //start preparing the order for the assigned customer
        }
    }

    public IEnumerator OrderPrep()
    {
        if(CustomerManager.WaitingCustomers.Count > 0)
        {
            animator.SetTrigger("OrderStarted");
            CustomerManager.WaitingCustomers.RemoveAt(0);
            prepTime = GameManager.OrderPrepTime;
            progressBarFill.sprite = progressBarSprites[GameManager.CurrentDishRecipe];
            progressBarBackground.sprite = progressBarSprites[GameManager.CurrentDishRecipe];
            progressBar.SetActive(true); //show the preparation progress bar above the employee
            customerBeingServed = true;

            yield return new WaitForSeconds(prepTime);

            if (ServedCustomer != null)
            {
                ParticleManager.DisappearParticlePosition = ServedCustomer.position; //let the particle manager know where to spawn a poof particle
                ParticleManager.CashEarnedParticlePosition = transform.position + new Vector3(0f, 3.2f, 0f); //let the particle manager know where to spawn a money particle
                Destroy(ServedCustomer.gameObject); //remove the completely served customer from the game
            }

            ServedCustomer = null;
            customerBeingServed = false;
            progressBarFill.fillAmount = 0f; //reset the employee's progress bar
            progressBar.SetActive(false); //hide the preparation progress bar above the employee
            animator.SetTrigger("OrderFinished");
            RuntimeEvents.NewCustomerAtCounter.Raise();
            RuntimeEvents.OrderFinished.Raise();
        }
    }
}
