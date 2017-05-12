using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for options that can be placed on tiles and have some form of interaction.
public class InteractiveItem {

    public enum InteractiveStatus { Damaged, On, Off, Disabled };

    // By default all items are damaged.
    InteractiveStatus status = InteractiveStatus.Damaged;

    // If its damaged, the player needs to repair it before it becomes functional
    float repairProgress = 0;

    // How much power does this item use when turned on
    float powerUsage = 0;

    // Currently we do not have modules, but later prehaps mod should be assigned, so it can be managed via the interactive option for turning on/off
    // Module mod;

    // What tile is this object on, we might do the reverse of this, so a tile can hold an interactive item.
    Tile tile;

    public float RepairProgress {
        get {
            return repairProgress;
        }

        set {
            repairProgress = value;
        }
    }

    public InteractiveStatus Status {
        get {
            return status;
        }

        set {
            status = value;
        }
    }

    // Currently we dont need to init any variables since its assumed every part of the ship is not working
    public InteractiveItem(Tile tile) {
        this.tile = tile;
    }

    // Might do something diffrent from deltaTime, see how it goes, it might be a flat 1, or prehaps a set float, so some items can take longer then others.
    public void repairItem() {

        // Use the deltaTime to increase the repairProgress to 100%
        if (RepairProgress < 100) {
            RepairProgress += 10;
        }

        // If the repair has reached 100% turn the module on by default.
        if(RepairProgress >= 100) {
            Status = InteractiveStatus.On;
        }

    }

    // Try to turn on module, can only turn on if status is set to Off, whatever calls this should handle if they failed to turn the item on.
    public bool needsRepair() {
        if (Status == InteractiveStatus.Damaged) {
            return true;
        }
        return false;
    }


    // Try to turn on module, can only turn on if status is set to Off, whatever calls this should handle if they failed to turn the item on.
    public bool turnOn() {
        if(Status == InteractiveStatus.Off) {
            Status = InteractiveStatus.On;
            return true;
        }
        return false;
    }

    // Same as above, could be a single statement like flickSwitch, but I think this feels better for calling.
    public bool turnOff() {
        if (Status == InteractiveStatus.On) {
            Status = InteractiveStatus.Off;
            return true;
        }
        return false;
    }
}
