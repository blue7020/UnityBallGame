#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("Ef65AsCg2Zdg3xobddjkXu2mbZsvYltg139tdhNSHH7G0KpBrgbMuUsHJTmmA4+DMbQpWwfnUbsQgmGCvvCRZ5MsyXCvbuu/ilUtft5JL7i6CIuouoeMg6AMwgx9h4uLi4+Kib4BpNPsVpD+w32jF08+ays88gfsjHb4XpFZdOk/TiJaG0oOWTS+oUA2ev0l85U+RhH+doBCCyEkH4glBDYEbXQOQ1aZBcTtul+6Zxi0fxHiLaouz0fUsBGmISKzCSDaELAZaasgw3uFwBpmkIPLZGobxbhPypNW9wiLhYq6CIuAiAiLi4oSbK3cxXWwiYWPpdyC3+D797zVZn/mjq9+JcE9ncFhpV5DZAieN0PXdtCjwqun1KS1p6zQM1qT94iJi4qL");
        private static int[] order = new int[] { 9,2,13,3,9,6,8,7,9,10,11,13,13,13,14 };
        private static int key = 138;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
