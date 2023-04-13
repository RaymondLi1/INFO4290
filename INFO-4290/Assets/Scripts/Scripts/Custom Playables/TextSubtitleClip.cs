using UnityEngine;
using UnityEngine.Playables;

public class TextSubtitleClip : PlayableAsset
{
    public string subtitleTexts;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<TextSubBehaviour>.Create(graph);

        TextSubBehaviour textSubtitleBehaviour = playable.GetBehaviour();
        textSubtitleBehaviour.subtitleTexts = subtitleTexts;

        return playable;
    }
}
