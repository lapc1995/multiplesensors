﻿using System;

namespace OpenMovement.AxLE.Comms
{
    public class AxLEHelper
    {
        public static byte[] StringToByteArray(string hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

        /// <summary>
        /// Returns four-digit (bytewise) little-endian hex number: first two nibbles are the low byte, second two are the high byte.
        /// </summary>
        /// <returns>Four digit string</returns>
        public static string ShortToHexWordsLE(UInt16 value)
        {
            var low = value & 0xFF;
            var high = (value >> 8) & 0xFF;
            var result = low.ToString("X2") + high.ToString("X2");
            return result;
        }

        /// <summary>
        /// Returns eight-digit (bytewise) little-endian hex number: first two nibbles are the low byte, etc.
        /// </summary>
        /// <returns>Eight digit string</returns>
        public static string IntToHexWordsLE(UInt32 value)
        {
            var a = value & 0xFF;
            var b = (value >> 8) & 0xFF;
            var c = (value >> 16) & 0xFF;
            var d = (value >> 24) & 0xFF;
            var result = a.ToString("X2") + b.ToString("X2") + c.ToString("X2") + d.ToString("X2");
            return result;
        }
    }
}