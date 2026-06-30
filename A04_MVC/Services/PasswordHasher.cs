/*
 * FILE          : PasswordHasher.cs
 * PROJECT       : PROG2126 - Assignment 04 - Home Inventory System
 * PROGRAMMER    : Shefilkhan Fizokhan, Shreyans Kalpesh, Chase McCash
 * FIRST VERSION : 2026-04-12
 * DESCRIPTION   : This file contains the PasswordHasher class which provides SHA-256-based
 *   password hashing and verification utility methods. These are used by the
 *   AccountController during user registration (to hash and store) and during
 *   login (to verify the submitted password against the stored hash).
 */

using System;
using System.Security.Cryptography;
using System.Text;

namespace A04_MVC.Services
{
    /*
     * NAME    : PasswordHasher
     * PURPOSE : The PasswordHasher class provides two static utility methods for
     *           secure password management. HashPassword computes a SHA-256 hex
     *           digest of a plain-text password for database storage. VerifyPassword
     *           rehashes a submitted password and compares it against the stored
     *           digest to authenticate a login attempt. Using SHA-256 ensures
     *           that passwords are never stored in plain text in the SQL Server
     *           database, as noted in course material (AuthenticationTutorialNotes).
     */
    public class PasswordHasher
    {
        //
        // METHOD      : HashPassword
        // DESCRIPTION : Computes the SHA-256 hash of the provided plain-text
        //               password encoded as UTF-8 bytes and returns the result
        //               as a lowercase hexadecimal string suitable for storage
        //               in the AppUser.PasswordHash column.
        // PARAMETERS  : string password : the plain-text password to hash
        // RETURNS     : string : lowercase hexadecimal SHA-256 hash of the password
        //
        public static string HashPassword(string password)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashBytes = SHA256.HashData(inputBytes);
            string hashHex = Convert.ToHexString(hashBytes).ToLower();
            return hashHex;
        }

        //
        // METHOD      : VerifyPassword
        // DESCRIPTION : Hashes the submitted plain-text password using HashPassword
        //               and performs a case-insensitive comparison against the stored
        //               hash retrieved from the database. Returns true only if the
        //               hashes match, indicating valid credentials.
        // PARAMETERS  : string password   : the plain-text password submitted at login
        //               string storedHash : the SHA-256 hash stored in the database
        // RETURNS     : bool : true if the submitted password matches the stored hash
        //
        public static bool VerifyPassword(string password, string storedHash)
        {
            string computedHash = HashPassword(password);
            bool isMatch = string.Equals(computedHash, storedHash, StringComparison.OrdinalIgnoreCase);
            return isMatch;
        }
    }
}
