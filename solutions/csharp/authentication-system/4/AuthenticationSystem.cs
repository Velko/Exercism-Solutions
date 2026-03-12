using System.Collections.Generic;
using System.Linq;

public class Authenticator
{
    private class EyeColor
    {
        public const string Blue = "blue";
        public const string Green = "green";
        public const string Brown = "brown";
        public const string Hazel = "hazel";
        public const string Brey = "grey";
    }

    public Authenticator(Identity admin)
    {
        this.admin = admin;
    }

    private readonly Identity admin;

    private readonly Dictionary<string, Identity> developers
        = new Dictionary<string, Identity>
        {
            ["Bertrand"] = new Identity
            {
                Email = "bert@ex.ism",
                EyeColor = "blue"
            },

            ["Anders"] = new Identity
            {
                Email = "anders@ex.ism",
                EyeColor = "brown"
            }
        };

    public Identity Admin => new Identity{ Email = admin.Email, EyeColor = admin.EyeColor };

    public IDictionary<string, Identity> GetDevelopers()
        => developers.ToDictionary(k => k.Key, v => new Identity { Email = v.Value.Email, EyeColor = v.Value.EyeColor }).AsReadOnly();

}

public struct Identity
{
    public string Email { get; set; }

    public string EyeColor { get; set; }
}
