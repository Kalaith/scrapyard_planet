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

    // How many materials does it take it repair, currently using this for how much its prepared, but they that be a diffrent % later
    // such as a turrent might cost 100 materials overall, 10 repairs, 10 materials each, 10% repair, 
    // but the ship engines might be 500 materials, so at 10 at a time that is 50 repairs, only 2% each repair.
    int repairCost;

    // Instead of when an item is repaired it uses power, I am thinking instead that the repairprogress is how much of the power your using.
    // a domaint statregie if this was not implemented is to repair everything to 90/99% and only at the end do the final %, but if this change is done.
    // then 90% of everything repaired means the number of enemies attacking you is 90% and nothing finished to defend yourself.

    // How much power does this item use when turned on
    double operational_power_usage;
    double reserved_power_usage; 
    // Currently we do not have modules, but later prehaps mod should be assigned, so it can be managed via the interactive option for turning on/off
    // Module mod;

    // What tile is this object on, we might do the reverse of this, so a tile can hold an interactive item.
    Tile tile;

    public double currentPowerUsage() {
        if (status == InteractiveStatus.Off) {
            return Reserved_power_usage;
        } else if (status == InteractiveStatus.On) {
            return (Reserved_power_usage+Operational_power_usage);
        }
        return 0;
    }

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

    public int RepairCost {
        get { return repairCost; }
    }

    public double Operational_power_usage {
        get {
            return operational_power_usage;
        }

        set {
            operational_power_usage = value;
        }
    }

    public double Reserved_power_usage {
        get {
            return reserved_power_usage;
        }

        set {
            reserved_power_usage = value;
        }
    }

    // Currently we dont need to init any variables since its assumed every part of the ship is not working
    public InteractiveItem(Tile tile, double operational = 150, double reserved = 25, int repaircost = 5) {
        this.tile = tile;
        repairCost = repaircost;

        // Assign these from the constructor
        Operational_power_usage = operational;
        Reserved_power_usage = reserved;

    }

    // Might do something diffrent from deltaTime, see how it goes, it might be a flat 1, or prehaps a set float, so some items can take longer then others.
    public void repairItem() {

        // Use the deltaTime to increase the repairProgress to 100%
        if (RepairProgress < 100) {
            RepairProgress += (float)RepairCost*2;
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
