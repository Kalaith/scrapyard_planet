using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : InteractiveItem {

    public Engine(Tile tile, double operational = 200, double reserved = 100, int repairCost = 2) : base(tile, operational, reserved, repairCost) {

    }

}
