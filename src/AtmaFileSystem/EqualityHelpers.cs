using System;
using System.Collections.Generic;

namespace AtmaFileSystem
{
  //this class is buggy...
  class EqualityHelpers
  {
    public static bool Equal<T1>(T1 a1, T1 a2)
    {
      return EqualityComparer<T1>.Default.Equals(a1, a2);
    }

    public static bool SafeToAccessFields<T>(T _this, T other)
    {
      if (ReferenceEquals(null, other))
      {
        return true;
      }
      if (ReferenceEquals(_this, other))
      {
        return true;
      }
      return false;
    }

    public static bool Equal<T, T1, T2>
      (T _this, T other,
        T1 a1, T1 a2,
        T2 b1, T2 b2)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(_this, other)) return true;
      return
        EqualityComparer<T1>.Default.Equals(a1, a2) &&
        EqualityComparer<T2>.Default.Equals(b1, b2);
    }


    public static bool Equal<T, T1, T2, T3>
      (T _this, T other,
        T1 a1, T1 a2,
        T2 b1, T2 b2,
        T3 c1, T3 c2)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(_this, other)) return true;
      return
        EqualityComparer<T1>.Default.Equals(a1, a2) &&
        EqualityComparer<T2>.Default.Equals(b1, b2) &&
        EqualityComparer<T3>.Default.Equals(c1, c2);
    }


    public static bool Equal<T, T1, T2, T3, T4>
      (T _this, T other,
        T1 a1, T1 a2,
        T2 b1, T2 b2,
        T3 c1, T3 c2,
        T4 d1, T4 d2)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(_this, other)) return true;
      return
        EqualityComparer<T1>.Default.Equals(a1, a2) &&
        EqualityComparer<T2>.Default.Equals(b1, b2) &&
        EqualityComparer<T3>.Default.Equals(c1, c2) &&
        EqualityComparer<T4>.Default.Equals(d1, d2);
    }


    public static bool Equal<T, T1, T2, T3, T4, T5>
      (T _this, T other,
        T1 a1, T1 a2,
        T2 b1, T2 b2,
        T3 c1, T3 c2,
        T4 d1, T4 d2,
        T5 e1, T5 e2)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(_this, other)) return true;
      return
        EqualityComparer<T1>.Default.Equals(a1, a2) &&
        EqualityComparer<T2>.Default.Equals(b1, b2) &&
        EqualityComparer<T3>.Default.Equals(c1, c2) &&
        EqualityComparer<T4>.Default.Equals(d1, d2) &&
        EqualityComparer<T5>.Default.Equals(e1, e2);
    }


    public static bool Equal<T, T1, T2, T3, T4, T5, T6>
      (T _this, T other,
        T1 a1, T1 a2,
        T2 b1, T2 b2,
        T3 c1, T3 c2,
        T4 d1, T4 d2,
        T5 e1, T5 e2,
        T6 f1, T6 f2)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(_this, other)) return true;
      return
        EqualityComparer<T1>.Default.Equals(a1, a2) &&
        EqualityComparer<T2>.Default.Equals(b1, b2) &&
        EqualityComparer<T3>.Default.Equals(c1, c2) &&
        EqualityComparer<T4>.Default.Equals(d1, d2) &&
        EqualityComparer<T5>.Default.Equals(e1, e2) &&
        EqualityComparer<T6>.Default.Equals(f1, f2);
    }


    public static bool Equal<T, T1, T2, T3, T4, T5, T6, T7>
      (T _this, T other,
        T1 a1, T1 a2,
        T2 b1, T2 b2,
        T3 c1, T3 c2,
        T4 d1, T4 d2,
        T5 e1, T5 e2,
        T6 f1, T6 f2,
        T7 g1, T7 g2)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(_this, other)) return true;
      return
        EqualityComparer<T1>.Default.Equals(a1, a2) &&
        EqualityComparer<T2>.Default.Equals(b1, b2) &&
        EqualityComparer<T3>.Default.Equals(c1, c2) &&
        EqualityComparer<T4>.Default.Equals(d1, d2) &&
        EqualityComparer<T5>.Default.Equals(e1, e2) &&
        EqualityComparer<T6>.Default.Equals(f1, f2) &&
        EqualityComparer<T7>.Default.Equals(g1, g2);
    }

    public static bool EqualsAsObject<T>(IEquatable<T> _this, object other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(_this, other)) return true;
      if (other.GetType() != _this.GetType()) return false;
      return _this.Equals((T) other);
    }

    public static int GenerateHashCode<T1>(T1 a)
    {
      unchecked
      {
        var hashCode = EqualityComparer<T1>.Default.GetHashCode(a);
        return hashCode;
      }
    }


    public static int GenerateHashCode<T1, T2>(T1 a, T2 b)
    {
      unchecked
      {
        var hashCode = EqualityComparer<T1>.Default.GetHashCode(a);
        hashCode = (hashCode*397) ^ EqualityComparer<T2>.Default.GetHashCode(b);
        return hashCode;
      }
    }


    public static int GenerateHashCode<T1, T2, T3>(T1 a, T2 b, T3 c)
    {
      unchecked
      {
        var hashCode = EqualityComparer<T1>.Default.GetHashCode(a);
        hashCode = (hashCode*397) ^ EqualityComparer<T2>.Default.GetHashCode(b);
        hashCode = (hashCode*397) ^ EqualityComparer<T3>.Default.GetHashCode(c);
        return hashCode;
      }
    }


    public static int GenerateHashCode<T1, T2, T3, T4>(T1 a, T2 b, T3 c, T4 d)
    {
      unchecked
      {
        var hashCode = EqualityComparer<T1>.Default.GetHashCode(a);
        hashCode = (hashCode*397) ^ EqualityComparer<T2>.Default.GetHashCode(b);
        hashCode = (hashCode*397) ^ EqualityComparer<T3>.Default.GetHashCode(c);
        hashCode = (hashCode*397) ^ EqualityComparer<T4>.Default.GetHashCode(d);
        return hashCode;
      }
    }

    public static int GenerateHashCode<T1, T2, T3, T4, T5>(T1 a, T2 b, T3 c, T4 d, T5 e)
    {
      unchecked
      {
        var hashCode = EqualityComparer<T1>.Default.GetHashCode(a);
        hashCode = (hashCode*397) ^ EqualityComparer<T2>.Default.GetHashCode(b);
        hashCode = (hashCode*397) ^ EqualityComparer<T3>.Default.GetHashCode(c);
        hashCode = (hashCode*397) ^ EqualityComparer<T4>.Default.GetHashCode(d);
        hashCode = (hashCode*397) ^ EqualityComparer<T5>.Default.GetHashCode(e);
        return hashCode;
      }
    }


    public static int GenerateHashCode<T1, T2, T3, T4, T5, T6>(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f)
    {
      unchecked
      {
        var hashCode = EqualityComparer<T1>.Default.GetHashCode(a);
        hashCode = (hashCode*397) ^ EqualityComparer<T2>.Default.GetHashCode(b);
        hashCode = (hashCode*397) ^ EqualityComparer<T3>.Default.GetHashCode(c);
        hashCode = (hashCode*397) ^ EqualityComparer<T4>.Default.GetHashCode(d);
        hashCode = (hashCode*397) ^ EqualityComparer<T5>.Default.GetHashCode(e);
        hashCode = (hashCode*397) ^ EqualityComparer<T6>.Default.GetHashCode(f);
        return hashCode;
      }
    }


    public static int GenerateHashCode<T1, T2, T3, T4, T5, T6, T7>(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g)
    {
      unchecked
      {
        var hashCode = EqualityComparer<T1>.Default.GetHashCode(a);
        hashCode = (hashCode*397) ^ EqualityComparer<T2>.Default.GetHashCode(b);
        hashCode = (hashCode*397) ^ EqualityComparer<T3>.Default.GetHashCode(c);
        hashCode = (hashCode*397) ^ EqualityComparer<T4>.Default.GetHashCode(d);
        hashCode = (hashCode*397) ^ EqualityComparer<T5>.Default.GetHashCode(e);
        hashCode = (hashCode*397) ^ EqualityComparer<T6>.Default.GetHashCode(f);
        hashCode = (hashCode*397) ^ EqualityComparer<T7>.Default.GetHashCode(g);
        return hashCode;
      }
    }
  }
}