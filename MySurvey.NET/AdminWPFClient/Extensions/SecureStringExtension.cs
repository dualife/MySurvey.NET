// <copyright file="SecureStringExtension.cs" company="GosselCorp" author="gossel_c">
// Copyright © 2016 Cyril GOSSELIN gossel_c@outlook.fr
// This work is free. You can redistribute it and/or modify it under the
// terms of the Do What The Fuck You Want To Public License, Version 2,
// as published by Sam Hocevar. See http://www.wtfpl.net/ for more details.
// </copyright>

using System;
using System.Runtime.InteropServices;
using System.Security;

namespace AdminWPFClient.Extensions
{
    public static class SecureStringExtension
    {
        public static string GetUnsecureString(this SecureString sec)
        {
            string result = null;
            int length = sec.Length;
            IntPtr pointer = IntPtr.Zero;
            char[] chars = new char[length];

            try
            {
                pointer = Marshal.SecureStringToBSTR(sec);
                Marshal.Copy(pointer, chars, 0, length);

                result = string.Join(string.Empty, chars);
            }
            finally
            {
                if (pointer != IntPtr.Zero)
                {
                    Marshal.ZeroFreeBSTR(pointer);
                }
            }

            return result;
        }
    }
}
