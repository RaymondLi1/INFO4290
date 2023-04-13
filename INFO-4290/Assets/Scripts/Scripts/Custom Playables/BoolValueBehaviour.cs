using UnityEngine;
using UnityEngine.Playables;

public class BoolValueBehaviour : PlayableBehaviour
{
    public bool trueOrFalse;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        BoolValue boolValue = playerData as BoolValue;
        boolValue.RunTimeValue = trueOrFalse;
    }
}
