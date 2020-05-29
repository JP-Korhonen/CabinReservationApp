using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace CommonModels
{
    public class JWTConfig
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public string SecretSharedKey { get; set; }
        public TimeSpan TokenLifetime { get; set; }

        public SymmetricSecurityKey SecurityKey { get; private set; }

        /// <summary>
        /// Creates a <see cref="SymmetricSecurityKey"/> from <see cref="SecurityKey"/> using <see cref="Encoding.UTF8"/> encoding.
        /// Sets <see cref="SecretSharedKey"/> to null after the <see cref="SecurityKey"/> is set.
        /// </summary>
        public void CreateSecurityKey()
        {
            SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretSharedKey));
            SecretSharedKey = null;
        }
    }
}
