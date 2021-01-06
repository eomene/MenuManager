using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cradaptive.TransitionsTypes;

[RequireComponent(typeof(ScreenOpener), typeof(ScreenCloser))]
public class TransitionElement : MonoBehaviour
{
    ScreenCloser screenCloser;
    ScreenOpener screenOpener;
    [HideInInspector, Header("Opening Settings")]
    public TransitionType openingTransitionType;
    [HideInInspector]
    public TransitionHelperType postOpeningTransitionEffect;
    [HideInInspector]
    public MoveDirection entryDirection;
    MoveTransitionData OpeningTransitionData = new MoveTransitionData();

    [HideInInspector, Header("Closing Settings")]
    public TransitionType closingTransitionType;
    [HideInInspector]
    public TransitionHelperType preClosingTransitionEffect;
    [HideInInspector]
    public MoveDirection exitDirection;
    MoveTransitionData ClosingTransitionData = new MoveTransitionData();


    private void Awake()
    {

        // SetUpTransitions();
    }

    public void SetUpTransitions()
    {
        screenCloser = GetComponent<ScreenCloser>();
        screenOpener = GetComponent<ScreenOpener>();
        Transition[] transitions = GetComponents<Transition>();
        TransitionHelper[] transitionHelper = GetComponents<TransitionHelper>();

        for (int i = 0; i < transitions.Length; i++)
        {
            DestroyImmediate(transitions[i]);
        }
        for (int i = 0; i < transitionHelper.Length; i++)
        {
            DestroyImmediate(transitionHelper[i]);
        }
        if (TryGetComponent(out CanvasGroup canvasGroup))
        {
            DestroyImmediate(canvasGroup);
        }
        Transition transition = GetTransition(closingTransitionType);
        if (transition != null)
        {
            ClosingTransitionData = new MoveTransitionData();
            ClosingTransitionData.exitDirection = exitDirection;
            ClosingTransitionData.exitTransitionHelper = preClosingTransitionEffect;
            transition.SetUpData(ClosingTransitionData);
        }
        screenCloser.SetUpTranstion(transition);
        transition = GetTransition(openingTransitionType);
        if (transition != null)
        {
            OpeningTransitionData = new MoveTransitionData();
            OpeningTransitionData.entryDirection = entryDirection;
            OpeningTransitionData.entryTransitionHelper = postOpeningTransitionEffect;
            transition.SetUpData(OpeningTransitionData);
        }
        screenOpener.SetUpTranstion(transition);
    }

    public Transition GetTransition(TransitionType transitionHelperType)
    {
        Transition transition = null;
        switch (transitionHelperType)
        {
            case TransitionType.Fade:
                transition = gameObject.AddComponent<FadeTransition>();
                //if (TryGetComponent(out FadeTransition fade))
                //{
                //    transition = fade;
                //}
                //else
                //{
                //    transition = gameObject.AddComponent<FadeTransition>();
                //}
                break;
            case TransitionType.Move:
                transition = gameObject.AddComponent<MoveTransition>();
                //if (TryGetComponent(out MoveTransition move))
                //{
                //    transition = move as Transition;
                //}
                //else
                //{
                //    transition = gameObject.AddComponent<MoveTransition>();
                //}

                break;
        }
        return transition;
    }
    [ContextMenu("Open Menu")]
    public void Open()
    {
        screenOpener.Open();
    }

    [ContextMenu("Close Menu")]
    public void Close()
    {
        screenCloser.Close();
    }

}