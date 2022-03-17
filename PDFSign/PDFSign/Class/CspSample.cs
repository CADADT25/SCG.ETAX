using Microsoft.Win32.SafeHandles;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

namespace PDFSign.Class
{
    public class CspSample
    {
        public class Provider
        {
            public string Name { get; set; }
            public int Type { get; set; }
        }

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern bool CryptEnumProviderTypes(
 uint dwIndex,
 uint pdwReserved,
 uint dwFlags,
 [In] ref uint pdwProvType,
 StringBuilder pszTypeName,
 [In] ref uint pcbTypeName);

        [DllImport("Advapi32.dll")]
        private static extern bool CryptEnumProviders(
            int dwIndex,
            IntPtr pdwReserved,
            int dwFlags,
            ref int pdwProvType,
            StringBuilder pszProvName,
            ref int pcbProvName);

        public List<Provider> ListtempProvider()
        {
            List<Provider> installedCSPs = new List<Provider>();
            int cbName;
            int dwType;
            int dwIndex;
            StringBuilder pszName;
            dwIndex = 0;
            dwType = 1;
            cbName = 0;
            while (CryptEnumProviders(dwIndex, IntPtr.Zero, 0, ref dwType, null, ref cbName))
            {
                pszName = new StringBuilder(cbName);

                if (CryptEnumProviders(dwIndex++, IntPtr.Zero, 0, ref dwType, pszName, ref cbName))
                {
                    installedCSPs.Add(new Provider { Name = pszName.ToString(), Type = dwType });
                }
            }
            string json = JsonConvert.SerializeObject(installedCSPs);
            return installedCSPs;
        }

        const int PP_ENUMCONTAINERS = 2;
        const int PROV_RSA_FULL = 1;
        const int ERROR_MORE_DATA = 234;
        const int ERROR_NO_MORE_ITEMS = 259;
        const int CRYPT_FIRST = 1;
        const int CRYPT_NEXT = 2;
        //TODO: Find how to disable this flag (not machine keystore)
        const int CRYPT_MACHINE_KEYSET = 0x20;
        const int CRYPT_VERIFYCONTEXT = unchecked((int)0xF0000000);

        public static IList<string> EnumerateKeyContainers(string providerName, int providerType)
        {
            ProvHandle prov;
            if (!CryptAcquireContext(out prov, null, providerName, providerType, CRYPT_MACHINE_KEYSET | CRYPT_VERIFYCONTEXT))
                throw new Win32Exception(Marshal.GetLastWin32Error());

            List<string> list = new List<string>();
            IntPtr data = IntPtr.Zero;
            try
            {
                int flag = CRYPT_FIRST;
                int len = 0;
                if (!CryptGetProvParam(prov, PP_ENUMCONTAINERS, IntPtr.Zero, ref len, flag))
                {
                    if (Marshal.GetLastWin32Error() != ERROR_MORE_DATA)
                        throw new Win32Exception(Marshal.GetLastWin32Error());
                }

                data = Marshal.AllocHGlobal(len);
                do
                {
                    if (!CryptGetProvParam(prov, PP_ENUMCONTAINERS, data, ref len, flag))
                    {
                        if (Marshal.GetLastWin32Error() == ERROR_NO_MORE_ITEMS)
                            break;

                        //throw new Win32Exception(Marshal.GetLastWin32Error());
                    }

                    list.Add(Marshal.PtrToStringAnsi(data));
                    flag = CRYPT_NEXT;
                }
                while (true);
            }
            finally
            {
                if (data != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(data);
                }

                prov.Dispose();
            }
            return list;
        }

        private sealed class ProvHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ProvHandle()
                : base(true)
            {
            }

            protected override bool ReleaseHandle()
            {
                return CryptReleaseContext(handle, 0);
            }

            [DllImport("advapi32.dll")]
            private static extern bool CryptReleaseContext(IntPtr hProv, int dwFlags);
        }


        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern bool CryptAcquireContext(out ProvHandle phProv, string pszContainer, string pszProvider, int dwProvType, int dwFlags);

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool CryptGetProvParam(ProvHandle hProv, int dwParam, IntPtr pbData, ref int pdwDataLen, int dwFlags);

    }
}
