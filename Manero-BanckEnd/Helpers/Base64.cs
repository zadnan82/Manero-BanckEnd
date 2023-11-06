﻿using System.Diagnostics;
using System.Text;

namespace Manero_BanckEnd.Helpers
{
    public class Base64
    {
        public static string Encode(string content)
        {
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(content);
                return Convert.ToBase64String(bytes);
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return null!;
        }

        public static string Decode(string content) 
        {
            try
            {
                byte[] bytes = Convert.FromBase64String(content);
                return Encoding.UTF8.GetString(bytes);
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return null!;
        }
    }
}
