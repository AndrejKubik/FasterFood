using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager instance;
    private void Awake()
    {
        instance = this;
    }

    public GameObject PoofParticle;
    public static Vector3 DisappearParticlePosition;

    [SerializeField] private GameObject cashEarnedParticle;
    public static Vector3 CashEarnedParticlePosition;

    public void SpawnCustomerDisappearParticle() //called by: OrderFinished + EmployeesMerged
    {
        SpawnParticle(PoofParticle, ref DisappearParticlePosition);
    }

    public void SpawnCashEarnedParticle() //called by: OrderFinished
    {
        SpawnParticle(cashEarnedParticle, ref CashEarnedParticlePosition);
    }

    private void SpawnParticle(GameObject particle, ref Vector3 position)
    {
        Instantiate(particle, position, transform.rotation); //spawn the the chosen particle at the chosen location

        position = Vector3.zero; //reset the spawn position 
    }
}
