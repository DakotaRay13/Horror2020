using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_DisableFlashlight : BaseEvents
{
    Light flashlight;

    public override void DoEvent()
    {
        StartCoroutine(DisableFlashlight());
        GetComponent<Collider>().enabled = false;
    }

    public IEnumerator DisableFlashlight()
    {
        flashlight = GameObject.Find("Player").GetComponentInChildren<Light>();

        float maxIntensity = flashlight.intensity;
        float dimVal = maxIntensity / 33.8f;

        int i = 0;

        while (i < 3)
        {
            while (flashlight.intensity > 0.2f)
            {
                flashlight.intensity -= dimVal;
                yield return null;
            }

            while (flashlight.intensity < maxIntensity)
            {
                flashlight.intensity += dimVal;
                yield return null;
            }

            i++;
        }

        yield return new WaitForSecondsRealtime(1f);

        while(flashlight.intensity > 0)
        {
            if (flashlight.intensity < 0) flashlight.intensity = 0;
            else
            {
                flashlight.intensity -= dimVal;
                yield return null;
            }
        }

        GameObject.Find("Player").GetComponent<PlayerManager>().SetFlashlight(false);
    }
}
