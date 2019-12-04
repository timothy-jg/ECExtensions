﻿using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace ECExtensions
{
    public static class ModMath
    {
        public static BigInteger Mod(this BigInteger value, BigInteger modulus) // https://stackoverflow.com/questions/1082917/mod-of-negative-number-is-melting-my-brain
        {
            var r = value % modulus;
            return r < 0 ? r + modulus : r;
        }

        public static BigInteger ModAdd(this BigInteger augend, BigInteger addend, BigInteger p) =>
            (augend + addend).Mod(p);

        public static BigInteger ModSubtract(this BigInteger minuend, BigInteger subtrahend, BigInteger p) =>
            (minuend - subtrahend).Mod(p);

        public static BigInteger ModMultiply(this BigInteger multiplicand, BigInteger multiplier, BigInteger p) =>
            (multiplier * multiplicand).Mod(p);

        /// <summary>
        /// Performs the multiplicative inverse integer n modulo p
        /// </summary>
        /// <param name="k">An Integer</param>
        /// <param name="p">Modulus</param>
        /// <returns>Integer m such that (n * m) mod p = 1</returns>
        public static BigInteger ModMultInverse(this BigInteger k, BigInteger p) // https://andrea.corbellini.name/2015/05/23/elliptic-curve-cryptography-finite-fields-and-discrete-logarithms/
        {
            if (k == 0)
                throw new Exception();

            if (k < 0)
                return p - (-k).ModMultInverse(p);

            (BigInteger old, BigInteger curr) s = (1, 0);
            (BigInteger old, BigInteger curr) t = (0, 1);
            (BigInteger old, BigInteger curr) r = (k, p);

            while (r.curr != 0)
            {
                var quotient = r.old / r.curr;
                r = (r.curr, r.old - quotient * r.curr);
                s = (s.curr, s.old - quotient * s.curr);
                t = (t.curr, t.old - quotient * t.curr);
            }

            (BigInteger gcd, BigInteger x, BigInteger y) = (r.old, s.old, t.old);

            if (gcd != 1)
                throw new Exception();

            if ((k * x).Mod(p) != 1)
                throw new Exception();

            return x.Mod(p);
        }
    }
}