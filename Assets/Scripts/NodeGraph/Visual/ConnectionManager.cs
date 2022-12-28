using NodeGraph;
using NodeGraph.Visual;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField] Material lineMaterial, outlineMaterial;

    private void Awake()
    {
        Instance = this;
    }

    private Pair<Edge, EdgeVis> CreateEdge(Knob knob)
    {
        Edge edge = new GameObject("edge").AddComponent<Edge>();
        UIfy(knob.GetComponentInParent<Canvas>(), edge.transform);

        EdgeVis edgeVis = edge.gameObject.AddComponent<EdgeVis>();
        edge.gameObject.AddComponent<CanvasRenderer>();
        edgeVis.material = lineMaterial;

        Outline4UILineRendererCreator outline = edgeVis.gameObject.AddComponent<Outline4UILineRendererCreator>();
        outline.AssignDefaultMaterial(outlineMaterial);

        Color body = Color.HSVToRGB(0, 0, 0.16f);
        Color border = Color.HSVToRGB(0, 0, 0.12f);
        body.a = 1;
        border.a = 1;
        edgeVis.SetVisuals(body, 3.5f);
        outline.SetVisuals(border, 2f);

        return new Pair<Edge, EdgeVis>(edge, edgeVis);
    }

    public void CreateConenction(Knob knob, KnobType knobType)
    {
        var edgeSet = CreateEdge(knob);
        currentEdgeInHold = edgeSet.First;
        currentEdgeInHold_Vis = edgeSet.Second;

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
