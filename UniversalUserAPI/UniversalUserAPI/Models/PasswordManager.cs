﻿using System.Security.Cryptography;
using System.Text;

namespace UniversalUserAPI.Models.Password_Manager
{ 
    public class PasswordManager
    {
        readonly String plainPassword;        

        readonly string salt;
        public string Salt { get { return salt; } }

        string computedHashedPassword;
        public string ComputedHashedPassword { get { return computedHashedPassword; } }

        readonly HashAlgorithmName algorithm=HashAlgorithmName.SHA384;
       
        //keySize value should align with hash size of used algorithm (bytes)
        //SHA384 produces a 48-byte hash value
        const int keySize=48;

        const int iterations = 100_000;
               
        //this constructor is used for registration
        public PasswordManager(String plainPassword)
        {
            this.plainPassword = plainPassword;
            this.salt = Convert.ToHexString(RandomNumberGenerator.GetBytes(keySize));
            this.computedHashedPassword = HashPassword();
        }

        //this constructor is used for login
        public PasswordManager(String plainPassword, String salt)
        {
            this.plainPassword = plainPassword;
            this.salt = salt;
            this.computedHashedPassword = HashPassword();
        }

        private string HashPassword()
        {  
            byte[] hashedPassword = Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(plainPassword),
                                                    Convert.FromHexString(salt),
                                                    iterations,
                                                    algorithm,
                                                    keySize);
            return Convert.ToHexString(hashedPassword);
        }

        /// <summary>
        /// Method for password verification
        /// </summary>
        /// <param name="dbHashedPassword"> hashed password from database</param>
        /// <returns> True if passwords match otherwise false</returns>
        public bool Compare(string dbHashedPassword)
        {
            return ComputedHashedPassword.Equals(dbHashedPassword);
        }

    }
}
