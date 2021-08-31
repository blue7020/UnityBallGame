#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("YY938QTwMfY+36ZmKQM3s9NSm4CYtL2VDdY5eylp/co1aStJLwl1bOvKBcKzmS3nEcI+YuCGKtkve8H44t3ppvubGELuMGsR6cyhZQ7DkHbMDRI32ddBYTXWCm7NTisapVA/4Ixpw+aDzzdAtOmYtve/6ym9s5EL42BuYVHjYGtj42BgYej0cdfeUjZcKZU6zNZBSzA9g9cRII/NQKFTnj5YaEFJj1qzVSVRz8kkamIBNrb9sluwMir/v+iTA75GKrtRXLuYxbm9pcBGdiLuN+oD/HYA1wsMcerGWgfwSlGVlNoYWwGPrgQt3X2dE/BhUeNgQ1FsZ2hL5ynnlmxgYGBkYWKKkj2GiZGBRDjr2jL4LiBEE6SleMp2GiHpZGlKhGNiYGFg");
        private static int[] order = new int[] { 5,10,8,11,4,13,10,7,8,9,11,12,13,13,14 };
        private static int key = 97;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
