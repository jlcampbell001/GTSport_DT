using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;

// Got this code from https://weblogs.asp.net/jongalloway/encrypting-passwords-in-a-net-app-config-file.
namespace GTSport_DT.General
{
    /// <summary>Allows string to be encrypted or decrypted.</summary>
    public class EncryptStrings
    {
        private static byte[] entropy = Encoding.Unicode.GetBytes("jklfdsienlkdsfnkdsfjfdfdsae");

        /// <summary>Decrypts the string.</summary>
        /// <param name="encryptedData">The encrypted string.</param>
        /// <returns>The string value after it is decrypted.</returns>
        public static SecureString DecryptString(string encryptedData)
        {
            try
            {
                byte[] decryptedData = ProtectedData.Unprotect(
                    Convert.FromBase64String(encryptedData),
                    entropy,
                    DataProtectionScope.CurrentUser);
                return ToSecureString(Encoding.Unicode.GetString(decryptedData));
            }
            catch
            {
                return new SecureString();
            }
        }

        /// <summary>Encrypts the string.</summary>
        /// <param name="input">The string to encrypt.</param>
        /// <returns>The string as an encrypted value.</returns>
        public static string EncryptString(SecureString input)
        {
            byte[] encryptedData = ProtectedData.Protect(
               Encoding.Unicode.GetBytes(ToInsecureString(input)),
                entropy,
                DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(encryptedData);
        }

        /// <summary>Converts to insecurestring.</summary>
        /// <param name="input">The secure string to convert.</param>
        /// <returns>The string value.</returns>
        public static string ToInsecureString(SecureString input)
        {
            string returnValue = string.Empty;
            IntPtr ptr = Marshal.SecureStringToBSTR(input);
            try
            {
                returnValue = Marshal.PtrToStringBSTR(ptr);
            }
            finally
            {
                Marshal.ZeroFreeBSTR(ptr);
            }
            return returnValue;
        }

        /// <summary>Converts to securestring.</summary>
        /// <param name="input">The string to convert.</param>
        /// <returns>The secure string object.</returns>
        public static SecureString ToSecureString(string input)
        {
            SecureString secure = new SecureString();
            foreach (char c in input)
            {
                secure.AppendChar(c);
            }
            secure.MakeReadOnly();
            return secure;
        }
    }
}