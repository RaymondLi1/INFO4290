using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BoolValueClip : PlayableAsset
{
    public bool boolValue;
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<BoolValueBehaviour>.Create(graph);
        BoolValueBehaviour boolValueBehaviour = playable.GetBehaviour();
        boolValueBehaviour.trueOrFalse = boolValue;

        return playable;
    }
}
