using NodeGraph;
using NodeGraph.Visual;
using UnityEngine;

public class ConnectionManager : MonoBehaviour
{
    private static ConnectionManager instance;
    public static ConnectionManager Instance
    {
        get => instance;
        set
        {
            if (instance)
                Destroy(value.gameObject);
            else
            {
                instance = value;
                DontDestroyOnLoad(value.gameObject);
            }
        }
    }

    [SerializeField] Edge currentEdgeInHold;
    [SerializeField] Node probableParent, probableChild;

    private void Awake()
    {
        Instance = this;
    }

    public void CreateConenction(Knob from, KnobType knobType)
    {
        currentEdgeInHold = new GameObject("edge").AddComponent<Edge>();
        UIfy(from.GetComponentInParent<Canvas>(), currentEdgeInHold.transform);
        if(knobType == KnobType.Input)
            probableParent = from.GetNodeOfKnob();
        else
            probableChild = from.GetNodeOfKnob();
    }

    public void CompleteConnection(Knob knob, KnobType knobType)
    {
        if (knobType == KnobType.Input && probableParent == null)
        {
            knob.GetNodeOfKnob().AssignChildren(currentEdgeInHold);
            currentEdgeInHold.AssignChildren(probableChild);
            currentEdgeInHold.name += "-" + knob.GetNodeOfKnob().name + "~" + probableChild.name;
            ResetState();
        }
        else if (knobType == KnobType.Output && probableChild == null)
        {
            currentEdgeInHold.AssignChildren(knob.GetNodeOfKnob());
            probableParent.AssignChildren(currentEdgeInHold);
            currentEdgeInHold.name += "-" + probableParent.name + "~" + knob.GetNodeOfKnob().name;
            ResetState();
        }
    }

    private void ResetState()
    {
        probableParent = null;
        probableChild = null;
        currentEdgeInHold = null;
    }

    public void TryDestroyInvalidConnection()
    {
        if (!currentEdgeInHold) return;

        Destroy(currentEdgeInHold.gameObject);
        ResetState();
    }

    private void UIfy(Canvas canvas, Transform target)
    {
        target.SetParent(canvas.transform, false);
        target.gameObject.AddComponent<RectTransform>();
    }
}
