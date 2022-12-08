using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] private GameObject customerDisappearParticle;
    public static Vector3 DisappearParticlePosition;

    [SerializeField] private GameObject cashEarnedParticle;
    public static Vector3 CashEarnedParticlePosition;

    public void SpawnCustomerDisappearParticle() //called by: OrderFinished
    {
        Instantiate(customerDisappearParticle, DisappearParticlePosition, transform.rotation); //spawn the the chosen particle at the chosen location

        DisappearParticlePosition = Vector3.zero; //reset the spawn position 
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
