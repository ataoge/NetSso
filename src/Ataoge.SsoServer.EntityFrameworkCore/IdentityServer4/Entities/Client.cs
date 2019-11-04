using Ataoge.Data;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;

namespace IdentityServer4.EntityFramework.Entities
{
    [DbTable("Clients")]
    public class Client<TKey> : IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        //[DbAutoCreatedAttribute]
		[DbFieldAttribute("Id", IsPrimaryKey = true)]
        public TKey Id {get; set;}

		[DbFieldAttribute("Enabled")]
        public bool Enabled { get; set; } = true;

        [DbFieldAttribute("ClientId")]
        public string ClientId { get; set; }

        [DbFieldAttribute("ProtocolType")]
        public string ProtocolType { get; set; } = "oidc";// ProtocolTypes.OpenIdConnect;

        //public List<ClientSecret> ClientSecrets { get; set; }

        [DbFieldAttribute("RequireClientSecret")]
        public bool RequireClientSecret { get; set; } = true;

        [DbFieldAttribute("ClientName")]
        public string ClientName { get; set; }

        [DbFieldAttribute("Description")]
        public string Description { get; set; }


        [DbFieldAttribute("ClientUri")]
        public string ClientUri { get; set; }

        [DbFieldAttribute("LogoUri")]
        public string LogoUri { get; set; }

        [DbFieldAttribute("RequireConsent")]
        public bool RequireConsent { get; set; } = true;

        [DbFieldAttribute("AllowRememberConsent")]
        public bool AllowRememberConsent { get; set; } = true;

        [DbFieldAttribute("AlwaysIncludeUserClaimsInIdToken")]
        public bool AlwaysIncludeUserClaimsInIdToken { get; set; }

        //public List<ClientGrantType> AllowedGrantTypes { get; set; }

        [DbFieldAttribute("RequirePkce")]
        public bool RequirePkce { get; set; }

        [DbFieldAttribute("AllowPlainTextPkce")]
        public bool AllowPlainTextPkce { get; set; }

        [DbFieldAttribute("AllowAccessTokensViaBrowser")]
        public bool AllowAccessTokensViaBrowser { get; set; }

        //public List<ClientRedirectUri> RedirectUris { get; set; }
        //public List<ClientPostLogoutRedirectUri> PostLogoutRedirectUris { get; set; }

        [DbFieldAttribute("FrontChannelLogoutUri")]
        public string FrontChannelLogoutUri { get; set; }
        [DbFieldAttribute("FrontChannelLogoutSessionRequired")]
        public bool FrontChannelLogoutSessionRequired { get; set; } = true;
        [DbFieldAttribute("BackChannelLogoutUri")]
        public string BackChannelLogoutUri { get; set; }
        [DbFieldAttribute("BackChannelLogoutSessionRequired")]
        public bool BackChannelLogoutSessionRequired { get; set; } = true;

        //[DbFieldAttribute("LogoutUri")]
        //public string LogoutUri { get; set; }

        //[DbFieldAttribute("LogoutSessionRequired")]
        //public bool LogoutSessionRequired { get; set; } = true;

        [DbFieldAttribute("AllowOfflineAccess")]
        public bool AllowOfflineAccess { get; set; }

        //public List<ClientScope> AllowedScopes { get; set; }

        [DbFieldAttribute("IdentityTokenLifetime")]
        public int IdentityTokenLifetime { get; set; } = 300;
        [DbFieldAttribute("AccessTokenLifetime")]
        public int AccessTokenLifetime { get; set; } = 3600;

        [DbFieldAttribute("AuthorizationCodeLifetime")]
        public int AuthorizationCodeLifetime { get; set; } = 300;
        
        [DbFieldAttribute("ConsentLifetime")]
        public int? ConsentLifetime { get; set; } = null;
  
        [DbFieldAttribute("AbsoluteRefreshTokenLifetime")]
        public int AbsoluteRefreshTokenLifetime { get; set; } = 2592000;
        
        [DbFieldAttribute("SlidingRefreshTokenLifetime")]
        public int SlidingRefreshTokenLifetime { get; set; } = 1296000;

        [DbFieldAttribute("RefreshTokenUsage")]
        public int RefreshTokenUsage { get; set; } = 1;// (int)TokenUsage.OneTimeOnly;

        [DbFieldAttribute("UpdateAccessTokenClaimsOnRefresh")]
        public bool UpdateAccessTokenClaimsOnRefresh { get; set; }

        [DbFieldAttribute("RefreshTokenExpiration")]
        public int RefreshTokenExpiration { get; set; } = 1; // (int)TokenExpiration.Absolute;
        
        [DbFieldAttribute("AccessTokenType")]
        public int AccessTokenType { get; set; } = (int)0; // AccessTokenType.Jwt;
        
        [DbFieldAttribute("EnableLocalLogin")]
        public bool EnableLocalLogin { get; set; } = true;

        //public List<ClientIdPRestriction> IdentityProviderRestrictions { get; set; }

         [DbFieldAttribute("IncludeJwtId")]
        public bool IncludeJwtId { get; set; }
        //public List<ClientClaim> Claims { get; set; }

        [DbFieldAttribute("AlwaysSendClientClaims")]
        public bool AlwaysSendClientClaims { get; set; }

        //[DbFieldAttribute("PrefixClientClaims")]
        //public bool PrefixClientClaims { get; set; } = true;

        [DbFieldAttribute("ClientClaimsPrefix")]
        public string ClientClaimsPrefix { get; set; } = "client_";

        [DbFieldAttribute("PairWiseSubjectSalt")]
        public string PairWiseSubjectSalt { get; set; }
        //public List<ClientCorsOrigin> AllowedCorsOrigins { get; set; }
        //public List<ClientProperty> Properties { get; set; }

        [DbFieldAttribute("Created")]
        public DateTime Created { get; set; } = DateTime.UtcNow;
        
        [DbFieldAttribute("Updated")]
        public DateTime? Updated { get; set; }
        
        [DbFieldAttribute("LastAccessed")]
        public DateTime? LastAccessed { get; set; }
        
        [DbFieldAttribute("UserSsoLifetime")]
        public int? UserSsoLifetime { get; set; }
        
        [DbFieldAttribute("UserCodeType")]
        public string UserCodeType { get; set; }
        
        [DbFieldAttribute("DeviceCodeLifetime")]
        public int DeviceCodeLifetime { get; set; } = 300;
        
        [DbFieldAttribute("NonEditable")]
        public bool NonEditable { get; set; }
    }

    public class Client : Client<int>
    {
            
        public List<ClientSecret> ClientSecrets { get; set; }
       
        public List<ClientGrantType> AllowedGrantTypes { get; set; }
   
        public List<ClientRedirectUri> RedirectUris { get; set; }
        public List<ClientPostLogoutRedirectUri> PostLogoutRedirectUris { get; set; }
      
        public List<ClientScope> AllowedScopes { get; set; }
        
     
        public List<ClientIdPRestriction> IdentityProviderRestrictions { get; set; }
   
        public List<ClientClaim> Claims { get; set; }
       
        public List<ClientCorsOrigin> AllowedCorsOrigins { get; set; }
        public List<ClientProperty> Properties { get; set; }
       
    }
}