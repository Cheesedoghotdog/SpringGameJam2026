using UnityEngine;
using System.Collections;

public class CoolDownTest : MonoBehaviour
{
    public float cooldown;
    public bool abilityReady;

    private void CastAbility()
    {  if (abilityReady == false)
            return;
           
        StartCoroutine(AbilityCoolDownRoutine(cooldown));
    }
    IEnumerator AbilityCoolDownRoutine(float cooldown)
    {
        abilityReady = false;

        yield return new WaitForSeconds(cooldown);

        abilityReady = true;
    }


}