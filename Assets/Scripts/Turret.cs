using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret {

    private GameObject _TurrentGO;
    private GameObject _CurrentTarget;
    private List<GameObject> _EnemyTargets = new List<GameObject>();

    private InteractiveItem _Switch;

    private string _Type;

    private int _BullPS;

    public Turret(InteractiveItem s, string t)
    {
        _Switch = s;
        _Type = t;
        _EnemyTargets = new List<GameObject>();
    }

    // Returns the current target of the turrent, or null if no current enemy to target
    public GameObject CurrentTarget() {

        if (_CurrentTarget == null) {
            if (EnemyTargets.Count > 0) {
                _CurrentTarget = _EnemyTargets[0];
                _EnemyTargets.RemoveAt(0);
            } else {
                _CurrentTarget = null;
            }
        }
        return _CurrentTarget;
    }

    public void addTarget(GameObject e) {
        _EnemyTargets.Add(e);
    }

    // This might be to much, the add should be enough.
    public List<GameObject> EnemyTargets {
        get { return _EnemyTargets; }
        set { _EnemyTargets = value; }
    }

    // Get the status from the switch, the switch controls its own status so we dont set from the turrent
    public InteractiveItem.InteractiveStatus Status {
        get { return _Switch.Status; }
    }

    public int BullPS {
        get {
            return _BullPS;
        }

        set {
            _BullPS = value;
        }
    }
}
