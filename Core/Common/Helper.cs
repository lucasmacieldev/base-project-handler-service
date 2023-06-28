﻿using System;
using System.Text;
using System.Security.Cryptography;


namespace Common
{
    public static class Helper
    {
        private const string Format = "x2";

        public static Guid CreateDeterministicGuid(string uniqueCode)
        {
            var hash = new StringBuilder();
            var md5provider = MD5.Create();
            var bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(uniqueCode));

            for (var i = 0; i < bytes.Length; i++)
            {
                _ = hash.Append(bytes[i].ToString(Format));
            }
            return new Guid(hash.ToString());
        }
    }
}
