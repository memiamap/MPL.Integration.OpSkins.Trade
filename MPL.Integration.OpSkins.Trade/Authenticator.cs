using System;
using System.Security.Cryptography;

namespace MPL.Integration.OpSkins.Trade
{
    /// <summary>
    /// A class that defines a 2FA generator.
    /// </summary>
    internal class Authenticator
    {
        #region Constructors
        static Authenticator()
        {
            // Defaults
            _Step = 30;
            _OutputSize = 6;
        }

        #endregion

        #region Declarations
        #region _Constants_
        private const long c_UNIX_EPOCH_TICKS = 621355968000000000L;
        private const long c_TICKS_TO_SECONDS = 10000000L;

        #endregion
        #region _Members_
        private static int _OutputSize;
        private static int _Step;

        #endregion
        #endregion

        #region Methods
        #region _Private_
        private static long CalculateTimeStep()
        {
            return CalculateTimeStep(DateTime.UtcNow);
        }
        private static long CalculateTimeStep(DateTime timestamp)
        {
            return CalculateTimeStep(timestamp, _Step);
        }
        private static long CalculateTimeStep(DateTime timestamp, int step)
        {
            long UnixTimestamp;
            long ReturnValue;

            UnixTimestamp = (timestamp.Ticks - c_UNIX_EPOCH_TICKS) / c_TICKS_TO_SECONDS;
            ReturnValue = UnixTimestamp / step;

            return ReturnValue;
        }

        private static byte[] GetBigEndianBytes(long input)
        {
            byte[] ReturnValue;

            ReturnValue = BitConverter.GetBytes(input);
            Array.Reverse(ReturnValue);

            return ReturnValue;
        }
        private static byte[] GetBigEndianBytes(int input)
        {
            byte[] ReturnValue;

            ReturnValue = BitConverter.GetBytes(input);
            Array.Reverse(ReturnValue);

            return ReturnValue;
        }

        private static string TruncateOutput(long input, int digitCount)
        {
            string ReturnValue;
            int TruncatedValue;

            TruncatedValue = ((int)input % (int)Math.Pow(10, digitCount));
            ReturnValue = TruncatedValue.ToString().PadLeft(digitCount, '0');

            return ReturnValue;
        }

        #endregion
        #region _Public_
        /// <summary>
        /// Computes the current 2FA for the specified key.
        /// </summary>
        /// <param name="key">An array of byte containing the key to compute the 2FA for.</param>
        /// <returns>A string containing the computed 2FA.</returns>
        internal static string Compute2FA(byte[] key)
        {
            byte[] Data;
            byte[] HashedValue = null;
            int Offset;
            long Passcode;
            string ReturnValue;
            long TimeStep;

            TimeStep = CalculateTimeStep();
            Data = GetBigEndianBytes(TimeStep);

            using (HMAC hmac = new HMACSHA1())
            {
                hmac.Key = key;
                HashedValue = hmac.ComputeHash(Data);
            }

            Offset = HashedValue[HashedValue.Length - 1] & 0x0F;
            Passcode = (HashedValue[Offset] & 0x7f) << 24
                        | (HashedValue[Offset + 1] & 0xff) << 16
                        | (HashedValue[Offset + 2] & 0xff) << 8
                        | (HashedValue[Offset + 3] & 0xff) % 1000000;

            ReturnValue = TruncateOutput(Passcode, _OutputSize);

            return ReturnValue;
        }
        /// <summary>
        /// Computes the current 2FA for the specified key.
        /// </summary>
        /// <param name="key">An string containing the key to compute the 2FA for.</param>
        /// <returns>A string containing the computed 2FA.</returns>
        internal static string Compute2FA(string secret)
        {
            string ReturnValue;

            ReturnValue = Compute2FA(Base32Encoding.ToBytes(secret));

            return ReturnValue;
        }

        #endregion
        #endregion
    }
}