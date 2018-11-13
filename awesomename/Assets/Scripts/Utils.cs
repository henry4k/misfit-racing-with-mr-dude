using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils {

   public static int mod(int x, int m) {
        if (m == 0) return 0;
        return (x % m + m) % m;
    }
}
