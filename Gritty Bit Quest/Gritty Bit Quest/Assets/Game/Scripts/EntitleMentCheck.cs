
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Platform;
public class EntitleMentCheck : MonoBehaviour {

void Start()
    {
        Core.Initialize("1511453535565371");
        Entitlements.IsUserEntitledToApplication().OnComplete(
            (Message msg) =>
            {
                if (msg.IsError)
                {
                    print("Oculus entitlement check not passed");
                    // User is NOT ENTITLED
                    UnityEngine.Application.Quit();
                }
                else
                {
                    print("Oculus entitlement check passed");
                    // User is entitled
                }
            }
        );
    }
}
