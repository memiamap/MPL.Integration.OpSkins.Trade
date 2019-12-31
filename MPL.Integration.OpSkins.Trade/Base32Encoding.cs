using System;

namespace MPL.Integration.OpSkins.Trade
{
    /// <summary>
    /// A class that provides base32 encoding.
    /// </summary>
    public static class Base32Encoding
    {
        #region Methods
        #region _Private_
        private static char ToChar(byte b)
        {
            char ReturnValue;

            if (b < 26)
                ReturnValue = (char)(b + 65);
            else if (b < 32)
                ReturnValue = (char)(b + 24);
            else
                throw new ArgumentException("Byte is not a value Base32 value.", "b");

            return ReturnValue;
            
        }

        private static int ToValue(char c)
        {
            int ReturnValue;
            int value = (int)c;

            if (value < 91 && value > 64)
                ReturnValue = value - 65;
            else if (value < 56 && value > 49)
                ReturnValue = value - 24;
            else if (value < 123 && value > 96)
                ReturnValue = value - 97;
            else
                throw new ArgumentException("Character is not a Base32 character.", "c");

            return ReturnValue;
        }

        #endregion
        #region _Public_
        /// <summary>
        /// Decodes the specified base32 input to an array of bytes.
        /// </summary>
        /// <param name="input">A string containing the base32 string to decode.</param>
        /// <returns>An array of byte containing the decoded data</returns>
        public static byte[] ToBytes(string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentNullException("input");

            input = input.TrimEnd('='); //remove padding characters
            int byteCount = input.Length * 5 / 8; //this must be TRUNCATED
            byte[] returnArray = new byte[byteCount];

            byte curByte = 0, bitsRemaining = 8;
            int mask = 0, arrayIndex = 0;

            foreach (char c in input)
            {
                int cValue = ToValue(c);

                if (bitsRemaining > 5)
                {
                    mask = cValue << (bitsRemaining - 5);
                    curByte = (byte)(curByte | mask);
                    bitsRemaining -= 5;
                }
                else
                {
                    mask = cValue >> (5 - bitsRemaining);
                    curByte = (byte)(curByte | mask);
                    returnArray[arrayIndex++] = curByte;
                    curByte = (byte)(cValue << (3 + bitsRemaining));
                    bitsRemaining += 3;
                }
            }

            if (arrayIndex != byteCount)
                returnArray[arrayIndex] = curByte;

            return returnArray;
        }

        /// <summary>
        /// Encodes the specified input to a base32 string.
        /// </summary>
        /// <param name="input">An array of byte containing the data to encode.</param>
        /// <returns>A string containing the encoded data.</returns>
        public static string ToString(byte[] input)
        {
            if (input == null || input.Length == 0)
                throw new ArgumentNullException("input");

            int charCount = (int)Math.Ceiling(input.Length / 5d) * 8;
            char[] returnArray = new char[charCount];

            byte nextChar = 0, bitsRemaining = 5;
            int arrayIndex = 0;

            foreach (byte b in input)
            {
                nextChar = (byte)(nextChar | (b >> (8 - bitsRemaining)));
                returnArray[arrayIndex++] = ToChar(nextChar);

                if (bitsRemaining < 4)
                {
                    nextChar = (byte)((b >> (3 - bitsRemaining)) & 31);
                    returnArray[arrayIndex++] = ToChar(nextChar);
                    bitsRemaining += 5;
                }

                bitsRemaining -= 3;
                nextChar = (byte)((b << bitsRemaining) & 31);
            }

            // Add padding as needed
            if (arrayIndex != charCount)
            {
                returnArray[arrayIndex++] = ToChar(nextChar);
                while (arrayIndex != charCount) returnArray[arrayIndex++] = '=';
            }

            return new string(returnArray);
        }

        #endregion
        #endregion
    }
}