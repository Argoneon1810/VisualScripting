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
    [SerializeField] EdgeVis currentEdgeInHold_Vis;
    [SerializeField] Node probableParent, probableChild;

    private void Awake()
    {
        Instance = this;
    }

    public void CreateConenction(Knob knob, KnobType knobType)
    {
        currentEdgeInHold = new GameObject("edge").AddComponent<Edge>();
        UIfy(knob.GetComponentInParent<Canvas>(), currentEdgeInHold.transform);

        currentEdgeInHold.gameObject.AddComponent<CanvasRenderer>();
        currentEdgeInHold_Vis = currentEdgeInHold.gameObject.AddComponent<EdgeVis>();

        if (knobType == KnobType.Input)
        {
            probableParent = knob.GetNodeOfKnob();
            currentEdgeInHold_Vis.EdgeStartsFrom(knob.transform);
        }
        else
        {
            probableChild = knob.GetNodeOfKnob();
            currentEdgeInHold_Vis.EdgeEndsTo(knob.transform);
        }
    }

    public void CompleteConnection(Knob knob, KnobType knobType)
    {
        if (knobType == KnobType.Input && probableParent == null)
        {
            knob.GetNodeOfKnob().AssignChildren(currentEdgeInHold);
            currentEdgeInHold.AssignChildren(probableChild);
            currentEdgeInHold.name += "-" + knob.GetNodeOfKnob().name + "~" + probableChild.name;
            currentEdgeInHold_Vis.EdgeStartsFrom(knob.transform);
            ResetState();
        }
        else if (knobType == KnobType.Output && probableChild == null)
        {
            currentEdgeInHold.AssignChildren(knob.GetNodeOfKnob());
            probableParent.AssignChildren(currentEdgeInHold);
            currentEdgeInHold.name += "-" + probableParent.name + "~" + knob.GetNodeOfKnob().name;
            currentEdgeInHold_Vis.EdgeEndsTo(knob.transform);
            ResetState();
        }
    }

    private void ResetState()
    {
        probableParent = null;
        probableChild = null;
        currentEdgeInHold = null;
        currentEdgeInHold_Vis.isIncomplete = false;
        currentEdgeInHold_Vis = null;
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
