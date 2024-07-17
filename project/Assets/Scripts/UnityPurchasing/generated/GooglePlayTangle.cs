// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security
{
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("BJGpdCaeJGwu/TIrXT7Ct00GLrlW1/MwdxV+xUMsrkF2Swpiu5wxuMfB5Sw107tZd2E4T0Ywj71HTNQ35jfkan95FeLIo86iY/AaJ10ySrto6+Xq2mjr4Oho6+vqYM6PgstVnNPRRdKgQk65aYSreiO7+OqjzNyZ2mjryNrn7OPAbKJsHefr6+vv6unNIn+kF9X7ZGv6EE9lxL7w19tKtG9E9OQ+sWuJ3oEc9mkWNfNJWnqVFP4MQcjtHjJDPakDJpqUbUBMF4fuZHvbtqsBcQVPfpyB4CJsUf5bT86M2kTKsXlhRiyhfD9vvAriMFUpb9+m8m2+CLH4KcUS4fGUzwC/CvAXRyWRmSOlXTIRLRRabykogRj48HJSK9Q9Kg27x+jp6+rr");
        private static int[] order = new int[] { 2, 3, 5, 12, 12, 8, 8, 7, 9, 12, 13, 12, 13, 13, 14 };
        private static int key = 234;

        public static readonly bool IsPopulated = true;

        public static byte[] Data()
        {
            if (IsPopulated == false)
                return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}