using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class TextSubBehaviour : PlayableBehaviour
{
    public string subtitleTexts;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        Text text = playerData as Text;
        text.text = subtitleTexts;
        text.color = new Color(0, 0, 0, info.weight);
    }
}
