#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class AppleTangle
    {
        private static byte[] data = System.Convert.FromBase64String("aWMnZGhpY25zbmhpdCdoYSdydGIPLAEGAgIABQYRGW9zc3d0PSgocDI1NjM3NDFdEAo0Mjc1Nz41NjM3LYFPgfAKBgYCAgc3ZTYMNw4BBFIDARQFUlQ2FDcWAQRSAw0UDUZ3dydmaWMnZGJ1c25hbmRmc25oaSd3D1k3hQYWAQRSGicDhQYPN4UGAzcCBwSFBggHN4UGDQWFBgYH45auDmMyJBJMEl4atJPw8ZuZyFe9xl9XfTeFBnE3CQEEUhoIBgb4AwMEBQbeMXjGgFLeoJ6+NUX839J2mXmmVSdoYSdzb2Inc29iaSdmd3drbmRmcHApZnd3a2IpZGhqKGZ3d2tiZGbHZDRw8D0AK1Hs3QgmCd29dB5IsrAcupRFIxUtwAgasUqbWWTPTIcQOiFgJ400bfAKhcjZ7KQo/lRtXGOHEyzXbkCTcQ7582yKKUeh8EBKeF6gAg57EEdRFhlz1LCMJDxApNJoKydkYnVzbmFuZGZzYid3aGtuZH6IdIZnwRxcDiiVtf9DT/dnP5kS8hiW3BlAV+wC6ll+gyrsMaVQS1Lrd2tiJ0RidXNuYW5kZnNuaGknRnJ+J2Z0dHJqYnQnZmRkYndzZmlkYgE3CAEEUhoUBgb4AwI3BAYG+DcaCgEOLYFPgfAKBgYCAgcEhQYGB1sRNxMBBFIDBBQKRnd3a2InVWhocyPl7Nawd9gIQuYgzfZqf+rgshAQsj2q8wgJB5UMtiYRKXPSOwrcZRF1ZmRzbmRiJ3RzZnNiamJpc3QpNzQxXTdlNgw3DgEEUgMBFAVSVDYUhQYHAQ4tgU+B8GRjAgY3hvU3LQEYgoSCHJ46QDD1rpxHiSvTtpcV32CID7Mn8MyrKydod7E4BjeLsETIbmFuZGZzbmhpJ0Zyc29odW5zfjY3FgEEUgMNFA1Gd3drYidOaWQpNiE3IwEEUgMMFBpGd3drYidEYnVzMZ5LKn+w6ouc2/RwnPVx1XA3SMZzb2h1bnN+NhE3EwEEUgMEFApGdwDrej6EjFQn1D/DtridSA1s+Cz7a2InTmlkKTYhNyMBBFIDDBQaRncBBFIaCQMRAxMs125Ak3EO+fNsijeFA7w3hQSkpwQFBgUFBgU3CgEOufN0nOnVYwjMfkgz36U5/n/4bM9O33GYNBNipnCTzioFBAYHBqSFBrY3X+tdAzWLb7SIGtlidPhgWWK7d2tiJ1VoaHMnREY3GRAKNzE3MzV4Rq+f/tbNYZsjbBbXpLzjHC3EGIwejtn+TGvyAKwlNwXvHzn/Vw7UrKR2lUBUUsaoKEa0//zkd8rhpEtla2IndHNmaWNmdWMnc2J1anQnZq/beSUyzSLS3gjRbNOlIyQW8KarJ0RGN4UGJTcKAQ4tgU+B8AoGBgbOHnXyWgnSeFic9SIEvVKISloK9pKZfQujQIxc0xEwNMzDCErJE27WKUeh8EBKeA9ZNxgBBFIaJAMfNxFzbmFuZGZzYidlfidmaX4nd2Z1cyg3hsQBDywBBgICAAUFN4axHYa0QnkYS2xXkUaOw3NlDBeERoA0jYZVYmtuZmlkYidoaSdzb250J2RidQiaOvQsTi8dz/nJsr4J3lkb0cw6V62N0t3j+9cOADC3cnIm");
        private static int[] order = new int[] { 43,35,7,35,7,45,33,14,20,28,58,41,36,57,59,56,32,52,44,55,56,59,40,29,47,36,53,29,46,56,34,49,59,41,46,58,48,45,51,58,46,43,50,56,53,56,52,59,59,50,54,57,55,53,54,58,56,59,59,59,60 };
        private static int key = 7;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
