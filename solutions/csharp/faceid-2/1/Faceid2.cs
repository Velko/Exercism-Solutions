using System;
using System.Collections.Generic;

public class FacialFeatures : IEquatable<FacialFeatures>
{
    public string EyeColor { get; }
    public decimal PhiltrumWidth { get; }

    public FacialFeatures(string eyeColor, decimal philtrumWidth)
    {
        EyeColor = eyeColor;
        PhiltrumWidth = philtrumWidth;
    }

    public override bool Equals(object obj) => this.Equals(obj as FacialFeatures);
    
    public bool Equals(FacialFeatures p)
    {
        if (p is null)
        {
            return false;
        }

        // Optimization for a common success case.
        if (Object.ReferenceEquals(this, p))
        {
            return true;
        }

        // If run-time types are not exactly the same, return false.
        if (this.GetType() != p.GetType())
        {
            return false;
        }

        // Return true if the fields match.
        // Note that the base class is not invoked because it is
        // System.Object, which defines Equals as reference equality.
        return (EyeColor == p.EyeColor) && (PhiltrumWidth == p.PhiltrumWidth);
    }

    public override int GetHashCode() => (EyeColor, PhiltrumWidth).GetHashCode();

}

public class Identity : IEquatable<Identity>
{
    public string Email { get; }
    public FacialFeatures FacialFeatures { get; }

    public Identity(string email, FacialFeatures facialFeatures)
    {
        Email = email;
        FacialFeatures = facialFeatures;
    }
    
    public bool Equals(Identity p)
    {
        if (p is null)
        {
            return false;
        }

        // Optimization for a common success case.
        if (Object.ReferenceEquals(this, p))
        {
            return true;
        }

        // If run-time types are not exactly the same, return false.
        if (this.GetType() != p.GetType())
        {
            return false;
        }

        // Return true if the fields match.
        // Note that the base class is not invoked because it is
        // System.Object, which defines Equals as reference equality.
        return (Email == p.Email) && (FacialFeatures.Equals(p.FacialFeatures));
    }

    public override int GetHashCode() => (Email, FacialFeatures.GetHashCode()).GetHashCode();
}

public class Authenticator
{
    public static bool AreSameFace(FacialFeatures faceA, FacialFeatures faceB)
    {
        return faceA.Equals(faceB);
    }

    public bool IsAdmin(Identity identity)
    {
        return AreSameFace(identity.FacialFeatures, new FacialFeatures("green", 0.9m))
            && identity.Email == "admin@exerc.ism";
    }

    private List<Identity> m_registeredIdentities = new List<Identity>();
    
    public bool Register(Identity identity)
    {
        if (IsRegistered(identity))
            return false;

        m_registeredIdentities.Add(identity);
        return true;
    }

    public bool IsRegistered(Identity identity) => m_registeredIdentities.Contains(identity);

    public static bool AreSameObject(Identity identityA, Identity identityB) =>
        Object.ReferenceEquals(identityA, identityB);

}
