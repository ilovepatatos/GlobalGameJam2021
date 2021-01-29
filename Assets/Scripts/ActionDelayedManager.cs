using System;
using System.Collections.Generic;
using UnityEngine;

public class ActionDelayed
{
    public float Delay;
    public Action Action;

    public ActionDelayed(float delay, Action action) {
        Delay = delay;
        Action = action;
    }
}

public class ActionDelayedManager : MonoBehaviour
{
    private static HashSet<ActionDelayed> actions = new HashSet<ActionDelayed>();

    private static List<ActionDelayed> toAddActions = new List<ActionDelayed>(), toRemoveActions = new List<ActionDelayed>();

    private void Update() {
        Refresh();
    }

    public static void Refresh() {
        foreach (ActionDelayed action in actions) {
            action.Delay -= Time.deltaTime;

            if (action.Delay > 0)
                continue;

            action.Action.Invoke();
            toRemoveActions.Add(action);
        }

        //Remove finished actions
        foreach (ActionDelayed action in toRemoveActions)
            actions.Remove(action);
        toRemoveActions.Clear();
        
        //Add actions that were added this frame
        foreach (ActionDelayed action in toAddActions)
            actions.Add(action);
        toAddActions.Clear();
    }

    public static ActionDelayed AddAction(ActionDelayed action) {
        toAddActions.Add(action);
        return action;
    }

    public static bool RemoveAction(ActionDelayed action) {
        return actions.Remove(action);
    }

    public static void ClearActionsList() {
        actions.Clear();
    }
}