using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CallMixinActions : MixinBase
{
    public List<MixinBase> callMixins;
    public List<MixinBase> actionsMixins;
    public override bool Check()
    {
        return true;
    }
}
